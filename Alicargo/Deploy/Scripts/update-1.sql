--:setvar MainDbName "Alicargo_2_1"
GO

USE [$(MainDbName)]
GO

PRINT N'DROP OLD INDEXES AND CONSTRAINTS'
GO

DROP INDEX [IX_ApplicationEvent_StateId] ON [dbo].[ApplicationEvent];
GO

DROP INDEX [IX_AvailableState_RoleId] ON [dbo].[AvailableState];
GO

DROP INDEX [IX_AvailableState_StateId] ON [dbo].[AvailableState];
GO

DROP INDEX [IX_VisibleState_RoleId] ON [dbo].[VisibleState];
GO

DROP INDEX [IX_VisibleState_StateId] ON [dbo].[VisibleState];
GO

ALTER TABLE [dbo].[StateLocalization] DROP CONSTRAINT [DF__StateLoca__TwoLe__6B24EA82];
GO

ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF_AirWaybillCreationTimestamp];
GO

ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF__tmp_ms_xx__State__2A164134];
GO

ALTER TABLE [dbo].[Calculation] DROP CONSTRAINT [DF__Calculati__State__3493CFA7];
GO

ALTER TABLE [dbo].[Sender] DROP CONSTRAINT [DF__Sender__TariffOf__2DE6D218];
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF__User__TwoLetterI__339FAB6E];
GO

ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId];
GO

ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId];
GO

ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId];
GO

ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.State_StateId];
GO

ALTER TABLE [dbo].AvailableState DROP CONSTRAINT [FK_dbo.AvailableState_dbo.State_StateId];
GO

ALTER TABLE [dbo].AvailableState DROP CONSTRAINT [PK_dbo.AvailableState];
GO

ALTER TABLE [dbo].StateLocalization DROP CONSTRAINT [FK_dbo.StateLocalization_dbo.State_State_Id];
GO

ALTER TABLE [dbo].VisibleState DROP CONSTRAINT [FK_dbo.VisibleState_dbo.State_StateId];
GO

ALTER TABLE [dbo].VisibleState DROP CONSTRAINT [PK_dbo.VisibleState];
GO


PRINT N'ALTERING TABLES...'
GO

UPDATE [dbo].[Application] SET [CountryId] = 108 WHERE [CountryId] IS NULL
GO

ALTER TABLE [dbo].[Application] ALTER COLUMN [CountryId] BIGINT NOT NULL;
GO

UPDATE [dbo].[Application] SET [SenderId] = 1 WHERE [SenderId] IS NULL
GO

ALTER TABLE [dbo].[Application] ALTER COLUMN [SenderId] BIGINT NOT NULL;
GO

ALTER TABLE [dbo].[Country] ADD [Position] INT NULL;
GO

UPDATE [dbo].[Country] SET [Position] = [Id]
UPDATE [dbo].[Country] SET [Position] = 108	WHERE [Id] = 1
UPDATE [dbo].[Country] SET [Position] = 111	WHERE [Id] = 2
UPDATE [dbo].[Country] SET [Position] = 75	WHERE [Id] = 3
UPDATE [dbo].[Country] SET [Position] = 47	WHERE [Id] = 4
UPDATE [dbo].[Country] SET [Position] = 238	WHERE [Id] = 5
UPDATE [dbo].[Country] SET [Position] = 218	WHERE [Id] = 6
UPDATE [dbo].[Country] SET [Position] = 81	WHERE [Id] = 7
UPDATE [dbo].[Country] SET [Position] = 15	WHERE [Id] = 8
UPDATE [dbo].[Country] SET [Position] = 119	WHERE [Id] = 10
UPDATE [dbo].[Country] SET [Position] = 206	WHERE [Id] = 11
UPDATE [dbo].[Country] SET [Position] = 9	WHERE [Id] = 15
UPDATE [dbo].[Country] SET [Position] = 4	WHERE [Id] = 47
UPDATE [dbo].[Country] SET [Position] = 3	WHERE [Id] = 75
UPDATE [dbo].[Country] SET [Position] = 8	WHERE [Id] = 81
UPDATE [dbo].[Country] SET [Position] = 1	WHERE [Id] = 108
UPDATE [dbo].[Country] SET [Position] = 2	WHERE [Id] = 111
UPDATE [dbo].[Country] SET [Position] = 10	WHERE [Id] = 119
UPDATE [dbo].[Country] SET [Position] = 11	WHERE [Id] = 206
UPDATE [dbo].[Country] SET [Position] = 6	WHERE [Id] = 218
UPDATE [dbo].[Country] SET [Position] = 5	WHERE [Id] = 238
UPDATE [dbo].[Country] SET [Position] = 7	WHERE [Id] = 239
GO

ALTER TABLE [dbo].[Country] ALTER COLUMN [Position] INT NOT NULL
GO

ALTER TABLE [dbo].[State] ADD [IsSystem] BIT NULL;
GO

UPDATE [dbo].[State] SET [IsSystem] = 1
WHERE [Id] IN (1,6,7,8,9,10,11,12)
GO

UPDATE [dbo].[State] SET [IsSystem] = 0
WHERE NOT [Id] IN (1,6,7,8,9,10,11,12)
GO

ALTER TABLE [dbo].[State] ALTER COLUMN [IsSystem] BIT NOT NULL
GO

ALTER TABLE [dbo].[State] ALTER COLUMN [Name] NVARCHAR (320) NOT NULL
GO

ALTER TABLE [dbo].[StateLocalization] ALTER COLUMN [Name] NVARCHAR (320) NOT NULL
GO

EXECUTE sp_rename N'dbo.AvailableState', N'StateAvailability', 'OBJECT' 
GO

EXECUTE sp_rename N'dbo.VisibleState', N'StateVisibility', 'OBJECT' 
GO


PRINT N'CREATING TABLES...'
GO

CREATE TABLE [dbo].[EventEmailRecipient] (
	[RoleId] INT NOT NULL,
	[EventTypeId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.EventEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [EventTypeId] ASC),
);
GO

CREATE TABLE [dbo].[EmailTemplate]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL CONSTRAINT [PK_dbo.EmailTemplate] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[EventEmailTemplate]
(
	[EventTypeId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_EventEmailTemplate_EnableEmailSend] DEFAULT 1,
	
	CONSTRAINT [PK_dbo.EventEmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC, [EventTypeId] ASC),
	CONSTRAINT [FK_dbo.EventEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
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

CREATE TABLE [dbo].[StateEmailRecipient] (
	[RoleId] INT NOT NULL,
	[StateId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.StateEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC),
	CONSTRAINT [FK_dbo.StateEmailRecipient_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[StateEmailTemplate]
(
	[StateId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_StateEmailTemplate_EnableEmailSend] DEFAULT 1,
	[UseEventTemplate] BIT NOT NULL CONSTRAINT [DF_StateEmailTemplate_UseEventTemplate] DEFAULT 1,
	
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

CREATE PROCEDURE [dbo].[EventEmailRecipient_Get]
	@EventTypeId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [RoleId]
	FROM [dbo].[EventEmailRecipient]
	WHERE [EventTypeId] = @EventTypeId
END
GO

CREATE PROCEDURE [dbo].[EventEmailRecipient_Set]
	@EventTypeId BIGINT,
	@Recipients [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[EventEmailRecipient]
		WHERE [EventTypeId] = @EventTypeId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Recipients)
		
		INSERT [dbo].[EventEmailRecipient] ([EventTypeId], [RoleId])
		SELECT @EventTypeId AS [StateId], r.[Id] AS [RoleId]
		FROM @Recipients r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[EventEmailRecipient] a WHERE a.[EventTypeId] = @EventTypeId)

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

	SELECT st.[EmailTemplateId], st.[EnableEmailSend], st.[UseEventTemplate]
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
	@UseEventTemplate BIT
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
				[UseEventTemplate] = @UseEventTemplate
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
					([EmailTemplateId], [EnableEmailSend], [StateId], [UseEventTemplate])
			VALUES (@TemplateId, @EnableEmailSend, @StateId, @UseEventTemplate);
		COMMIT
	END

END
GO

CREATE PROCEDURE [dbo].[EmailTemplate_MergeEvent]
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
	FROM  [dbo].[EventEmailTemplate]
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

			UPDATE TOP(1) [dbo].[EventEmailTemplate]
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

			INSERT [dbo].[EventEmailTemplate] 
					([EmailTemplateId], [EnableEmailSend], [EventTypeId])
			VALUES (@TemplateId, @EnableEmailSend, @EventTypeId);
		COMMIT
	END

END
GO

CREATE PROCEDURE [dbo].[EmailTemplate_GetByEvent]
	@EventTypeId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [EmailTemplateId], [EnableEmailSend]
	FROM [dbo].[EventEmailTemplate]
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


PRINT N'CREATING INDEXES AND CONSTRAINTS'
GO

CREATE UNIQUE INDEX [IX_EmailTemplateLocalization_EmailTemplateId_TwoLetterISOLanguageName] 
	ON [dbo].[EmailTemplateLocalization] ([EmailTemplateId], [TwoLetterISOLanguageName])
GO

CREATE NONCLUSTERED INDEX [IX_StateEmailRecipient_StateId] ON [dbo].[StateEmailRecipient]([StateId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_StateEmailRecipient_RoleId] ON [dbo].[StateEmailRecipient]([RoleId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_StateAvailability_StateId]
    ON [dbo].[StateAvailability]([StateId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_StateAvailability_RoleId]
    ON [dbo].[StateAvailability]([RoleId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_StateVisibility_StateId]
    ON [dbo].[StateVisibility]([StateId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_StateVisibility_RoleId]
    ON [dbo].[StateVisibility]([RoleId] ASC);
GO

ALTER TABLE [dbo].[AirWaybill]
    ADD CONSTRAINT [DF_AirWaybill_CreationTimestamp] DEFAULT (GETUTCDATE()) FOR [CreationTimestamp];
GO

ALTER TABLE [dbo].[AirWaybill]
    ADD CONSTRAINT [DF_AirWaybill_StateChangeTimestamp] DEFAULT (GETUTCDATE()) FOR [StateChangeTimestamp];
GO

ALTER TABLE [dbo].[Calculation]
    ADD CONSTRAINT [DF_Calculation_StateIdTimestamp] DEFAULT (GETUTCDATE()) FOR [StateIdTimestamp];
GO

ALTER TABLE [dbo].[Sender]
    ADD CONSTRAINT [DF_Sender_TariffOfTapePerBox] DEFAULT (4) FOR [TariffOfTapePerBox];
GO

ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_TwoLetterISOLanguageName] DEFAULT 'en' FOR [TwoLetterISOLanguageName];
GO

ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]);
GO

ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Sender] ([Id]);
GO

ALTER TABLE [dbo].[AirWaybill] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);
GO

ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);
GO

ALTER TABLE [dbo].[StateLocalization] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.StateLocalization_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[StateAvailability] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.StateAvailability_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[StateAvailability] WITH NOCHECK
	ADD CONSTRAINT [PK_dbo.StateAvailability] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC)
GO

ALTER TABLE [dbo].[StateVisibility] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.StateVisibility_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[StateVisibility] WITH NOCHECK
	ADD CONSTRAINT [PK_dbo.StateVisibility] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC)
GO