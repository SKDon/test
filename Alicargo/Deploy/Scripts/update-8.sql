USE [$(MainDbName)]
GO

ALTER TABLE [dbo].[Country] DROP COLUMN [Code];
GO

ALTER TABLE [dbo].[Application] ADD [ForwarderId] BIGINT CONSTRAINT [_Temp] DEFAULT (1) NOT NULL
GO

ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Forwarder_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [dbo].[Forwarder] ([Id]);
GO

ALTER TABLE [dbo].[Application] DROP CONSTRAINT [_Temp];
GO

ALTER TABLE [dbo].[Client] DROP CONSTRAINT [FK_dbo.Client_dbo.Transit_Transit_Id];
ALTER TABLE [dbo].[Client] WITH NOCHECK ADD CONSTRAINT [FK_dbo.Client_dbo.Transit_TransitId] 
	FOREIGN KEY ([TransitId]) REFERENCES [dbo].[Transit] ([Id]);
GO

INSERT [dbo].[City] ([Name_En], [Name_Ru], [Position])
SELECT t.[City],  t.[City], 132-COUNT( t.[City]) AS [Position]
FROM [dbo].[Transit] t
GROUP BY  t.[City]
ORDER BY 132-COUNT([City]) DESC,  t.[City] ASC
GO

ALTER TABLE [dbo].[Transit] DROP CONSTRAINT [FK_dbo.Transit_dbo.Carrier_Carrier_Id];
ALTER TABLE [dbo].[Transit] WITH NOCHECK ADD CONSTRAINT [FK_dbo.Transit_dbo.Carrier_CarrierId] 
	FOREIGN KEY ([CarrierId]) REFERENCES [dbo].[Carrier] ([Id]);
GO

ALTER TABLE [dbo].[Transit] ADD [CityId] BIGINT CONSTRAINT [_Temp] DEFAULT (1) NOT NULL
GO

ALTER TABLE [dbo].[Transit] ADD CONSTRAINT [FK_dbo.Transit_dbo.City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id])
GO

UPDATE t
SET t.[CityId] = c.[Id]
FROM [dbo].[Transit] t
JOIN [dbo].[City] c
ON t.[City] = c.[Name_En]
GO

ALTER TABLE [dbo].[Transit] DROP COLUMN [City];
GO

ALTER TABLE [dbo].[Transit] DROP CONSTRAINT [_Temp];
GO

CREATE TABLE [dbo].[ForwarderCity]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[ForwarderId] BIGINT NOT NULL,
	[CityId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.ForwarderCity] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.ForwarderCity_dbo.Forwarder_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [dbo].[Forwarder] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.ForwarderCity_dbo.City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]) ON DELETE CASCADE
)
GO

INSERT [dbo].[ForwarderCity] ([ForwarderId], [CityId])
SELECT 1, [Id]
FROM [dbo].[City]
GO

CREATE TABLE [dbo].[SenderCountry]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[SenderId] BIGINT NOT NULL,
	[CountryId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.SenderCountry] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.SenderCountry_dbo.Sender_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Sender] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.SenderCountry_dbo.Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]) ON DELETE CASCADE
)
GO

INSERT [dbo].[SenderCountry] ([SenderId], [CountryId])
SELECT 1, [Id]
FROM [dbo].[Country]
GO


ALTER PROCEDURE [dbo].[Sender_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			s.[Name],
			s.[Email],
			s.[TariffOfTapePerBox],
			u.[Login],
			u.[TwoLetterISOLanguageName] AS [Language]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]
	WHERE	s.[Id] = @Id

END
GO
PRINT N'Altering [dbo].[Sender_GetTariffs]...';


GO
ALTER PROCEDURE [dbo].[Sender_GetTariffs]
	@Ids [dbo].[IdsTable] READONLY

AS BEGIN
	SET NOCOUNT ON;

	SELECT	s.[Id],
			s.[TariffOfTapePerBox]
	FROM	[dbo].[Sender] s
	WHERE	s.[Id] IN (SELECT [Id] FROM @Ids)

END
GO
PRINT N'Altering [dbo].[Sender_GetUserId]...';


GO
ALTER PROCEDURE [dbo].[Sender_GetUserId]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[UserId]
	FROM	[dbo].[Sender] s
	WHERE	s.[Id] = @Id

END
GO
PRINT N'Creating [dbo].[Country_Add]...';


GO
CREATE PROCEDURE [dbo].[Country_Add]
	@EnglishName NVARCHAR(128),
	@RussianName NVARCHAR(128),
	@Position INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Position])
	OUTPUT INSERTED.[Id]
	VALUES (@EnglishName, @RussianName, @Position)

END
GO
PRINT N'Creating [dbo].[Country_GetList]...';


GO
CREATE PROCEDURE [dbo].[Country_GetList]
	@Language CHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT c.[Id], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]) AS [Name], c.[Position]
	FROM [dbo].[Country] c
	ORDER BY c.[Position], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]), c.[Id]

END
GO
PRINT N'Creating [dbo].[Country_Update]...';


GO
CREATE PROCEDURE [dbo].[Country_Update]
	@EnglishName NVARCHAR(128),
	@RussianName NVARCHAR(128),
	@Position INT,
	@Id BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE TOP (1) [dbo].[Country]
	SET [Name_En] = @EnglishName,
		[Name_Ru] = @RussianName,
		[Position] = @Position
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[Forwarder_Add]...';


GO
CREATE PROCEDURE [dbo].[Forwarder_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC	@UserId = [dbo].[User_Add]
				@Login = @Login, @PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @Language

		INSERT	[dbo].[Forwarder]
				([UserId], [Name], [Email])
		OUTPUT	INSERTED.[Id]
		VALUES	(@UserId, @Name, @Email)

	COMMIT

END
GO
PRINT N'Creating [dbo].[Forwarder_Get]...';


GO
CREATE PROCEDURE [dbo].[Forwarder_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			f.[Id],
			u.[Id] AS [UserId],
			f.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			f.[Name]
	FROM	[dbo].[Forwarder] f
	JOIN	[dbo].[User] u
	ON		u.[Id] = f.[UserId]
	WHERE	f.[Id] = @Id

END
GO
PRINT N'Creating [dbo].[Forwarder_GetAll]...';


GO
CREATE PROCEDURE [dbo].[Forwarder_GetAll]
AS BEGIN
	SET NOCOUNT ON;

	SELECT	f.[Id],
			u.[Id] AS [UserId],
			f.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			f.[Name]
	FROM	[dbo].[Forwarder] f
	JOIN	[dbo].[User] u
	ON		u.[Id] = f.[UserId]

END
GO
PRINT N'Creating [dbo].[Forwarder_GetByCity]...';


GO
CREATE PROCEDURE [dbo].[Forwarder_GetByCity]
	@CityId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT s.[ForwarderId]
	FROM [dbo].[ForwarderCity] s
	WHERE s.[CityId] = @CityId

END
GO
PRINT N'Creating [dbo].[Forwarder_GetByUserId]...';


GO
CREATE PROCEDURE [dbo].[Forwarder_GetByUserId]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) [Id]
	FROM	[dbo].[Forwarder]
	WHERE	[UserId] = @UserId

END
GO
PRINT N'Creating [dbo].[Forwarder_Update]...';


GO
CREATE PROCEDURE [dbo].[Forwarder_Update]
	@Id BIGINT,
	@Name NVARCHAR (MAX),
	@Login NVARCHAR(320),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @Table TABLE ([UserId] BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Forwarder]
		SET		[Name] = @Name,
				[Email] = @Email
		OUTPUT	INSERTED.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login
		WHERE	[Id] IN (SELECT [UserId] FROM @Table);

	COMMIT

END
GO
PRINT N'Creating [dbo].[ForwarderCity_Get]...';


GO
CREATE PROCEDURE [dbo].[ForwarderCity_Get]
	@ForwarderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [CityId]
	FROM [dbo].[ForwarderCity]
	WHERE [ForwarderId] = @ForwarderId

END
GO
PRINT N'Creating [dbo].[ForwarderCity_Set]...';


GO
CREATE PROCEDURE [dbo].[ForwarderCity_Set]
	@CityIds [dbo].[IdsTable] READONLY,
	@ForwarderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[ForwarderCity]
		WHERE [ForwarderId] = @ForwarderId
		AND [CityId] NOT IN (SELECT [Id] FROM @CityIds)
		
		INSERT [dbo].[ForwarderCity] ([CityId], [ForwarderId])
		SELECT [Id] AS [CityId],  @ForwarderId AS [ForwarderId]
		FROM @CityIds
		WHERE [Id] NOT IN (SELECT [CityId] FROM [ForwarderCity] WHERE [ForwarderId] = @ForwarderId)

	COMMIT

END
GO
PRINT N'Creating [dbo].[Sender_GetAll]...';


GO
CREATE PROCEDURE [dbo].[Sender_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT	s.[Id] AS [EntityId],
			u.[Id] AS [UserId],
			s.[Name],
			u.[Login],
			s.[Email],
			u.[TwoLetterISOLanguageName] AS [Language]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]

END
GO
PRINT N'Creating [dbo].[Sender_GetByCountry]...';


GO
CREATE PROCEDURE [dbo].[Sender_GetByCountry]
	@CountryId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT s.[SenderId]
	FROM [dbo].[SenderCountry] s
	WHERE s.[CountryId] = @CountryId

END
GO
PRINT N'Creating [dbo].[Sender_GetByUser]...';


GO
CREATE PROCEDURE [dbo].[Sender_GetByUser]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[Id]
	FROM	[dbo].[Sender] s
	WHERE	s.[UserId] = @UserId

END
GO
PRINT N'Creating [dbo].[SenderCountry_Get]...';


GO
CREATE PROCEDURE [dbo].[SenderCountry_Get]
	@SenderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [CountryId]
	FROM [dbo].[SenderCountry]
	WHERE [SenderId] = @SenderId

END
GO
PRINT N'Creating [dbo].[SenderCountry_Set]...';


GO
CREATE PROCEDURE [dbo].[SenderCountry_Set]
	@CountryIds [dbo].[IdsTable] READONLY,
	@SenderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[SenderCountry]
		WHERE [SenderId] = @SenderId
		AND [CountryId] NOT IN (SELECT [Id] FROM @CountryIds)
		
		INSERT [dbo].[SenderCountry] ([CountryId], [SenderId])
		SELECT [Id] AS [CountryId],  @SenderId AS [SenderId]
		FROM @CountryIds
		WHERE [Id] NOT IN (SELECT [CountryId] FROM [SenderCountry] WHERE [SenderId] = @SenderId)

	COMMIT

END
GO
PRINT N'Creating [dbo].[Transit_Add]...';


GO
CREATE PROCEDURE [dbo].[Transit_Add]
	@CityId BIGINT,
	@CarrierId BIGINT,
	@Address NVARCHAR(MAX),	
	@RecipientName NVARCHAR(MAX),
	@Phone NVARCHAR(MAX),
	@MethodOfTransit INT,
	@DeliveryType INT,
	@WarehouseWorkingTime NVARCHAR(MAX)

AS BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Transit]
           ([CityId]
           ,[Address]
           ,[RecipientName]
           ,[Phone]
           ,[WarehouseWorkingTime]
           ,[MethodOfTransitId]
           ,[DeliveryTypeId]
           ,[CarrierId])
	OUTPUT INSERTED.[Id]
	VALUES (@CityId
           ,@Address
           ,@RecipientName
           ,@Phone
           ,@WarehouseWorkingTime
           ,@MethodOfTransit
           ,@DeliveryType
           ,@CarrierId)

END
GO
PRINT N'Creating [dbo].[Transit_Delete]...';


GO
CREATE PROCEDURE [dbo].[Transit_Delete]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[Transit]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[Transit_Get]...';


GO
CREATE PROCEDURE [dbo].[Transit_Get]
	@Ids [dbo].[IdsTable] READONLY

AS BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
		  ,[CityId]
		  ,[Address]
		  ,[RecipientName]
		  ,[Phone]
		  ,[WarehouseWorkingTime]
		  ,[MethodOfTransitId] AS [MethodOfTransit]
		  ,[DeliveryTypeId] AS [DeliveryType]
		  ,[CarrierId]
	  FROM [dbo].[Transit]
	 WHERE [Id] IN (SELECT [Id] FROM @Ids)

END
GO
PRINT N'Creating [dbo].[Transit_GetByApplication]...';


GO
CREATE PROCEDURE [dbo].[Transit_GetByApplication]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT t.[Id]
		  ,t.[CityId]
		  ,t.[Address]
		  ,t.[RecipientName]
		  ,t.[Phone]
		  ,t.[WarehouseWorkingTime]
		  ,t.[MethodOfTransitId] AS [MethodOfTransit]
		  ,t.[DeliveryTypeId] AS [DeliveryType]
		  ,t.[CarrierId]
	  FROM [dbo].[Transit] t
	  JOIN [dbo].[Application] a
		ON t.[Id] = a.[TransitId]
	 WHERE a.[Id] = @ApplicationId

END
GO
PRINT N'Creating [dbo].[Transit_GetByClient]...';


GO
CREATE PROCEDURE [dbo].[Transit_GetByClient]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT t.[Id]
		  ,t.[CityId]
		  ,t.[Address]
		  ,t.[RecipientName]
		  ,t.[Phone]
		  ,t.[WarehouseWorkingTime]
		  ,t.[MethodOfTransitId] AS [MethodOfTransit]
		  ,t.[DeliveryTypeId] AS [DeliveryType]
		  ,t.[CarrierId]
	  FROM [dbo].[Transit] t
	  JOIN [dbo].[Client] c
		ON t.[Id] = c.[TransitId]
	 WHERE c.[Id] = @ClientId

END
GO
PRINT N'Creating [dbo].[Transit_Update]...';


GO
CREATE PROCEDURE [dbo].[Transit_Update]
	@Id BIGINT,
	@CityId BIGINT,
	@CarrierId BIGINT,
	@Address NVARCHAR(MAX),	
	@RecipientName NVARCHAR(MAX),
	@Phone NVARCHAR(MAX),
	@MethodOfTransit INT,
	@DeliveryType INT,
	@WarehouseWorkingTime NVARCHAR(MAX)

AS BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Transit]
	   SET [CityId] = @CityId
		  ,[Address] = @Address
		  ,[RecipientName] = @RecipientName
		  ,[Phone] = @Phone
		  ,[WarehouseWorkingTime] = @WarehouseWorkingTime
		  ,[MethodOfTransitId] = @MethodOfTransit
		  ,[DeliveryTypeId] = @DeliveryType
		  ,[CarrierId] = @CarrierId
	 WHERE [Id] = @Id
 
END
GO


INSERT [dbo].[User] ([Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
SELECT 'Carrier_' + [Name], 0x0, 0x0, 'ru'
FROM [Carrier]
GO

ALTER TABLE [dbo].[Carrier] ADD [UserId] BIGINT NULL, [Email] NVARCHAR (320) NULL
GO

UPDATE c
SET c.[UserId] = u.[Id],
	c.[Email] = u.[Login]
FROM [dbo].[Carrier] c
JOIN [dbo].[User] u ON 'Carrier_' + c.[Name] = u.[Login]
GO

DROP INDEX [IX_Carrier_Name] ON [dbo].[Carrier];
GO

ALTER TABLE [dbo].[Carrier] ALTER COLUMN [Email] NVARCHAR (320) NOT NULL
ALTER TABLE [dbo].[Carrier] ALTER COLUMN [Name] NVARCHAR (MAX) NOT NULL
ALTER TABLE [dbo].[Carrier] ALTER COLUMN [UserId] BIGINT NOT NULL
GO

ALTER TABLE [dbo].[Carrier] ADD CONSTRAINT [FK_dbo.Carrier_dbo.User_UserId]
	FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Carrier]([UserId] ASC);
GO



CREATE TABLE [dbo].[CarrierCity]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[CarrierId] BIGINT NOT NULL,
	[CityId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.CarrierCity] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.CarrierCity_dbo.Carrier_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [dbo].[Carrier] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.CarrierCity_dbo.City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]) ON DELETE CASCADE
)
GO

CREATE PROCEDURE [dbo].[Carrier_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC	@UserId = [dbo].[User_Add]
				@Login = @Login, @PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @Language

		INSERT	[dbo].[Carrier]
				([UserId], [Name], [Email])
		OUTPUT	INSERTED.[Id]
		VALUES	(@UserId, @Name, @Email)

	COMMIT

END
GO

CREATE PROCEDURE [dbo].[Carrier_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			f.[Id],
			u.[Id] AS [UserId],
			f.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			f.[Name]
	FROM	[dbo].[Carrier] f
	JOIN	[dbo].[User] u
	ON		u.[Id] = f.[UserId]
	WHERE	f.[Id] = @Id

END
GO

CREATE PROCEDURE [dbo].[Carrier_GetAll]
AS BEGIN
	SET NOCOUNT ON;

	SELECT	f.[Id],
			u.[Id] AS [UserId],
			f.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			f.[Name]
	FROM	[dbo].[Carrier] f
	JOIN	[dbo].[User] u
	ON		u.[Id] = f.[UserId]

END
GO

CREATE PROCEDURE [dbo].[Carrier_GetByCity]
	@CityId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT s.[CarrierId]
	FROM [dbo].[CarrierCity] s
	WHERE s.[CityId] = @CityId

END
GO

CREATE PROCEDURE [dbo].[Carrier_GetByUserId]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) [Id]
	FROM	[dbo].[Carrier]
	WHERE	[UserId] = @UserId

END
GO

CREATE PROCEDURE [dbo].[Carrier_Update]
	@Id BIGINT,
	@Name NVARCHAR (MAX),
	@Login NVARCHAR(320),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @Table TABLE ([UserId] BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Carrier]
		SET		[Name] = @Name,
				[Email] = @Email
		OUTPUT	INSERTED.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login
		WHERE	[Id] IN (SELECT [UserId] FROM @Table);

	COMMIT

END
GO

CREATE PROCEDURE [dbo].[CarrierCity_Get]
	@CarrierId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [CityId]
	FROM [dbo].[CarrierCity]
	WHERE [CarrierId] = @CarrierId

END
GO

CREATE PROCEDURE [dbo].[CarrierCity_Set]
	@CityIds [dbo].[IdsTable] READONLY,
	@CarrierId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[CarrierCity]
		WHERE [CarrierId] = @CarrierId
		AND [CityId] NOT IN (SELECT [Id] FROM @CityIds)
		
		INSERT [dbo].[CarrierCity] ([CityId], [CarrierId])
		SELECT [Id] AS [CityId],  @CarrierId AS [CarrierId]
		FROM @CityIds
		WHERE [Id] NOT IN (SELECT [CityId] FROM [CarrierCity] WHERE [CarrierId] = @CarrierId)

	COMMIT

END
GO