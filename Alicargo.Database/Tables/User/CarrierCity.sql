CREATE TABLE [dbo].[CarrierCity]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[CarrierId] BIGINT NOT NULL,
	[CityId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.CarrierCity] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.CarrierCity_dbo.Carrier_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [dbo].[Carrier] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.CarrierCity_dbo.City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]) ON DELETE CASCADE
)
GO