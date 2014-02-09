USE [$(MainDbName)]
GO

GO
PRINT N'Dropping [dbo].[AirWaybill].[IX_AirWaybill_Bill]...';


GO
DROP INDEX [IX_AirWaybill_Bill]
    ON [dbo].[AirWaybill];


GO
PRINT N'Dropping DF_AirWaybill_CreationTimestamp...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF_AirWaybill_CreationTimestamp];


GO
PRINT N'Dropping DF_AirWaybill_StateChangeTimestamp...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF_AirWaybill_StateChangeTimestamp];


GO
PRINT N'Dropping DF_StateEmailTemplate_EnableEmailSend...';


GO
ALTER TABLE [dbo].[StateEmailTemplate] DROP CONSTRAINT [DF_StateEmailTemplate_EnableEmailSend];


GO
PRINT N'Dropping DF_StateEmailTemplate_UseEventTemplate...';


GO
ALTER TABLE [dbo].[StateEmailTemplate] DROP CONSTRAINT [DF_StateEmailTemplate_UseEventTemplate];


GO
PRINT N'Dropping FK_dbo.AirWaybill_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId];


GO
PRINT N'Dropping FK_dbo.AirWaybill_dbo.Broker_BrokerId...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [FK_dbo.AirWaybill_dbo.Broker_BrokerId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.AirWaybill_AirWaybillId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId];


GO
PRINT N'Dropping FK_dbo.StateEmailRecipient_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[StateEmailRecipient] DROP CONSTRAINT [FK_dbo.StateEmailRecipient_dbo.State_StateId];


GO
PRINT N'Dropping FK_dbo.StateEmailTemplate_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[StateEmailTemplate] DROP CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.State_StateId];


GO
PRINT N'Dropping FK_dbo.StateEmailTemplate_dbo.EmailTemplate_EmailTemplateId...';


GO
ALTER TABLE [dbo].[StateEmailTemplate] DROP CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.EmailTemplate_EmailTemplateId];


GO
PRINT N'Dropping [dbo].[EmailTemplate_GetByStateId]...';


GO
DROP PROCEDURE [dbo].[EmailTemplate_GetByStateId];


GO
PRINT N'Dropping [dbo].[EmailTemplate_MergeState]...';


GO
DROP PROCEDURE [dbo].[EmailTemplate_MergeState];


GO
PRINT N'Dropping [dbo].[State_GetStateEmailRecipients]...';


GO
DROP PROCEDURE [dbo].[State_GetStateEmailRecipients];


GO
PRINT N'Dropping [dbo].[State_SetStateEmailRecipients]...';


GO
DROP PROCEDURE [dbo].[State_SetStateEmailRecipients];


GO
PRINT N'Dropping [dbo].[StateEmailRecipient]...';


GO
DROP TABLE [dbo].[StateEmailRecipient];


GO
PRINT N'Dropping [dbo].[StateEmailTemplate]...';


GO
DROP TABLE [dbo].[StateEmailTemplate];


GO
PRINT N'Starting rebuilding table [dbo].[AirWaybill]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AirWaybill] (
    [Id]                         BIGINT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp]          DATETIMEOFFSET (7) CONSTRAINT [DF_AirWaybill_CreationTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,
    [ArrivalAirport]             NVARCHAR (MAX)     NOT NULL,
    [DepartureAirport]           NVARCHAR (MAX)     NOT NULL,
    [DateOfDeparture]            DATETIMEOFFSET (7) NOT NULL,
    [DateOfArrival]              DATETIMEOFFSET (7) NOT NULL,
    [BrokerId]                   BIGINT             NOT NULL,
    [GTD]                        NVARCHAR (320)     NULL,
    [Bill]                       NVARCHAR (320)     NOT NULL,
    [GTDFileData]                VARBINARY (MAX)    NULL,
    [GTDFileName]                NVARCHAR (320)     NULL,
    [GTDAdditionalFileData]      VARBINARY (MAX)    NULL,
    [GTDAdditionalFileName]      NVARCHAR (320)     NULL,
    [PackingFileData]            VARBINARY (MAX)    NULL,
    [PackingFileName]            NVARCHAR (320)     NULL,
    [InvoiceFileData]            VARBINARY (MAX)    NULL,
    [InvoiceFileName]            NVARCHAR (320)     NULL,
    [AWBFileData]                VARBINARY (MAX)    NULL,
    [AWBFileName]                NVARCHAR (320)     NULL,
    [DrawFileData]               VARBINARY (MAX)    NULL,
    [DrawFileName]               NVARCHAR (320)     NULL,
    [StateId]                    BIGINT             NOT NULL,
    [StateChangeTimestamp]       DATETIMEOFFSET (7) CONSTRAINT [DF_AirWaybill_StateChangeTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,
    [FlightCost]                 MONEY              NULL,
    [CustomCost]                 MONEY              NULL,
    [BrokerCost]                 MONEY              NULL,
    [AdditionalCost]             MONEY              NULL,
    [TotalCostOfSenderForWeight] MONEY              NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.AirWaybill] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AirWaybill])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AirWaybill] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AirWaybill] ([Id], [CreationTimestamp], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [Bill], [GTDFileData], [GTDFileName], [GTDAdditionalFileData], [GTDAdditionalFileName], [PackingFileData], [PackingFileName], [InvoiceFileData], [InvoiceFileName], [AWBFileData], [AWBFileName], [StateId], [StateChangeTimestamp], [FlightCost], [CustomCost], [BrokerCost], [AdditionalCost], [TotalCostOfSenderForWeight])
        SELECT   [Id],
                 [CreationTimestamp],
                 [ArrivalAirport],
                 [DepartureAirport],
                 [DateOfDeparture],
                 [DateOfArrival],
                 [BrokerId],
                 [GTD],
                 [Bill],
                 [GTDFileData],
                 [GTDFileName],
                 [GTDAdditionalFileData],
                 [GTDAdditionalFileName],
                 [PackingFileData],
                 [PackingFileName],
                 [InvoiceFileData],
                 [InvoiceFileName],
                 [AWBFileData],
                 [AWBFileName],
                 [StateId],
                 [StateChangeTimestamp],
                 [FlightCost],
                 [CustomCost],
                 [BrokerCost],
                 [AdditionalCost],
                 [TotalCostOfSenderForWeight]
        FROM     [dbo].[AirWaybill]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AirWaybill] OFF;
    END

DROP TABLE [dbo].[AirWaybill];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AirWaybill]', N'AirWaybill';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.AirWaybill]', N'PK_dbo.AirWaybill', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AirWaybill].[IX_BrokerId]...';


GO
CREATE NONCLUSTERED INDEX [IX_BrokerId]
    ON [dbo].[AirWaybill]([BrokerId] ASC);


GO
PRINT N'Creating FK_dbo.AirWaybill_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[AirWaybill] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);


GO
PRINT N'Creating FK_dbo.AirWaybill_dbo.Broker_BrokerId...';


GO
ALTER TABLE [dbo].[AirWaybill] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AirWaybill_dbo.Broker_BrokerId] FOREIGN KEY ([BrokerId]) REFERENCES [dbo].[Broker] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.AirWaybill_AirWaybillId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId] FOREIGN KEY ([AirWaybillId]) REFERENCES [dbo].[AirWaybill] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[AirWaybill] WITH CHECK CHECK CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId];

ALTER TABLE [dbo].[AirWaybill] WITH CHECK CHECK CONSTRAINT [FK_dbo.AirWaybill_dbo.Broker_BrokerId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId];


GO
PRINT N'Update complete.';


GO
