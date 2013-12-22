CREATE PROCEDURE [dbo].[User_GetLanguage]
	@UserId BIGINT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT TOP(1) [TwoLetterISOLanguageName]
	FROM [dbo].[User]
	WHERE [Id] = @UserId

END
GO