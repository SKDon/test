GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId];

GO
ALTER TABLE [dbo].[Application] ALTER COLUMN [SenderId] BIGINT NULL;

GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Sender] ([Id]);

GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_DeleteForce]';

GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Transit_GetByApplication]';

GO
ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId];
GO