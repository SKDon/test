CREATE PROCEDURE [dbo].[Calculation_GetByClient]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	[ClientId],
			[AirWaybillDisplay],
			[ApplicationDisplay],
			[MarkName],
			[Weight],
			[TariffPerKg],
			[ScotchCost],
			[InsuranceCost],
			[FactureCost],
			[TransitCost],
			[PickupCost],
			[FactoryName],
			[CreationTimestamp]
	FROM [dbo].[Calculation]
	WHERE [ClientId] = @ClientId

END
GO