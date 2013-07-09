CREATE TABLE [dbo].[AvailableState] (
    [RoleId]  INT    NOT NULL,
    [StateId] BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.AvailableState] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC),
    CONSTRAINT [FK_dbo.AvailableState_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_AvailableState_StateId]
    ON [dbo].[AvailableState]([StateId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_AvailableState_RoleId]
    ON [dbo].[AvailableState]([RoleId] ASC);