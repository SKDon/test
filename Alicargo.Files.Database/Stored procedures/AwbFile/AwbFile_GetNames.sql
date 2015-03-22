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