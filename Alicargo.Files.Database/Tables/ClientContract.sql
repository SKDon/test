CREATE TABLE [dbo].[ClientContract]
(
	[Id] BIGINT NOT NULL PRIMARY KEY,
	[UpdateTimestamp] DATETIMEOFFSET NOT NULL DEFAULT(GETUTCDATE()),
	[ClientId] BIGINT NOT NULL,
	[Data]	VARBINARY (MAX)	NOT NULL,
	[Name]	NVARCHAR (MAX) NOT NULL,
)
GO

CREATE INDEX [IX_ClientContract_ClientId] ON [dbo].[ClientContract] ([ClientId])
GO
