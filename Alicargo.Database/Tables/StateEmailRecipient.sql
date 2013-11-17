CREATE TABLE [dbo].[StateEmailRecipient]
(
	[RoleId] INT NOT NULL,
	[StateId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.StateEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC),
	CONSTRAINT [FK_dbo.StateEmailRecipient_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);
GO