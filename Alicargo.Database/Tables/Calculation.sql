CREATE TABLE [dbo].[Calculation]
(
	[Id]					BIGINT			NOT NULL PRIMARY KEY IDENTITY,
	[RowVersion]			ROWVERSION		NOT NULL,
	[StateId]				INT				NOT NULL,
	[StateIdTimestamp]		DATETIMEOFFSET	DEFAULT (GETUTCDATE()) NOT NULL,

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

	CONSTRAINT [FK_Calculation_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id])
)
GO

CREATE INDEX [IX_Calculation_StateId] ON [dbo].[Calculation] ([StateId])
GO

CREATE INDEX [IX_Calculation_ClientId] ON [dbo].[Calculation] ([ClientId])
GO

CREATE UNIQUE INDEX [IX_Calculation_ApplicationHistoryId] ON [dbo].[Calculation] ([ApplicationHistoryId])
GO