﻿CREATE TABLE [dbo].[AirWaybill] (
	[Id]							BIGINT				IDENTITY (1, 1) NOT NULL,
	[CreationTimestamp]				DATETIMEOFFSET		CONSTRAINT [DF_AirWaybill_CreationTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,	

	[ArrivalAirport]				NVARCHAR (MAX)		NOT NULL,
	[DepartureAirport]				NVARCHAR (MAX)		NOT NULL,
	[DateOfDeparture]				DATETIMEOFFSET		NOT NULL,
	[DateOfArrival]					DATETIMEOFFSET		NOT NULL,

	[BrokerId]						BIGINT				NULL,
	[GTD]							NVARCHAR (320)		NULL,
	[Bill]							NVARCHAR (320)		NOT NULL,

	[StateId]						BIGINT				NOT NULL,
	[StateChangeTimestamp]			DATETIMEOFFSET		CONSTRAINT [DF_AirWaybill_StateChangeTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,

	[FlightCost]					MONEY				NULL,
	[CustomCost]					MONEY				NULL,
	[BrokerCost]					MONEY				NULL,	
	[AdditionalCost]				MONEY				NULL,
	[TotalCostOfSenderForWeight]	MONEY				NULL,

	CONSTRAINT [PK_dbo.AirWaybill] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.AirWaybill_dbo.Broker_BrokerId] FOREIGN KEY ([BrokerId]) REFERENCES [dbo].[Broker] ([Id]),
	CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id])
);
GO

CREATE NONCLUSTERED INDEX [IX_BrokerId] ON [dbo].[AirWaybill]([BrokerId] ASC);
GO