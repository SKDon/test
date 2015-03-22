CREATE TABLE [dbo].[Transit] 
(
	[Id]					BIGINT			IDENTITY (1, 1) NOT NULL,
	[Address]				NVARCHAR (MAX)	NOT NULL,
	[RecipientName]			NVARCHAR (MAX)	NOT NULL,
	[Phone]					NVARCHAR (MAX)	NOT NULL,
	[WarehouseWorkingTime]	NVARCHAR (MAX)	NULL,
	[MethodOfTransitId]		INT				NOT NULL,
	[DeliveryTypeId]		INT				NOT NULL,
	[CarrierId]				BIGINT			NOT NULL,
	[CityId]				BIGINT			NOT NULL,

	CONSTRAINT [PK_dbo.Transit] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Transit_dbo.Carrier_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [dbo].[Carrier] ([Id]),
	CONSTRAINT [FK_dbo.Transit_dbo.City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id])
);
GO

CREATE NONCLUSTERED INDEX [IX_CarrierId] ON [dbo].[Transit]([CarrierId] ASC);
GO