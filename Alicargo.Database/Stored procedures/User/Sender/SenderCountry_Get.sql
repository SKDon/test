CREATE PROCEDURE [dbo].[SenderCountry_Get]
	@SenderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [CountryId]
	FROM [dbo].[SenderCountry]
	WHERE [SenderId] = @SenderId

END
GO