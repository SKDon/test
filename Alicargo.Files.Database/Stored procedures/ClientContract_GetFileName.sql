CREATE PROCEDURE [dbo].[ClientContract_GetFileName]
	@ClientId BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP 1 c.[Name]
	FROM	[dbo].[ClientContract] c
	WHERE	c.[ClientId] = @ClientId