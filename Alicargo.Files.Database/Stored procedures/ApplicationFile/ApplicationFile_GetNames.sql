CREATE PROCEDURE [dbo].[ApplicationFile_GetNames]
	@AppIds [dbo].[IdsTable] READONLY,
	@TypeId INT

AS BEGIN
	SET NOCOUNT ON

	SELECT f.[Id], f.[Name], f.[ApplicationId]
	FROM [dbo].[ApplicationFile] f
	WHERE f.[ApplicationId] IN (SELECT [Id] FROM @AppIds)
	AND f.[TypeId] = @TypeId
	ORDER BY f.[ApplicationId], f.[Name], f.[Id]

END
GO