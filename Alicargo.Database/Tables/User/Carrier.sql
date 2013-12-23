CREATE TABLE [dbo].[Carrier] (
    [Id]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (320) NOT NULL,

    CONSTRAINT [PK_dbo.Carrier] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Carrier_Name]
    ON [dbo].[Carrier]([Name] ASC);