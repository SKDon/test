CREATE TABLE [dbo].[VisibleState] (
    [RoleId]  INT    NOT NULL,
    [StateId] BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.VisibleState] PRIMARY KEY CLUSTERED ([RoleId] ASC, [StateId] ASC),
    CONSTRAINT [FK_dbo.VisibleState_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_VisibleState_StateId]
    ON [dbo].[VisibleState]([StateId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_VisibleState_RoleId]
    ON [dbo].[VisibleState]([RoleId] ASC);