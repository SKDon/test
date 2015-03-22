USE [$(DatabaseName)];


GO
PRINT N'Dropping DF_User_TwoLetterISOLanguageName...';


GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF_User_TwoLetterISOLanguageName];


GO
PRINT N'Dropping FK_dbo.Manager_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Manager] DROP CONSTRAINT [FK_dbo.Manager_dbo.User_UserId];


GO
PRINT N'Dropping FK_dbo.Broker_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Broker] DROP CONSTRAINT [FK_dbo.Broker_dbo.User_UserId];


GO
PRINT N'Dropping FK_dbo.Admin_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Admin] DROP CONSTRAINT [FK_dbo.Admin_dbo.User_UserId];


GO
PRINT N'Dropping FK_dbo.Carrier_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Carrier] DROP CONSTRAINT [FK_dbo.Carrier_dbo.User_UserId];


GO
PRINT N'Dropping FK_dbo.Client_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Client] DROP CONSTRAINT [FK_dbo.Client_dbo.User_UserId];


GO
PRINT N'Dropping FK_dbo.Forwarder_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Forwarder] DROP CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId];


GO
PRINT N'Dropping FK_dbo.Sender_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Sender] DROP CONSTRAINT [FK_dbo.Sender_dbo.User_UserId];


GO
PRINT N'Altering [dbo].[AirWaybill]...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP COLUMN [AWBFileData], COLUMN [AWBFileName], COLUMN [DrawFileData], COLUMN [DrawFileName], COLUMN [GTDAdditionalFileData], COLUMN [GTDAdditionalFileName], COLUMN [GTDFileData], COLUMN [GTDFileName], COLUMN [InvoiceFileData], COLUMN [InvoiceFileName], COLUMN [PackingFileData], COLUMN [PackingFileName];


GO
PRINT N'Starting rebuilding table [dbo].[User]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_User] (
    [Id]                       BIGINT           IDENTITY (1, 1) NOT NULL,
    [Login]                    NVARCHAR (320)   NOT NULL,
    [PasswordHash]             VARBINARY (8000) NOT NULL,
    [PasswordSalt]             VARBINARY (8000) NOT NULL,
    [TwoLetterISOLanguageName] CHAR (2)         CONSTRAINT [DF_User_TwoLetterISOLanguageName] DEFAULT 'en' NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[User])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_User] ON;
        INSERT INTO [dbo].[tmp_ms_xx_User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
        SELECT   [Id],
                 [Login],
                 [PasswordHash],
                 [PasswordSalt],
                 [TwoLetterISOLanguageName]
        FROM     [dbo].[User]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_User] OFF;
    END

DROP TABLE [dbo].[User];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_User]', N'User';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.User]', N'PK_dbo.User', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[User].[IX_User_Login]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Login]
    ON [dbo].[User]([Login] ASC);



GO
PRINT N'Creating FK_dbo.Manager_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Manager] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Manager_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating FK_dbo.Broker_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Broker] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Broker_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating FK_dbo.Admin_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Admin] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Admin_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating FK_dbo.Carrier_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Carrier] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Carrier_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating FK_dbo.Client_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Client] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Client_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating FK_dbo.Forwarder_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Forwarder] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating FK_dbo.Sender_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Sender] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Sender_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Altering [dbo].[User_Add]...';


GO
ALTER PROCEDURE [dbo].[User_Add]
	@Login						NVARCHAR(320),
	@PasswordHash				VARBINARY(8000),
	@PasswordSalt				VARBINARY(8000),
	@TwoLetterISOLanguageName	CHAR(2)

AS BEGIN
	SET NOCOUNT ON;

	INSERT	[dbo].[User] ([Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
	OUTPUT	INSERTED.[Id]
	VALUES	(@Login, @PasswordHash, @PasswordSalt, @TwoLetterISOLanguageName)

END
GO
PRINT N'Altering [dbo].[User_SetPassword]...';


GO
ALTER PROCEDURE [dbo].[User_SetPassword]
	@UserId BIGINT,
	@Salt VARBINARY(8000),
	@Hash VARBINARY(8000)
AS
	SET NOCOUNT ON;

	UPDATE	TOP(1) [dbo].[User]
	SET		[PasswordHash] = @Hash,
			[PasswordSalt] = @Salt
	WHERE	[Id] = @UserId
GO
PRINT N'Altering [dbo].[Carrier_Add]...';


GO
ALTER PROCEDURE [dbo].[Carrier_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(8000),
	@PasswordSalt VARBINARY(8000),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login,
			@PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @Language

		INSERT [dbo].[Carrier] ([UserId], [Name], [Email], [Contact], [Phone], [Address])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email, @Contact, @Phone, @Address
		FROM @Table

	COMMIT

END
GO
PRINT N'Altering [dbo].[Forwarder_Add]...';


GO
ALTER PROCEDURE [dbo].[Forwarder_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(8000),
	@PasswordSalt VARBINARY(8000),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login, @PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @Language

		INSERT [dbo].[Forwarder] ([UserId], [Name], [Email])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email
		FROM @Table

	COMMIT

END
GO
PRINT N'Altering [dbo].[Sender_Add]...';


GO
ALTER PROCEDURE [dbo].[Sender_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(8000),
	@PasswordSalt VARBINARY(8000),
	@TwoLetterISOLanguageName CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TariffOfTapePerBox MONEY,
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login, @PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @TwoLetterISOLanguageName

		INSERT [dbo].[Sender] ([UserId], [Name], [Email], [TariffOfTapePerBox], [Contact], [Phone], [Address])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email, @TariffOfTapePerBox, @Contact, @Phone, @Address
		FROM @Table

	COMMIT

END
GO
PRINT N'Refreshing [dbo].[Carrier_Get]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Carrier_Get]';


GO
PRINT N'Refreshing [dbo].[Carrier_GetAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Carrier_GetAll]';


GO
PRINT N'Refreshing [dbo].[Carrier_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Carrier_Update]';


GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_DeleteForce]';


GO
PRINT N'Refreshing [dbo].[Client_Get]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_Get]';


GO
PRINT N'Refreshing [dbo].[Client_GetAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_GetAll]';


GO
PRINT N'Refreshing [dbo].[Client_GetByUserId]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_GetByUserId]';


GO
PRINT N'Refreshing [dbo].[Forwarder_Get]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Forwarder_Get]';


GO
PRINT N'Refreshing [dbo].[Forwarder_GetAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Forwarder_GetAll]';


GO
PRINT N'Refreshing [dbo].[Forwarder_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Forwarder_Update]';


GO
PRINT N'Refreshing [dbo].[Sender_Get]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Sender_Get]';


GO
PRINT N'Refreshing [dbo].[Sender_GetAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Sender_GetAll]';


GO
PRINT N'Refreshing [dbo].[Sender_Update]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Sender_Update]';


GO
PRINT N'Refreshing [dbo].[User_GetLanguage]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_GetLanguage]';


GO
PRINT N'Refreshing [dbo].[User_GetPasswordData]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_GetPasswordData]';


GO
PRINT N'Refreshing [dbo].[User_GetUserIdByEmail]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_GetUserIdByEmail]';


GO
PRINT N'Refreshing [dbo].[User_SetLanguage]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_SetLanguage]';


GO
PRINT N'Refreshing [dbo].[User_SetLogin]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_SetLogin]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Manager] WITH CHECK CHECK CONSTRAINT [FK_dbo.Manager_dbo.User_UserId];

ALTER TABLE [dbo].[Broker] WITH CHECK CHECK CONSTRAINT [FK_dbo.Broker_dbo.User_UserId];

ALTER TABLE [dbo].[Admin] WITH CHECK CHECK CONSTRAINT [FK_dbo.Admin_dbo.User_UserId];

ALTER TABLE [dbo].[Carrier] WITH CHECK CHECK CONSTRAINT [FK_dbo.Carrier_dbo.User_UserId];

ALTER TABLE [dbo].[Client] WITH CHECK CHECK CONSTRAINT [FK_dbo.Client_dbo.User_UserId];

ALTER TABLE [dbo].[Forwarder] WITH CHECK CHECK CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId];

ALTER TABLE [dbo].[Sender] WITH CHECK CHECK CONSTRAINT [FK_dbo.Sender_dbo.User_UserId];


GO
PRINT N'Update complete.';


GO
