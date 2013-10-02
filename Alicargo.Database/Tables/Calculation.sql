CREATE TABLE [dbo].[Calculation]
(
	[Id]					BIGINT			NOT NULL PRIMARY KEY IDENTITY, 
    [CliendId]				BIGINT			NOT NULL, 
    [AirWaybillDisplay]		NVARCHAR(320)	NOT NULL, 
    [ApplicationDisplay]	NVARCHAR(320)	NOT NULL, 
    [MarkName]				NVARCHAR(320)	NOT NULL, 
	[Weight]				NUMERIC(18, 4)	NOT NULL,
	[TariffPerKg]			SMALLMONEY		NOT NULL,
	[ScotchCost]			SMALLMONEY		NOT NULL,
	[InsuranceCost]			SMALLMONEY		NOT NULL,
	[FactureCost]			SMALLMONEY		NOT NULL,
	[State]					INT				NOT NULL
)