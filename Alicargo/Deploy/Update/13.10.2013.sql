use Alicargo_Dev
go

PRINT N'Dropping DF_Application_CreationTimestamp...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [DF_Application_CreationTimestamp];


GO
PRINT N'Dropping DF_ReferenceCreationTimestamp...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF_ReferenceCreationTimestamp];


GO
PRINT N'Dropping DF__Reference__State__6A30C649...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF__Reference__State__6A30C649];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Client_ClientId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Reference_ReferenceId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Reference_ReferenceId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.State_StateId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Transit_TransitId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Country_CountryId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId];


GO
PRINT N'Altering [dbo].[AirWaybill]...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP COLUMN [ForwarderCost];


GO
ALTER TABLE [dbo].[AirWaybill]
    ADD [TotalCostOfSenderForWeight] MONEY NULL;


GO
PRINT N'Starting rebuilding table [dbo].[Application]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Application] (
    [Id]                   BIGINT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp]    DATETIMEOFFSET (7) CONSTRAINT [DF_Application_CreationTimestamp] DEFAULT (getutcdate()) NOT NULL,
    [Invoice]              NVARCHAR (MAX)     NOT NULL,
    [InvoiceFileData]      VARBINARY (MAX)    NULL,
    [InvoiceFileName]      NVARCHAR (MAX)     NULL,
    [SwiftFileData]        VARBINARY (MAX)    NULL,
    [SwiftFileName]        NVARCHAR (MAX)     NULL,
    [DeliveryBillFileData] VARBINARY (MAX)    NULL,
    [DeliveryBillFileName] NVARCHAR (MAX)     NULL,
    [Torg12FileData]       VARBINARY (MAX)    NULL,
    [Torg12FileName]       NVARCHAR (MAX)     NULL,
    [CPFileData]           VARBINARY (MAX)    NULL,
    [CPFileName]           NVARCHAR (MAX)     NULL,
    [PackingFileData]      VARBINARY (MAX)    NULL,
    [PackingFileName]      NVARCHAR (320)     NULL,
    [Characteristic]       NVARCHAR (MAX)     NULL,
    [AddressLoad]          NVARCHAR (MAX)     NULL,
    [WarehouseWorkingTime] NVARCHAR (MAX)     NULL,
    [TransitReference]     NVARCHAR (MAX)     NULL,
    [Weight]               REAL               NULL,
    [Count]                INT                NULL,
    [Volume]               REAL               NOT NULL,
    [TermsOfDelivery]      NVARCHAR (MAX)     NULL,
    [MethodOfDeliveryId]   INT                NOT NULL,
    [DateOfCargoReceipt]   DATETIMEOFFSET (7) NULL,
    [DateInStock]          DATETIMEOFFSET (7) NULL,
    [StateChangeTimestamp] DATETIMEOFFSET (7) NOT NULL,
    [StateId]              BIGINT             NOT NULL,
    [Value]                MONEY              NOT NULL,
    [CurrencyId]           INT                NOT NULL,
    [ClassId]              INT                NULL,
    [Certificate]          NVARCHAR (MAX)     NULL,
    [ClientId]             BIGINT             NOT NULL,
    [TransitId]            BIGINT             NOT NULL,
    [CountryId]            BIGINT             NULL,
    [AirWaybillId]         BIGINT             NULL,
    [SenderId]             BIGINT             NULL,
    [FactoryName]          NVARCHAR (320)     NOT NULL,
    [FactoryPhone]         NVARCHAR (MAX)     NULL,
    [FactoryContact]       NVARCHAR (MAX)     NULL,
    [FactoryEmail]         NVARCHAR (320)     NULL,
    [MarkName]             NVARCHAR (320)     NOT NULL,
    [FactureCost]          MONEY              NULL,
    [ScotchCost]           MONEY              NULL,
    [WithdrawCost]         MONEY              NULL,
    [FactureCostEdited]    MONEY              NULL,
    [ScotchCostEdited]     MONEY              NULL,
    [WithdrawCostEdited]   MONEY              NULL,
    [TariffPerKg]          MONEY              NULL,
    [SenderRate]           MONEY              NULL,
    [TransitCost]          MONEY              NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Application] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Application])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Application] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Weight], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName])
        SELECT   [Id],
                 [CreationTimestamp],
                 [Invoice],
                 [InvoiceFileData],
                 [InvoiceFileName],
                 [SwiftFileData],
                 [SwiftFileName],
                 [DeliveryBillFileData],
                 [DeliveryBillFileName],
                 [Torg12FileData],
                 [Torg12FileName],
                 [CPFileData],
                 [CPFileName],
                 [PackingFileData],
                 [PackingFileName],
                 [Characteristic],
                 [AddressLoad],
                 [WarehouseWorkingTime],
                 [TransitReference],
                 [Weight],
                 [Count],
                 [Volume],
                 [TermsOfDelivery],
                 [MethodOfDeliveryId],
                 [DateOfCargoReceipt],
                 [DateInStock],
                 [StateChangeTimestamp],
                 [StateId],
                 [Value],
                 [CurrencyId],
                 [ClientId],
                 [TransitId],
                 [CountryId],
                 [AirWaybillId],
                 [FactoryName],
                 [FactoryPhone],
                 [FactoryContact],
                 [FactoryEmail],
                 [MarkName]
        FROM     [dbo].[Application]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Application] OFF;
    END

DROP TABLE [dbo].[Application];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Application]', N'Application';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Application]', N'PK_dbo.Application', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Application].[IX_ClientId]...';


GO
CREATE NONCLUSTERED INDEX [IX_ClientId]
    ON [dbo].[Application]([ClientId] ASC);


GO
PRINT N'Creating [dbo].[Application].[IX_StateId]...';


GO
CREATE NONCLUSTERED INDEX [IX_StateId]
    ON [dbo].[Application]([StateId] ASC);


GO
PRINT N'Creating [dbo].[Application].[IX_Application_TransitId]...';


GO
CREATE NONCLUSTERED INDEX [IX_Application_TransitId]
    ON [dbo].[Application]([TransitId] ASC);


GO
PRINT N'Creating [dbo].[Application].[IX_AirWaybillId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AirWaybillId]
    ON [dbo].[Application]([AirWaybillId] ASC);


GO
PRINT N'Altering [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD [ContractFileData]    VARBINARY (MAX) NULL,
        [ContractFileName]    NVARCHAR (MAX)  NULL,
        [CalculationFileData] VARBINARY (MAX) NULL;


GO
PRINT N'Creating [dbo].[Calculation]...';


GO
CREATE TABLE [dbo].[Calculation] (
    [Id]                   BIGINT             IDENTITY (1, 1) NOT NULL,
    [RowVersion]           ROWVERSION         NOT NULL,
    [StateId]              INT                NOT NULL,
    [StateIdTimestamp]     DATETIMEOFFSET (7) NOT NULL,
    [ClientId]             BIGINT             NOT NULL,
    [ApplicationHistoryId] BIGINT             NOT NULL,
    [AirWaybillDisplay]    NVARCHAR (320)     NOT NULL,
    [ApplicationDisplay]   NVARCHAR (320)     NOT NULL,
    [MarkName]             NVARCHAR (320)     NOT NULL,
    [Weight]               REAL               NOT NULL,
    [TariffPerKg]          MONEY              NOT NULL,
    [ScotchCost]           MONEY              NOT NULL,
    [InsuranceCost]        MONEY              NOT NULL,
    [FactureCost]          MONEY              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Calculation].[IX_Calculation_StateId]...';


GO
CREATE NONCLUSTERED INDEX [IX_Calculation_StateId]
    ON [dbo].[Calculation]([StateId] ASC);


GO
PRINT N'Creating [dbo].[Calculation].[IX_Calculation_Unique]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Calculation_Unique]
    ON [dbo].[Calculation]([ClientId] ASC, [ApplicationHistoryId] ASC);


GO
PRINT N'Creating DF_AirWaybillCreationTimestamp...';


GO
ALTER TABLE [dbo].[AirWaybill]
    ADD CONSTRAINT [DF_AirWaybillCreationTimestamp] DEFAULT (getutcdate()) FOR [CreationTimestamp];


GO
PRINT N'Creating DF__AirWaybil__State__1ED998B2...';


GO
ALTER TABLE [dbo].[AirWaybill]
    ADD DEFAULT (getutcdate()) FOR [StateChangeTimestamp];


GO
PRINT N'Creating DF__Calculati__State__20C1E124...';


GO
ALTER TABLE [dbo].[Calculation]
    ADD DEFAULT (getutcdate()) FOR [StateIdTimestamp];


GO
PRINT N'Creating FK_dbo.Application_dbo.Client_ClientId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.AirWaybill_AirWaybillId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId] FOREIGN KEY ([AirWaybillId]) REFERENCES [dbo].[AirWaybill] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Transit_TransitId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId] FOREIGN KEY ([TransitId]) REFERENCES [dbo].[Transit] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Country_CountryId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]);


GO
PRINT N'Creating FK_Calculation_Client...';


GO
ALTER TABLE [dbo].[Calculation] WITH NOCHECK
    ADD CONSTRAINT [FK_Calculation_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating [dbo].[Calculation_SetState]...';


GO
CREATE PROCEDURE [dbo].[Calculation_SetState]
	@Id	BIGINT,
	@RowVersion ROWVERSION,
	@State INT
AS
	SET NOCOUNT ON;

	UPDATE	[dbo].[Calculation]
	SET		[StateId] = @State,
			[StateIdTimestamp] = GETUTCDATE()
	OUTPUT	[INSERTED].[RowVersion], [INSERTED].[StateIdTimestamp] AS [StateTimestamp]
	WHERE	[Id] = @Id AND [RowVersion] = @RowVersion
GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.Client_DeleteForce';


GO
PRINT N'Checking existing data against newly created constraints';



GO
ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.State_StateId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId];

ALTER TABLE [dbo].[Calculation] WITH CHECK CHECK CONSTRAINT [FK_Calculation_Client];


GO
PRINT N'Update complete.'
GO

use master