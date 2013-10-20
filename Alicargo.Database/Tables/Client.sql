CREATE TABLE [dbo].[Client] (
	[Id]				BIGINT			IDENTITY (1, 1) NOT NULL,
	[UserId]			BIGINT			NOT NULL,
	[Email]				NVARCHAR (320)	NOT NULL,
	[Nic]				NVARCHAR (MAX)	NOT NULL,
	[LegalEntity]		NVARCHAR (MAX)	NOT NULL,
	[Contacts]			NVARCHAR (MAX)	NOT NULL,
	[Phone]				NVARCHAR (MAX)	NULL,
	[INN]				NVARCHAR (MAX)	NULL,
	[KPP]				NVARCHAR (MAX)	NULL,
	[OGRN]				NVARCHAR (MAX)	NULL,
	[Bank]				NVARCHAR (MAX)	NULL,
	[BIC]				NVARCHAR (MAX)	NULL,
	[LegalAddress]		NVARCHAR (MAX)	NULL,
	[MailingAddress]	NVARCHAR (MAX)	NULL,
	[RS]				NVARCHAR (MAX)	NULL,
	[KS]				NVARCHAR (MAX)	NULL,
	[TransitId]			BIGINT			NOT NULL,

	CONSTRAINT [PK_dbo.Client] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Client_dbo.Transit_Transit_Id] FOREIGN KEY ([TransitId]) REFERENCES [dbo].[Transit] ([Id]),
	CONSTRAINT [FK_dbo.Client_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId]
	ON [dbo].[Client]([UserId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_TransitId]
	ON [dbo].[Client]([TransitId] ASC);