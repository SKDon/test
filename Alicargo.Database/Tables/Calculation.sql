CREATE TABLE [dbo].[Calculation]
(
	[Id]					BIGINT			NOT NULL PRIMARY KEY IDENTITY,
	[RowVersion]			ROWVERSION		NOT NULL,
	[StateId]				INT				NOT NULL,
	[StateIdTimestamp]		DATETIMEOFFSET	DEFAULT (GETDATE()) NOT NULL,

	[ClientId]				BIGINT			NOT NULL,
	[ApplicationHistoryId]	BIGINT			NOT NULL,
	[AirWaybillDisplay]		NVARCHAR(320)	NOT NULL,
	[ApplicationDisplay]	NVARCHAR(320)	NOT NULL,
	[MarkName]				NVARCHAR(320)	NOT NULL,
	[Weight]				NUMERIC(18, 4)	NOT NULL,
	[TariffPerKg]			SMALLMONEY		NOT NULL,
	[ScotchCost]			SMALLMONEY		NOT NULL,
	[InsuranceCost]			SMALLMONEY		NOT NULL,
	[FactureCost]			SMALLMONEY		NOT NULL,

	CONSTRAINT [FK_Calculation_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id])
)
GO

CREATE INDEX [IX_Calculation_StateId] ON [dbo].[Calculation] ([StateId])

GO

CREATE INDEX [IX_Calculation_ApplicationHistoryId] ON [dbo].[Calculation] ([ApplicationHistoryId])