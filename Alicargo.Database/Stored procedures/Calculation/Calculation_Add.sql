CREATE PROCEDURE [dbo].[Calculation_Add]
	@ClientId BIGINT,
	@ApplicationHistoryId BIGINT,
	@AirWaybillDisplay NVARCHAR(320),
	@ApplicationDisplay NVARCHAR(320),
	@MarkName NVARCHAR(320),
	@Weight REAL,
	@TariffPerKg MONEY,
	@ScotchCost MONEY,
	@InsuranceRate REAL,
	@FactureCost MONEY,
	@FactureCostEx MONEY,
	@TransitCost MONEY,
	@PickupCost MONEY,
	@FactoryName NVARCHAR(320),
	@CreationTimestamp DATETIMEOFFSET,
	@ClassId INT,
	@Invoice NVARCHAR(MAX),
	@Value MONEY,
	@Count INT,
	@TotalTariffCost MONEY,
	@Profit MONEY

AS BEGIN
	SET NOCOUNT ON;

	INSERT	[dbo].[Calculation]
			([ClientId]
			,[ApplicationHistoryId]
			,[AirWaybillDisplay]
			,[ApplicationDisplay]
			,[MarkName]
			,[Weight]
			,[TariffPerKg]
			,[ScotchCost]
			,[FactureCost]
			,[FactureCostEx]
			,[TransitCost]
			,[PickupCost]
			,[FactoryName]
			,[CreationTimestamp]
			,[ClassId]
			,[Invoice]
			,[Value]
			,[Count]
			,[InsuranceRate]
			,[TotalTariffCost]
			,[Profit])
     VALUES	(@ClientId
			,@ApplicationHistoryId
			,@AirWaybillDisplay
			,@ApplicationDisplay
			,@MarkName
			,@Weight
			,@TariffPerKg
			,@ScotchCost
			,@FactureCost
			,@FactureCostEx
			,@TransitCost
			,@PickupCost
			,@FactoryName
			,@CreationTimestamp
			,@ClassId
			,@Invoice
			,@Value
			,@Count
			,@InsuranceRate
			,@TotalTariffCost
			,@Profit)

END
GO