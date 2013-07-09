CREATE TABLE [dbo].[Mark] (
    [Id]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (320) NOT NULL,
    CONSTRAINT [PK_dbo.Mark] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Mark_Name]
    ON [dbo].[Mark]([Name] ASC);

