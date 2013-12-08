CREATE PROCEDURE [dbo].[ApplicationFile_GetNames]
	@ApplicationId BIGINT,
	@TypeId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT f.[Id], f.[Name]
	FROM [dbo].[ApplicationFile] f
	WHERE f.[ApplicationId] = @ApplicationId
	AND f.[TypeId] = @TypeId
	ORDER BY f.[Name]

END
GO