GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application]
    ADD [DocumentWeight] REAL NULL;


GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_DeleteForce]';


GO
PRINT N'Refreshing [dbo].[Transit_GetByApplication]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Transit_GetByApplication]';


GO
PRINT N'Update complete.';


GO
