CREATE PROCEDURE [dbo].[User_SetLogin]
	@UserId BIGINT,
	@Login NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[User]
	SET [Login] = @Login
	WHERE [Id] = @UserId

END
GO