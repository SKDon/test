CREATE TABLE [dbo].[EmailMessage]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL,
	
	[StateId] INT NOT NULL,
	[StateIdTimestamp] DATETIMEOFFSET CONSTRAINT [DF_EmailMessage_StateIdTimestamp]  DEFAULT (GETUTCDATE()) NOT NULL,
	[CreationTimestamp] DATETIMEOFFSET CONSTRAINT [DF_EmailMessage_CreationTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,
	[PartitionId] INT NOT NULL,

	[From] NVARCHAR (MAX) NOT NULL,
	[To] NVARCHAR (MAX) NOT NULL,
	[CopyTo] NVARCHAR (MAX) NULL,
	[Subject] NVARCHAR (MAX) NOT NULL,
	[Body] NVARCHAR (MAX) NOT NULL,
	[IsBodyHtml] BIT CONSTRAINT [DF_EmailMessage_IsBodyHtml] DEFAULT (0) NOT NULL,
	[Files] VARBINARY (MAX) NULL,

	CONSTRAINT [PK_dbo.EmailMessage] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE INDEX [IX_EmailMessage_State_Partition] ON [dbo].[EmailMessage] ([StateId], [PartitionId])
GO
