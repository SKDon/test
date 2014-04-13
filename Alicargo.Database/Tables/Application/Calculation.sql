CREATE TABLE [dbo].[Calculation]
(
	[Id]					BIGINT			NOT NULL IDENTITY(1, 1),
	[CreationTimestamp]		DATETIMEOFFSET	NOT NULL,

	[ClientId]				BIGINT			NOT NULL,
	[ApplicationHistoryId]	BIGINT			NOT NULL,
	[AirWaybillDisplay]		NVARCHAR(320)	NOT NULL,
	[ApplicationDisplay]	NVARCHAR(320)	NOT NULL,
	[MarkName]				NVARCHAR(320)	NOT NULL,
	[Weight]				REAL			NOT NULL,
	[TariffPerKg]			MONEY			NOT NULL,
	[ScotchCost]			MONEY			NOT NULL,	
	[FactureCost]			MONEY			NOT NULL,	
	[TransitCost]			MONEY			NOT NULL,
	[PickupCost]			MONEY			NOT NULL,
	[FactoryName]			NVARCHAR(320)	NOT NULL,
	[ClassId]				INT					NULL,
	[Invoice]				NVARCHAR (MAX)	NOT NULL,
	[Value]					MONEY			NOT NULL,
	[Count]					INT					NULL,
	[FactureCostEx]			MONEY			NOT NULL,
	[InsuranceRate]			REAL			NOT NULL CONSTRAINT [DF_Calculation_InsuranceRate] DEFAULT 0.01,
	[TotalTariffCost]		MONEY				NULL,
	[Profit]				MONEY				NULL,

	CONSTRAINT [PK_dbo.Calculation] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Calculation_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id])
)
GO

CREATE INDEX [IX_Calculation_ClientId] ON [dbo].[Calculation] ([ClientId])
GO

CREATE UNIQUE INDEX [IX_Calculation_ApplicationHistoryId] ON [dbo].[Calculation] ([ApplicationHistoryId])
GO