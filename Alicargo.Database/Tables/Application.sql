﻿CREATE TABLE [dbo].[Application] (
	[Id]					BIGINT				IDENTITY (1, 1) NOT NULL,
	[CreationTimestamp]		DATETIMEOFFSET (7)	CONSTRAINT [DF_Application_CreationTimestamp] DEFAULT (getdate()) NOT NULL,
	
	[Invoice]				NVARCHAR (MAX)		NOT NULL,
	[InvoiceFileData]		VARBINARY (MAX)		NULL,
	[InvoiceFileName]		NVARCHAR (MAX)		NULL,

	[SwiftFileData]			VARBINARY (MAX)		NULL,
	[SwiftFileName]			NVARCHAR (MAX)		NULL,

	[DeliveryBillFileData]	VARBINARY (MAX)		NULL,
	[DeliveryBillFileName]	NVARCHAR (MAX)		NULL,

	[Torg12FileData]		VARBINARY (MAX)		NULL,
	[Torg12FileName]		NVARCHAR (MAX)		NULL,

	[CPFileData]			VARBINARY (MAX)		NULL,
	[CPFileName]			NVARCHAR (MAX)		NULL,

	[PackingFileData]		VARBINARY (MAX)		NULL,
	[PackingFileName]		NVARCHAR(320)		NULL,

	[Characteristic]		NVARCHAR (MAX)		NULL,
	[AddressLoad]			NVARCHAR (MAX)		NULL,
	[WarehouseWorkingTime]	NVARCHAR (MAX)		NULL,
	[TransitReference]		NVARCHAR (MAX)		NULL,
	[Weight]				NUMERIC(18, 4)		NULL,
	[Count]					INT					NULL,
	[Volume]				NUMERIC(18, 4)		NOT NULL,
	[TermsOfDelivery]		NVARCHAR (MAX)		NULL,
	[MethodOfDeliveryId]	INT					NOT NULL,
	[DateOfCargoReceipt]	DATETIMEOFFSET (7)	NULL,
	[DateInStock]			DATETIMEOFFSET (7)	NULL,

	[StateChangeTimestamp]	DATETIMEOFFSET (7)	NOT NULL,
	[StateId]				BIGINT				NOT NULL,
	
	[Value]					SMALLMONEY			NOT NULL,
	[CurrencyId]			INT					NOT NULL, 

	[ClassId]				INT					NULL,
	[Certificate]			NVARCHAR(MAX)		NULL, 

	[ClientId]				BIGINT				NOT NULL,
	[TransitId]				BIGINT				NOT NULL,
	[CountryId]				BIGINT				NULL,
	[AirWaybillId]			BIGINT				NULL,
	[SenderId]				BIGINT				NULL,

	[FactoryName]			NVARCHAR(320)		NOT NULL, 
	[FactoryPhone]			NVARCHAR(MAX)		NULL, 
	[FactoryContact]		NVARCHAR(MAX)		NULL,
	[FactoryEmail]			NVARCHAR(320)		NULL, 
	[MarkName]				NVARCHAR(320)		NOT NULL, 

	[FactureCost]			SMALLMONEY			NULL,
	[ScotchCost]			SMALLMONEY			NULL,
	[WithdrawCost]			SMALLMONEY			NULL,
	[FactureCostEdited]		SMALLMONEY			NULL,
	[ScotchCostEdited]		SMALLMONEY			NULL,
	[WithdrawCostEdited]	SMALLMONEY			NULL,
	[ForwarderCost]			SMALLMONEY			NULL,
	[TariffPerKg]			SMALLMONEY			NULL,
	[SenderRate]			SMALLMONEY			NULL,
	[TransitCost]			SMALLMONEY			NULL,
	
	CONSTRAINT [PK_dbo.Application] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]),
	CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId] FOREIGN KEY ([AirWaybillId]) REFERENCES [dbo].[AirWaybill] ([Id]),
	CONSTRAINT [FK_dbo.Application_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]),
	CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId] FOREIGN KEY ([TransitId]) REFERENCES [dbo].[Transit] ([Id]),
	CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientId]
    ON [dbo].[Application]([ClientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateId]
    ON [dbo].[Application]([StateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Application_TransitId]
    ON [dbo].[Application]([TransitId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AirWaybillId]
    ON [dbo].[Application]([AirWaybillId] ASC);