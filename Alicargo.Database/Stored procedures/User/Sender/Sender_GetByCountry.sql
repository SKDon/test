CREATE PROCEDURE [dbo].[Sender_GetByCountry]
	@CountryId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT s.[SenderId]
	FROM [dbo].[SenderCountry] s
	WHERE s.[CountryId] = @CountryId

END
GO