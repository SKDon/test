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
			[FactureCost],
			[FactureCostEx],
			[TransitCost],
			[PickupCost],
			[FactoryName],
			[CreationTimestamp],
			[ClassId] AS [Class],
			[Invoice],
			[Value],
			[Count],
			[InsuranceRate],
			[TotalTariffCost],
			[Profit]
	FROM [dbo].[Calculation]
	WHERE [ClientId] = @ClientId

END
GO