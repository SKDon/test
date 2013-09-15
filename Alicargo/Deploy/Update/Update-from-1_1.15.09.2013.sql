GO
ALTER TABLE [dbo].[AirWaybill] DROP COLUMN [ForwarderCost];


GO
ALTER TABLE [dbo].[AirWaybill]
    ADD [TotalCostOfSenderForWeight] MONEY NULL;


GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application]
    ADD [FactureCost]        MONEY NULL,
        [ScotchCost]         MONEY NULL,
        [WithdrawCost]       MONEY NULL,
        [FactureCostEdited]  MONEY NULL,
        [ScotchCostEdited]   MONEY NULL,
        [WithdrawCostEdited] MONEY NULL,
        [ForwarderCost]      MONEY NULL,
        [TariffPerKg]        MONEY NULL,
        [TransitCost]        MONEY NULL;


GO
PRINT N'Altering [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD [ContractFileData] VARBINARY (MAX) NULL,
        [ContractFileName] NVARCHAR (MAX)  NULL;


GO
PRINT N'Update complete.'
GO