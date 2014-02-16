﻿CREATE PROCEDURE [dbo].[Calculation_Add]
	@ClientId BIGINT,
	@ApplicationHistoryId BIGINT,
	@AirWaybillDisplay NVARCHAR(320),
	@ApplicationDisplay NVARCHAR(320),
	@MarkName NVARCHAR(320),
	@Weight REAL,
	@TariffPerKg MONEY,
	@ScotchCost MONEY,
	@InsuranceRate REAL,
	@InsuranceRateForClient REAL,
	@FactureCost MONEY,
	@FactureCostEx MONEY,
	@TransitCost MONEY,
	@PickupCost MONEY,
	@FactoryName NVARCHAR(320),
	@CreationTimestamp DATETIMEOFFSET,
	@ClassId INT,
	@Invoice NVARCHAR(MAX),
	@Value MONEY,
	@Count INT

AS BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Calculation]
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
		,[InsuranceRateForClient])
     VALUES
		(@ClientId
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
		,@InsuranceRateForClient)
END
GO