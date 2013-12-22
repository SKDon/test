CREATE PROCEDURE [dbo].[User_GetPasswordData]
	@Login NVARCHAR(320)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT TOP(1) u.[PasswordHash], u.[PasswordSalt], u.[Id] AS [UserId]
	FROM [dbo].[User] u
	WHERE u.[Login] = @Login

END
GO