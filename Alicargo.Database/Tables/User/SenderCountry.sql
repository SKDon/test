CREATE TABLE [dbo].[SenderCountry]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[SenderId] BIGINT NOT NULL,
	[CountryId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.SenderCountry] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.SenderCountry_dbo.Sender_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Sender] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.SenderCountry_dbo.Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]) ON DELETE CASCADE
)
GO