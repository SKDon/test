--:setvar MainDbName "Alicargo_2_1"
GO

USE [$(MainDbName)];


GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application] DROP COLUMN [CPFileData], COLUMN [CPFileName], COLUMN [DeliveryBillFileData], COLUMN [DeliveryBillFileName], COLUMN [InvoiceFileData], COLUMN [InvoiceFileName], COLUMN [PackingFileData], COLUMN [PackingFileName], COLUMN [SwiftFileData], COLUMN [SwiftFileName], COLUMN [Torg12FileData], COLUMN [Torg12FileName];


GO
PRINT N'Creating [dbo].[City]...';


GO
CREATE TABLE [dbo].[City] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name_En]  NVARCHAR (128) NOT NULL,
    [Name_Ru]  NVARCHAR (128) NOT NULL,
    [Position] INT            NOT NULL,
    CONSTRAINT [PK_dbo.City] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[City_Add]...';


GO
CREATE PROCEDURE [dbo].[City_Add]
	@EnglishName NVARCHAR(128),
	@RussianName NVARCHAR(128),
	@Position INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[City] ([Name_En], [Name_Ru], [Position])
	OUTPUT INSERTED.[Id]
	VALUES (@EnglishName, @RussianName, @Position)

END
GO
PRINT N'Creating [dbo].[City_GetList]...';


GO
CREATE PROCEDURE [dbo].[City_GetList]
	@Language CHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT c.[Id], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]) AS [Name], c.[Position]
	FROM [dbo].[City] c
	ORDER BY c.[Position], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]), c.[Id]

END
GO
PRINT N'Creating [dbo].[City_Update]...';


GO
CREATE PROCEDURE [dbo].[City_Update]
	@EnglishName NVARCHAR(128),
	@RussianName NVARCHAR(128),
	@Position INT,
	@Id BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE TOP (1) [dbo].[City]
	SET [Name_En] = @EnglishName,
		[Name_Ru] = @RussianName,
		[Position] = @Position
	WHERE [Id] = @Id

END
GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_DeleteForce]';


GO
PRINT N'Update complete.';


GO
