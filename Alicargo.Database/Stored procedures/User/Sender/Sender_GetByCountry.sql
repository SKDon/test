CREATE PROCEDURE [dbo].[Sender_GetByCountry]
	@CountryId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	s.[Id]
	FROM	[dbo].[Sender] s
	WHERE	s.[CountryId] = @CountryId

END
GO