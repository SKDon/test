CREATE PROCEDURE [dbo].[GetClientContract]
	@clientId BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP 1 c.[Name], c.[Data]
	FROM	[dbo].[ClientContract] c
	WHERE	c.[ClientId] = @clientId