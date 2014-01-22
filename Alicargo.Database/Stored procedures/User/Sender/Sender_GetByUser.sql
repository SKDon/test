CREATE PROCEDURE [dbo].[Sender_GetByUser]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[Id]
	FROM	[dbo].[Sender] s
	WHERE	s.[UserId] = @UserId

END
GO