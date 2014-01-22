CREATE TABLE [dbo].[Sender] 
(
	[Id]		BIGINT			IDENTITY (1, 1) NOT NULL,
	[UserId]	BIGINT			NOT NULL,
	[Name]		NVARCHAR (MAX)	NOT NULL,
	[Email]		NVARCHAR (320)	NOT NULL,
	[CountryId]	BIGINT			NOT NULL,
	[TariffOfTapePerBox] MONEY	NOT NULL CONSTRAINT [DF_Sender_TariffOfTapePerBox] DEFAULT(4),

	CONSTRAINT [PK_dbo.Sender] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Sender_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Sender_dbo.Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Sender]([UserId] ASC);
GO