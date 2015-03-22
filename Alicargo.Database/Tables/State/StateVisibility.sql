CREATE TABLE [dbo].[StateVisibility] (
	[RoleId] INT NOT NULL,
	[StateId] BIGINT NOT NULL,
	
	CONSTRAINT [PK_dbo.StateVisibility] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC),
	CONSTRAINT [FK_dbo.StateVisibility_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_StateVisibility_StateId] ON [dbo].[StateVisibility]([StateId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_StateVisibility_RoleId] ON [dbo].[StateVisibility]([RoleId] ASC);
GO