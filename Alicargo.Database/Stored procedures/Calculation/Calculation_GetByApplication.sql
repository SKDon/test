CREATE PROCEDURE [dbo].[Calculation_GetByApplication]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		[ClientId],
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
		[CreationTimestamp],
		[ClassId] AS [Class],
		[Invoice],
		[Value],
		[Count]
	FROM [dbo].[Calculation]
	WHERE [ApplicationHistoryId] = @ApplicationId

END
GO