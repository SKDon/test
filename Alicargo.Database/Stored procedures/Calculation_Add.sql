CREATE PROCEDURE [dbo].[Calculation_Add]
	@ClientId BIGINT,
	@ApplicationHistoryId BIGINT,
	@AirWaybillDisplay NVARCHAR(320),
    @ApplicationDisplay NVARCHAR(320),
    @MarkName NVARCHAR(320),
    @Weight REAL,
    @TariffPerKg MONEY,
    @ScotchCost MONEY,
    @InsuranceCost MONEY,
    @FactureCost MONEY,
    @TransitCost MONEY,
    @PickupCost MONEY,
    @FactoryName NVARCHAR(320)

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
           ,[InsuranceCost]
           ,[FactureCost]
           ,[TransitCost]
           ,[PickupCost]
           ,[FactoryName])
     VALUES
           (@ClientId
           ,@ApplicationHistoryId
           ,@AirWaybillDisplay
           ,@ApplicationDisplay
           ,@MarkName
           ,@Weight
           ,@TariffPerKg
           ,@ScotchCost
           ,@InsuranceCost
           ,@FactureCost
           ,@TransitCost
           ,@PickupCost
           ,@FactoryName)
END
GO