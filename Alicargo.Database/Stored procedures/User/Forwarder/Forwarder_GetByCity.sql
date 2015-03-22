CREATE PROCEDURE [dbo].[Forwarder_GetByCity]
	@CityId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT s.[ForwarderId]
	FROM [dbo].[ForwarderCity] s
	WHERE s.[CityId] = @CityId

END
GO