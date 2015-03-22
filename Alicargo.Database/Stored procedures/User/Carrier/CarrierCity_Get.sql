CREATE PROCEDURE [dbo].[CarrierCity_Get]
	@CarrierId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [CityId]
	FROM [dbo].[CarrierCity]
	WHERE [CarrierId] = @CarrierId

END
GO