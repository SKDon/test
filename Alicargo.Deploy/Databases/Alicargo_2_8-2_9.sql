GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application]
    ADD [IsPickup] BIT CONSTRAINT [DF_Application_IsPickup] DEFAULT 0 NOT NULL;


GO
UPDATE a
	SET a.[IsPickup] = 1
	FROM  [dbo].[Application] a
		JOIN [dbo].[Transit] t ON a.[TransitId] = t.[Id]
		JOIN [dbo].[Carrier] c ON c.[Id] = t.[CarrierId]
	WHERE c.[Name] = N'Самовывоз'


GO
ALTER TABLE [dbo].[Application]
    DROP CONSTRAINT [DF_Application_IsPickup];


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
