CREATE PROCEDURE [dbo].[User_SetPassword]
	@UserId BIGINT,
	@Salt VARBINARY(MAX),
	@Hash VARBINARY(MAX)
AS
	SET NOCOUNT ON;

	UPDATE	TOP(1) [dbo].[User]
	SET		[PasswordHash] = @Hash,
			[PasswordSalt] = @Salt
	WHERE	[Id] = @UserId