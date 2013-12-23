CREATE TABLE [dbo].[StateAvailability] (
	[RoleId] INT NOT NULL,
	[StateId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.StateAvailability] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC),
	CONSTRAINT [FK_dbo.StateAvailability_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_StateAvailability_StateId] ON [dbo].[StateAvailability]([StateId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_StateAvailability_RoleId] ON [dbo].[StateAvailability]([RoleId] ASC);
GO