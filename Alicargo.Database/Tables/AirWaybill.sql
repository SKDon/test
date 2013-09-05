CREATE TABLE [dbo].[AirWaybill] (
	[Id]				BIGINT				IDENTITY (1, 1) NOT NULL,
	[CreationTimestamp]	DATETIMEOFFSET (7)	CONSTRAINT [DF_AirWaybillCreationTimestamp] DEFAULT (GETDATE()) NOT NULL,	

	[ArrivalAirport]	NVARCHAR (MAX)		NOT NULL,
	[DepartureAirport]	NVARCHAR (MAX)		NOT NULL,
	[DateOfDeparture]	DATETIMEOFFSET (7)	NOT NULL,
	[DateOfArrival]		DATETIMEOFFSET (7)	NOT NULL,

	[BrockerId]			BIGINT				NOT NULL,
	[GTD]				NVARCHAR (320)		NULL,
	[Bill]				NVARCHAR (320)		NOT NULL,

	[GTDFileData]		VARBINARY (MAX)		NULL,
	[GTDFileName]		NVARCHAR(320)		NULL,
	[GTDAdditionalFileData]	VARBINARY (MAX)	NULL,
	[GTDAdditionalFileName]	NVARCHAR(320)	NULL,
	[PackingFileData]	VARBINARY (MAX)		NULL,
	[PackingFileName]	NVARCHAR(320)		NULL,
	[InvoiceFileData]	VARBINARY (MAX)		NULL,
	[InvoiceFileName]	NVARCHAR(320)		NULL,
	[AWBFileData]		VARBINARY (MAX)		NULL,
	[AWBFileName]		NVARCHAR(320)		NULL,

	[StateId]			BIGINT				NOT NULL,
	[StateChangeTimestamp] DATETIMEOFFSET(7) DEFAULT (GETDATE()) NOT NULL,

	[FlightCost]		MONEY				NULL,
	[CustomCost]		MONEY				NULL,
	[BrokerCost]		MONEY				NULL,
	[ForwarderCost]		MONEY				NULL,
	[AdditionalCost]	MONEY				NULL,

	CONSTRAINT [PK_dbo.AirWaybill] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.AirWaybill_dbo.Brocker_BrockerId] FOREIGN KEY ([BrockerId]) REFERENCES [dbo].[Brocker] ([Id]),
	CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BrockerId]
	ON [dbo].[AirWaybill]([BrockerId] ASC);

GO

CREATE UNIQUE INDEX [IX_AirWaybill_Bill] ON [dbo].[AirWaybill] ([Bill])
