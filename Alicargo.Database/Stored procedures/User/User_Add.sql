CREATE PROCEDURE [dbo].[User_Add]
	@Login						NVARCHAR(320),
	@PasswordHash				VARBINARY(MAX),
	@PasswordSalt				VARBINARY(MAX),
	@TwoLetterISOLanguageName	CHAR(2)

AS BEGIN
	SET NOCOUNT ON;

	INSERT	[dbo].[User] ([Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
	OUTPUT	INSERTED.[Id]
	VALUES	(@Login, @PasswordHash, @PasswordSalt, @TwoLetterISOLanguageName)

END
GO