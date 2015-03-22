GO
USE [$(DatabaseName)];


GO
PRINT N'Dropping [dbo].[ApplicationFile].[IX_ApplicationFile_ApplicationId_TypeId]...';


GO
DROP INDEX [IX_ApplicationFile_ApplicationId_TypeId]
    ON [dbo].[ApplicationFile];


GO
PRINT N'Creating [dbo].[AwbFile]...';


GO
CREATE TABLE [dbo].[AwbFile] (
    [Id]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [AwbId]  BIGINT          NOT NULL,
    [TypeId] INT             NOT NULL,
    [Name]   NVARCHAR (320)  NOT NULL,
    [Data]   VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.AwbFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AwbFile_Add]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_Add]
	@AwbId BIGINT,
	@TypeId INT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[AwbFile] ([AwbId], [Data], [Name], [TypeId])
	OUTPUT INSERTED.[Id]
	VALUES (@AwbId, @Data, @Name, @TypeId)

END
GO
PRINT N'Creating [dbo].[AwbFile_Delete]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_Delete]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[AwbFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[AwbFile_Get]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1) [Name], [Data]
	FROM [dbo].[AwbFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[AwbFile_GetNames]...';


GO
CREATE PROCEDURE [dbo].[AwbFile_GetNames]
	@AwbIds [dbo].[IdsTable] READONLY,
	@TypeId INT

AS BEGIN
	SET NOCOUNT ON

	SELECT f.[Id], f.[Name], f.[AwbId]
	FROM [dbo].[AwbFile] f
	WHERE f.[AwbId] IN (SELECT [Id] FROM @AwbIds)
	AND f.[TypeId] = @TypeId
	ORDER BY f.[AwbId], f.[Name], f.[Id]

END
GO
PRINT N'Update complete.';


GO