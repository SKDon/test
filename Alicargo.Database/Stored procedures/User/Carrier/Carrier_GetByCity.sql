CREATE PROCEDURE [dbo].[Carrier_GetByCity]
	@CityId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT s.[CarrierId]
	FROM [dbo].[CarrierCity] s
	WHERE s.[CityId] = @CityId

END
GO