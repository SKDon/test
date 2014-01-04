CREATE TABLE [dbo].[Calculation]
(
	[Id]					BIGINT			NOT NULL IDENTITY(1, 1),

	[ClientId]				BIGINT			NOT NULL,
	[ApplicationHistoryId]	BIGINT			NOT NULL,
	[AirWaybillDisplay]		NVARCHAR(320)	NOT NULL,
	[ApplicationDisplay]	NVARCHAR(320)	NOT NULL,
	[MarkName]				NVARCHAR(320)	NOT NULL,
	[Weight]				REAL			NOT NULL,
	[TariffPerKg]			MONEY			NOT NULL,
	[ScotchCost]			MONEY			NOT NULL,
	[InsuranceCost]			MONEY			NOT NULL,
	[FactureCost]			MONEY			NOT NULL,
	[TransitCost]			MONEY			NOT NULL,
	[PickupCost]			MONEY			NOT NULL,
	[FactoryName]			NVARCHAR(320)	NOT NULL, 

	CONSTRAINT [PK_dbo.Calculation] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Calculation_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id])
)
GO

CREATE INDEX [IX_Calculation_ClientId] ON [dbo].[Calculation] ([ClientId])
GO

CREATE UNIQUE INDEX [IX_Calculation_ApplicationHistoryId] ON [dbo].[Calculation] ([ApplicationHistoryId])
GO