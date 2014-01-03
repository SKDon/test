CREATE TABLE [dbo].[ClientBalanceHistory]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[Timestamp]	DATETIMEOFFSET NOT NULL,
	[ClientId] BIGINT NOT NULL,

	[Balance] MONEY NOT NULL,
	[Input] MONEY NOT NULL,
	[Comment] NVARCHAR(MAX) NULL,

	CONSTRAINT [PK_dbo.ClientBalanceHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.ClientBalanceHistory_dbo.Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]) ON DELETE CASCADE,
)
GO

CREATE NONCLUSTERED INDEX [IX_ClientId] ON [dbo].[ClientBalanceHistory]([ClientId] ASC)
GO