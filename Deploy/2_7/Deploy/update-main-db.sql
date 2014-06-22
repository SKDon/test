USE [$(DatabaseName)];


GO
PRINT N'Altering [dbo].[AirWaybill]...';


GO
ALTER TABLE [dbo].[AirWaybill]
    ADD [IsActive]      BIT    CONSTRAINT [DF_AirWaybill_IsActive] DEFAULT (1) NOT NULL,
        [CreatorUserId] BIGINT CONSTRAINT [DF_Temp] DEFAULT (4) NOT NULL


GO
PRINT N'Creating FK_dbo.AirWaybill_dbo.User_CreatorUserId...';


GO
ALTER TABLE [dbo].[AirWaybill] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AirWaybill_dbo.User_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [dbo].[User] ([Id]);

GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF_Temp];

GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[AirWaybill] WITH CHECK CHECK CONSTRAINT [FK_dbo.AirWaybill_dbo.User_CreatorUserId];


GO
PRINT N'Update complete.';


GO
