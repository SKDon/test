CREATE PROCEDURE [dbo].[User_SetPassword]
	@UserId BIGINT,
	@Salt VARBINARY(8000),
	@Hash VARBINARY(8000)
AS
	SET NOCOUNT ON;

	UPDATE	TOP(1) [dbo].[User]
	SET		[PasswordHash] = @Hash,
			[PasswordSalt] = @Salt
	WHERE	[Id] = @UserId