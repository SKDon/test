USE [$(MainDbName)];
GO

EXEC sp_rename 'Client.Email', 'Emails', 'COLUMN'
GO

ALTER TABLE [dbo].[Client] ALTER COLUMN [Emails] NVARCHAR (MAX) NOT NULL
GO

ALTER TABLE [dbo].[Client] ADD [Balance] MONEY CONSTRAINT [DF_Client_Balance] DEFAULT (0) NOT NULL
GO

ALTER PROCEDURE [dbo].[Sender_Get]
	@Id BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[Name],
			s.[Email],
			s.[TariffOfTapePerBox],
			u.[Login],
			u.[TwoLetterISOLanguageName] AS [Language]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]
	WHERE	s.[Id] = @Id
GO
PRINT N'Altering [dbo].[Sender_Update]...';


GO
ALTER PROCEDURE [dbo].[Sender_Update]
	@Id BIGINT,
	@Login NVARCHAR(320),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TwoLetterISOLanguageName CHAR(2),
	@TariffOfTapePerBox	MONEY
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @Table TABLE(UserId BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Sender]
		SET		[Name] = @Name,
				[Email] = @Email,
				[TariffOfTapePerBox] = @TariffOfTapePerBox
		OUTPUT	inserted.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login,
				[TwoLetterISOLanguageName] = @TwoLetterISOLanguageName
		WHERE	[Id] IN(SELECT [UserId] FROM @Table);

	COMMIT

END
GO
PRINT N'Altering [dbo].[User_Add]...';


GO
ALTER PROCEDURE [dbo].[User_Add]
	@Login						NVARCHAR(320),
	@PasswordHash				VARBINARY(MAX),
	@PasswordSalt				VARBINARY(MAX),
	@TwoLetterISOLanguageName	CHAR(2)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @Table TABLE([UserId] BIGINT);
	DECLARE @UserId	BIGINT;

	INSERT	[dbo].[User] ([Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
	OUTPUT	inserted.[Id] INTO @Table
	VALUES	(@Login, @PasswordHash, @PasswordSalt, @TwoLetterISOLanguageName)

	SELECT TOP 1 @UserId = [UserId] FROM @Table
	RETURN @UserId

END
GO
PRINT N'Creating [dbo].[Client_GetBalance]...';


GO
CREATE PROCEDURE [dbo].[Client_GetBalance]
	@ClientId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT TOP(1) c.[Balance]
	FROM [dbo].[Client] c
	WHERE c.[Id] = @ClientId

END
GO
PRINT N'Creating [dbo].[Client_SetBalance]...';


GO
CREATE PROCEDURE [dbo].[Client_SetBalance]
	@ClientId BIGINT,
	@Balance MONEY
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[Client]
	SET [Balance] = @Balance
	WHERE [Id] = @ClientId

END


GO
PRINT N'Creating [dbo].[User_GetLanguage]...';


GO
CREATE PROCEDURE [dbo].[User_GetLanguage]
	@UserId BIGINT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT TOP(1) [TwoLetterISOLanguageName]
	FROM [dbo].[User]
	WHERE [Id] = @UserId

END
GO
PRINT N'Creating [dbo].[User_GetPasswordData]...';


GO
CREATE PROCEDURE [dbo].[User_GetPasswordData]
	@Login NVARCHAR(320)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT TOP(1) u.[PasswordHash], u.[PasswordSalt], u.[Id] AS [UserId]
	FROM [dbo].[User] u
	WHERE u.[Login] = @Login

END
GO
PRINT N'Creating [dbo].[User_SetLanguage]...';


GO
CREATE PROCEDURE [dbo].[User_SetLanguage]
	@UserId BIGINT,
	@Language CHAR(2)
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[User]
	SET [TwoLetterISOLanguageName] = @Language
	WHERE [Id] = @UserId

END
GO
PRINT N'Altering [dbo].[Sender_Add]...';


GO
ALTER PROCEDURE [dbo].[Sender_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@TwoLetterISOLanguageName CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TariffOfTapePerBox MONEY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC	@UserId = [dbo].[User_Add]
				@Login = @Login, @PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @TwoLetterISOLanguageName

		INSERT	[dbo].[Sender]
				([UserId], [Name], [Email], [TariffOfTapePerBox])
		OUTPUT	inserted.[Id]
		VALUES	(@UserId, @Name, @Email, @TariffOfTapePerBox)

	COMMIT

END
GO

CREATE PROCEDURE [dbo].[User_GetUserIdByEmail]
	@Email NVARCHAR (320)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT COALESCE(
		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Admin] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Client] a ON a.[UserId] = u.[Id]
		WHERE a.[Emails] LIKE @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Sender] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Broker] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Forwarder] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email)) AS [Id]

END
GO