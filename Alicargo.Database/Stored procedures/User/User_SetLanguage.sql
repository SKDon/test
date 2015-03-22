CREATE PROCEDURE [dbo].[User_SetLanguage]
	@UserId BIGINT,
	@Language CHAR(2)

AS BEGIN
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[User]
	SET [TwoLetterISOLanguageName] = @Language
	WHERE [Id] = @UserId

END
GO