CREATE PROCEDURE [dbo].[Carrier_GetByUserId]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) [Id]
	FROM	[dbo].[Carrier]
	WHERE	[UserId] = @UserId

END
GO