CREATE TABLE [dbo].[Factory] (
    [Id]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (320) NOT NULL,
    [Phone] NVARCHAR (MAX) NULL,
    [Email] NVARCHAR (320) NULL,
    CONSTRAINT [PK_dbo.Factory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Factory_Name]
    ON [dbo].[Factory]([Name] ASC);

