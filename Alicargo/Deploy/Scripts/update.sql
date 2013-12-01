﻿--USE [$(MainDbName)]
GO

DROP INDEX [IX_ApplicationEvent_StateId]
GO

EXECUTE sp_rename N'dbo.AvailableState', N'StateAvailability', 'OBJECT' 
GO

EXECUTE sp_rename N'dbo.VisibleState', N'StateVisibility', 'OBJECT' 
GO

PRINT N'CREATING TABLES...'
CREATE TABLE [dbo].[ApplicationEventEmailRecipient] (
	[RoleId] INT NOT NULL,
	[EventTypeId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.ApplicationEventEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [EventTypeId] ASC),
);
GO

CREATE TABLE [dbo].[ApplicationEventEmailTemplate]
(
	[EventTypeId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_ApplicationEventEmailTemplate_EnableEmailSend] DEFAULT 1,
	
	CONSTRAINT [PK_dbo.ApplicationEventEmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC, [EventTypeId] ASC),
	CONSTRAINT [FK_dbo.ApplicationEventEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[EmailTemplate]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL CONSTRAINT [PK_dbo.EmailTemplate] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[EmailTemplateLocalization]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,
	[TwoLetterISOLanguageName] CHAR(2) NOT NULL,

	[Subject] NVARCHAR (MAX) NULL,
	[Body] NVARCHAR (MAX) NULL,
	[IsBodyHtml] BIT CONSTRAINT [DF_EmailTemplateLocalization_IsBodyHtml] DEFAULT (0) NOT NULL,

	CONSTRAINT [PK_dbo.EmailTemplateLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),

	CONSTRAINT [FK_dbo.EmailTemplateLocalization_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY 
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_EmailTemplateLocalization_EmailTemplateId_TwoLetterISOLanguageName] 
	ON [dbo].[EmailTemplateLocalization] ([EmailTemplateId], [TwoLetterISOLanguageName])
GO

CREATE TABLE [dbo].[StateEmailRecipient] (
	[RoleId] INT NOT NULL,
	[StateId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.StateEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC),
	CONSTRAINT [FK_dbo.StateEmailRecipient_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_StateEmailRecipient_StateId] ON [dbo].[StateEmailRecipient]([StateId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_StateEmailRecipient_RoleId] ON [dbo].[StateEmailRecipient]([RoleId] ASC);
GO

CREATE TABLE [dbo].[StateEmailTemplate]
(
	[StateId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_StateEmailTemplate_EnableEmailSend] DEFAULT 1,
	[UseApplicationEventTemplate] BIT NOT NULL CONSTRAINT [DF_StateEmailTemplate_UseApplicationEventTemplate] DEFAULT 1,
	
	CONSTRAINT [PK_dbo.StateEmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC, [StateId] ASC),
	CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO


PRINT N'CREATING PROCEDURES...'
GO

CREATE TYPE [dbo].[StringsTable] AS TABLE
(
	[Value] NVARCHAR(MAX)
);
GO

CREATE PROCEDURE [dbo].[ApplicationEventEmailRecipient_Get]
	@EventTypeId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [RoleId]
	FROM [dbo].[ApplicationEventEmailRecipient]
	WHERE [EventTypeId] = @EventTypeId
END
GO

CREATE PROCEDURE [dbo].[ApplicationEventEmailRecipient_Set]
	@EventTypeId BIGINT,
	@Recipients [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[ApplicationEventEmailRecipient]
		WHERE [EventTypeId] = @EventTypeId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Recipients)
		
		INSERT [dbo].[ApplicationEventEmailRecipient] ([EventTypeId], [RoleId])
		SELECT @EventTypeId AS [StateId], r.[Id] AS [RoleId]
		FROM @Recipients r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[ApplicationEventEmailRecipient] a WHERE a.[EventTypeId] = @EventTypeId)

	COMMIT

END
GO

CREATE PROCEDURE [dbo].[EmailTemplateLocalization_Get]
	@TemplateId BIGINT,
	@Localizations [dbo].[StringsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	l.[Subject],
			l.[Body],
			l.[IsBodyHtml],
			n.[Value] AS [TwoLetterISOLanguageName]
	FROM [dbo].[EmailTemplate] t CROSS JOIN @Localizations n
	LEFT JOIN [dbo].[EmailTemplateLocalization] l
	ON t.[Id] = l.[EmailTemplateId] AND l.[TwoLetterISOLanguageName] = n.[Value]
	WHERE t.[Id] = @TemplateId
	ORDER BY t.[Id], n.[Value]

END
GO

CREATE PROCEDURE [dbo].[EmailTemplate_GetByStateId]
	@StateId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT st.[EmailTemplateId], st.[EnableEmailSend], st.[UseApplicationEventTemplate]
	FROM  [dbo].[StateEmailTemplate] st
	WHERE st.[StateId] = @StateId

END
GO

CREATE PROCEDURE [dbo].[EmailTemplate_MergeState]
	@StateId BIGINT,
	@Subject NVARCHAR (MAX),
	@Body NVARCHAR (MAX),
	@IsBodyHtml BIT,
	@EnableEmailSend BIT,
	@TwoLetterISOLanguageName CHAR(2),
	@UseApplicationEventTemplate BIT
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TemplateId BIGINT;

	SELECT TOP(1) @TemplateId = st.[EmailTemplateId]
	FROM  [dbo].[StateEmailTemplate] st
	WHERE st.[StateId] = @StateId

	IF @TemplateId IS NOT NULL BEGIN
		BEGIN TRAN
			MERGE [dbo].[EmailTemplateLocalization] AS target
			USING (SELECT @Subject, @Body, @IsBodyHtml, @TemplateId, @TwoLetterISOLanguageName) AS source 
						([Subject], [Body], [IsBodyHtml], [EmailTemplateId], [TwoLetterISOLanguageName])
				ON (target.[EmailTemplateId] = source.[EmailTemplateId] 
					AND target.[TwoLetterISOLanguageName] = source.[TwoLetterISOLanguageName])
			WHEN MATCHED THEN
				UPDATE
				SET [Subject] = source.[Subject],
					[Body] = source.[Body],
					[IsBodyHtml] = source.[IsBodyHtml]
			WHEN NOT MATCHED THEN
				INSERT ([EmailTemplateId], [Subject], [Body], [IsBodyHtml], [TwoLetterISOLanguageName])
				VALUES (source.[EmailTemplateId], source.[Subject], source.[Body], source.[IsBodyHtml], source.[TwoLetterISOLanguageName]);

			UPDATE TOP(1) [dbo].[StateEmailTemplate]
			SET [EnableEmailSend] = @EnableEmailSend,
				[UseApplicationEventTemplate] = @UseApplicationEventTemplate
			WHERE [StateId] = @StateId AND [EmailTemplateId] = @TemplateId
		COMMIT
	END
	ELSE BEGIN
		BEGIN TRAN
			INSERT [dbo].[EmailTemplate] DEFAULT VALUES
			SET @TemplateId = SCOPE_IDENTITY()

			INSERT [dbo].[EmailTemplateLocalization] 
					([EmailTemplateId], [Subject], [Body], [IsBodyHtml], [TwoLetterISOLanguageName])
			VALUES (@TemplateId, @Subject, @Body, @IsBodyHtml, @TwoLetterISOLanguageName)

			INSERT [dbo].[StateEmailTemplate] 
					([EmailTemplateId], [EnableEmailSend], [StateId], [UseApplicationEventTemplate])
			VALUES (@TemplateId, @EnableEmailSend, @StateId, @UseApplicationEventTemplate);
		COMMIT
	END

END
GO

CREATE PROCEDURE [dbo].[EmailTemplate_MergeApplicationEvent]
	@EventTypeId INT,
	@Subject NVARCHAR (MAX),
	@Body NVARCHAR (MAX),
	@IsBodyHtml BIT,
	@EnableEmailSend BIT,
	@TwoLetterISOLanguageName CHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TemplateId BIGINT;

	SELECT TOP(1) @TemplateId = [EmailTemplateId]
	FROM  [dbo].[ApplicationEventEmailTemplate]
	WHERE [EventTypeId] = @EventTypeId

	IF @TemplateId IS NOT NULL BEGIN
		BEGIN TRAN
			MERGE [dbo].[EmailTemplateLocalization] AS target
			USING (SELECT @Subject, @Body, @IsBodyHtml, @TemplateId, @TwoLetterISOLanguageName) AS source 
						([Subject], [Body], [IsBodyHtml], [EmailTemplateId], [TwoLetterISOLanguageName])
				ON (target.[EmailTemplateId] = source.[EmailTemplateId] 
					AND target.[TwoLetterISOLanguageName] = source.[TwoLetterISOLanguageName])
			WHEN MATCHED THEN
				UPDATE
				SET [Subject] = source.[Subject],
					[Body] = source.[Body],
					[IsBodyHtml] = source.[IsBodyHtml]
			WHEN NOT MATCHED THEN
				INSERT ([EmailTemplateId], [Subject], [Body], [IsBodyHtml], [TwoLetterISOLanguageName])
				VALUES (source.[EmailTemplateId], source.[Subject], source.[Body], source.[IsBodyHtml], source.[TwoLetterISOLanguageName]);

			UPDATE TOP(1) [dbo].[ApplicationEventEmailTemplate]
			SET [EnableEmailSend] = @EnableEmailSend
			WHERE [EventTypeId] = @EventTypeId AND [EmailTemplateId] = @TemplateId
		COMMIT
	END
	ELSE BEGIN
		BEGIN TRAN
			INSERT [dbo].[EmailTemplate] DEFAULT VALUES
			SET @TemplateId = SCOPE_IDENTITY()

			INSERT [dbo].[EmailTemplateLocalization] 
					([EmailTemplateId], [Subject], [Body], [IsBodyHtml], [TwoLetterISOLanguageName])
			VALUES (@TemplateId, @Subject, @Body, @IsBodyHtml, @TwoLetterISOLanguageName)

			INSERT [dbo].[ApplicationEventEmailTemplate] 
					([EmailTemplateId], [EnableEmailSend], [EventTypeId])
			VALUES (@TemplateId, @EnableEmailSend, @EventTypeId);
		COMMIT
	END

END
GO

CREATE PROCEDURE [dbo].[EmailTemplate_GetByApplicationEvent]
	@EventTypeId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [EmailTemplateId], [EnableEmailSend]
	FROM [dbo].[ApplicationEventEmailTemplate]
	WHERE [EventTypeId] = @EventTypeId

END
GO

CREATE PROCEDURE [dbo].[State_Add]
	@Name  NVARCHAR (320),
	@Position INT,
	@IsSystem BIT
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT [dbo].[State] ([Name], [Position], [IsSystem])
	OUTPUT INSERTED.[Id]
	VALUES (@Name, @Position, @IsSystem)

END
GO

CREATE PROCEDURE [dbo].[State_Delete]
	@Id BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[State]
	WHERE [Id] = @Id
	AND [IsSystem] = 0

END
GO

CREATE PROCEDURE [dbo].[State_GetOrderedList]
	@Ids [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM @Ids)
		SELECT s.[Id], s.[Name], s.[Position], s.[IsSystem]
		FROM [dbo].[State] s
		WHERE s.[Id] IN (SELECT [Id] FROM @Ids)
		ORDER BY s.[Position], s.[Name], s.[Id]
	ELSE
		SELECT s.[Id], s.[Name], s.[Position], s.[IsSystem]
		FROM [dbo].[State] s
		ORDER BY s.[Position], s.[Name], s.[Id]

END
GO

CREATE PROCEDURE [dbo].[State_GetStateAvailabilities]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT s.[RoleId] AS [Role], s.[StateId]
	FROM [dbo].[StateAvailability] s
	ORDER BY s.[RoleId], s.[StateId]

END
GO

CREATE PROCEDURE [dbo].[State_GetStateEmailRecipients]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT s.[RoleId] AS [Role], s.[StateId]
	FROM [dbo].[StateEmailRecipient] s
	ORDER BY s.[RoleId], s.[StateId]

END
GO

CREATE PROCEDURE [dbo].[State_GetStateVisibilities]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT s.[RoleId] AS [Role], s.[StateId]
	FROM [dbo].[StateVisibility] s
	ORDER BY s.[RoleId], s.[StateId]

END
GO

CREATE PROCEDURE [dbo].[State_SetStateAvailabilities]
	@StateId BIGINT,
	@Roles [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[StateAvailability]
		WHERE [StateId] = @StateId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Roles)
		
		INSERT [dbo].[StateAvailability] ([StateId], [RoleId])
		SELECT @StateId AS [StateId], r.[Id] AS [RoleId]
		FROM @Roles r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[StateAvailability] a WHERE a.[StateId] = @StateId)

	COMMIT

END
GO

CREATE PROCEDURE [dbo].[State_SetStateEmailRecipients]
	@StateId BIGINT,
	@Roles [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[StateEmailRecipient]
		WHERE [StateId] = @StateId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Roles)
		
		INSERT [dbo].[StateEmailRecipient] ([StateId], [RoleId])
		SELECT @StateId AS [StateId], r.[Id] AS [RoleId]
		FROM @Roles r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[StateEmailRecipient] a WHERE a.[StateId] = @StateId)

	COMMIT

END
GO

CREATE PROCEDURE [dbo].[State_SetStateVisibilities]
	@StateId BIGINT,
	@Roles [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[StateVisibility]
		WHERE [StateId] = @StateId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Roles)
		
		INSERT [dbo].[StateVisibility] ([StateId], [RoleId])
		SELECT @StateId AS [StateId], r.[Id] AS [RoleId]
		FROM @Roles r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[StateVisibility] a WHERE a.[StateId] = @StateId)

	COMMIT

END
GO

CREATE PROCEDURE [dbo].[State_Update]
	@Id BIGINT,
	@Name  NVARCHAR (320),
	@Position INT
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [dbo].[State]
	SET [Name] = @Name, [Position] = @Position
	WHERE [Id] = @Id

END
GO

CREATE PROCEDURE [dbo].[StateLocalization_Get]
	@Ids [dbo].[IdsTable] READONLY,
	@Localizations [dbo].[StringsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM @Ids)
		SELECT	i.[Id] AS [StateId],
				COALESCE(l.[Name], s.[Name]) AS [Name],
				n.[Value] AS [TwoLetterISOLanguageName]
		FROM @Ids i CROSS JOIN @Localizations n
		JOIN [dbo].[State] s ON s.[Id] = i.[Id]
		LEFT JOIN [dbo].[StateLocalization] l
		ON s.[Id] = l.[StateId] AND l.[TwoLetterISOLanguageName] = n.[Value]
		ORDER BY i.[Id], n.[Value]
	ELSE
		SELECT	s.[Id] AS [StateId],
				COALESCE(l.[Name], s.[Name]) AS [Name],
				n.[Value] AS [TwoLetterISOLanguageName]
		FROM [dbo].[State] s CROSS JOIN @Localizations n
		LEFT JOIN [dbo].[StateLocalization] l
		ON s.[Id] = l.[StateId] AND l.[TwoLetterISOLanguageName] = n.[Value]
		ORDER BY s.[Id], n.[Value]

END
GO

CREATE PROCEDURE [dbo].[StateLocalization_Merge]
	@Name  NVARCHAR (320),
	@TwoLetterISOLanguageName CHAR(2),
	@StateId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;
	
    MERGE [dbo].[StateLocalization] AS target
    
	USING (SELECT @Name, @TwoLetterISOLanguageName, @StateId) AS source ([Name], [TwoLetterISOLanguageName], [StateId])
		ON (target.[StateId] = source.[StateId] 
		AND target.[TwoLetterISOLanguageName] = source.[TwoLetterISOLanguageName])
    
	WHEN MATCHED THEN 
        UPDATE SET [Name] = source.[Name]

	WHEN NOT MATCHED THEN	
	    INSERT ([Name], [TwoLetterISOLanguageName], [StateId])
	    VALUES (source.[Name], source.[TwoLetterISOLanguageName], source.[StateId]);

END
GO