:setvar FilesDbName "Alicargo_Files_2_1"
GO

USE [$(FilesDbName)];


GO
PRINT N'Creating [dbo].[IdsTable]...';


GO
CREATE TYPE [dbo].[IdsTable] AS TABLE (
    [Id] BIGINT NOT NULL);


GO
PRINT N'Creating [dbo].[ApplicationFile]...';


GO
CREATE TABLE [dbo].[ApplicationFile] (
    [Id]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [ApplicationId] BIGINT          NOT NULL,
    [TypeId]        INT             NOT NULL,
    [Name]          NVARCHAR (320)  NOT NULL,
    [Data]          VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.ApplicationFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ApplicationFile].[IX_ApplicationFile_ApplicationId_TypeId]...';


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationFile_ApplicationId_TypeId]
    ON [dbo].[ApplicationFile]([ApplicationId] ASC, [TypeId] ASC);


GO
PRINT N'Altering [dbo].[ClientContract_Merge]...';


GO
ALTER PROCEDURE [dbo].[ClientContract_Merge]
	@ClientId BIGINT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	MERGE	[dbo].[ClientContract] AS target

	USING	(VALUES (@ClientId, @Name, @Data))
			AS source ([ClientId], [Name], [Data])

		ON	target.[ClientId] = source.[ClientId]

	WHEN MATCHED AND source.[Name] IS NULL THEN
		DELETE

	WHEN MATCHED THEN
		UPDATE SET [Name] = Source.[Name], [Data] = source.[Data]

	WHEN NOT MATCHED THEN
		INSERT ([ClientId], [Name], [Data]) 
		VALUES (source.[ClientId], source.[Name], source.[Data]);
END
GO
PRINT N'Creating [dbo].[ApplicationFile_Add]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_Add]
	@ApplicationId BIGINT,
	@TypeId INT,	
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)
AS
BEGIN

	SET NOCOUNT ON;

	INSERT [dbo].[ApplicationFile] ([ApplicationId], [Data], [Name], [TypeId])
	OUTPUT INSERTED.[Id]
	VALUES (@ApplicationId, @Data, @Name, @TypeId)

END
GO
PRINT N'Creating [dbo].[ApplicationFile_Delete]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_Delete]
	@Id BIGINT
AS
BEGIN

	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[ApplicationFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[ApplicationFile_Get]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_Get]
	@Id BIGINT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT TOP(1) [Name], [Data]
	FROM [dbo].[ApplicationFile]
	WHERE [Id] = @Id

END
GO
PRINT N'Creating [dbo].[ApplicationFile_GetNames]...';


GO
CREATE PROCEDURE [dbo].[ApplicationFile_GetNames]
	@AppIds [dbo].[IdsTable] READONLY,
	@TypeId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT f.[Id], f.[Name], f.[ApplicationId]
	FROM [dbo].[ApplicationFile] f
	WHERE f.[ApplicationId] IN (SELECT [Id] FROM @AppIds)
	AND f.[TypeId] = @TypeId
	ORDER BY f.[ApplicationId], f.[Name], f.[Id]

END
GO
PRINT N'Update complete.';


GO
