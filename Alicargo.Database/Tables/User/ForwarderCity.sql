CREATE TABLE [dbo].[ForwarderCity]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[ForwarderId] BIGINT NOT NULL,
	[CityId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.ForwarderCity] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.ForwarderCity_dbo.Forwarder_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [dbo].[Forwarder] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.ForwarderCity_dbo.City_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[City] ([Id]) ON DELETE CASCADE
)
GO