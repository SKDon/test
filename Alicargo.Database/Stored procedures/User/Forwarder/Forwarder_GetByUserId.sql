CREATE PROCEDURE [dbo].[Forwarder_GetByUserId]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) [Id]
	FROM	[dbo].[Forwarder]
	WHERE	[UserId] = @UserId

END
GO