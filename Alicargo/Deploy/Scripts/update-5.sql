:setvar MainDbName "Alicargo_2_1"
GO

USE [$(MainDbName)];
GO


DROP INDEX [IX_Calculation_StateId]
    ON [dbo].[Calculation];

GO
ALTER TABLE [dbo].[Calculation] DROP CONSTRAINT [DF_Calculation_StateIdTimestamp];

GO
ALTER TABLE [dbo].[ApplicationEvent] DROP CONSTRAINT [DF_ApplicationEvent_StateIdTimestamp];

GO
ALTER TABLE [dbo].[ApplicationEvent] DROP CONSTRAINT [DF_ApplicationEvent_CreationTimestamp];

GO
ALTER TABLE [dbo].[Calculation] DROP CONSTRAINT [FK_Calculation_Client];

GO
ALTER TABLE [dbo].[ApplicationEvent] DROP CONSTRAINT [FK_dbo.ApplicationEvent_dbo.Application];

GO
DROP PROCEDURE [dbo].[ApplicationEvent_Add];

GO
DROP PROCEDURE [dbo].[ApplicationEvent_Delete];

GO
DROP PROCEDURE [dbo].[ApplicationEvent_GetNext];

GO
DROP PROCEDURE [dbo].[ApplicationEvent_SetState];

GO
DROP PROCEDURE [dbo].[Calculation_SetState];

GO
DROP TABLE [dbo].[ApplicationEvent];

GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Calculation] (
    [Id]                   BIGINT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp]    DATETIMEOFFSET (7) NOT NULL,
    [ClientId]             BIGINT             NOT NULL,
    [ApplicationHistoryId] BIGINT             NOT NULL,
    [AirWaybillDisplay]    NVARCHAR (320)     NOT NULL,
    [ApplicationDisplay]   NVARCHAR (320)     NOT NULL,
    [MarkName]             NVARCHAR (320)     NOT NULL,
    [Weight]               REAL               NOT NULL,
    [TariffPerKg]          MONEY              NOT NULL,
    [ScotchCost]           MONEY              NOT NULL,
    [InsuranceCost]        MONEY              NOT NULL,
    [FactureCost]          MONEY              NOT NULL,
    [TransitCost]          MONEY              NOT NULL,
    [PickupCost]           MONEY              NOT NULL,
    [FactoryName]          NVARCHAR (320)     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Calculation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Calculation])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Calculation] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Calculation] 
		([Id], [ClientId], [ApplicationHistoryId], [AirWaybillDisplay], [ApplicationDisplay], [MarkName], [Weight], 
		[TariffPerKg], [ScotchCost], [InsuranceCost], [FactureCost], [TransitCost], [PickupCost], [FactoryName], [CreationTimestamp])
        SELECT   [Id],
                 [ClientId],
                 [ApplicationHistoryId],
                 [AirWaybillDisplay],
                 [ApplicationDisplay],
                 [MarkName],
                 [Weight],
                 [TariffPerKg],
                 [ScotchCost],
                 [InsuranceCost],
                 [FactureCost],
                 [TransitCost],
                 [PickupCost],
                 [FactoryName],
				 GETUTCDATE()
        FROM     [dbo].[Calculation]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Calculation] OFF;
    END

DROP TABLE [dbo].[Calculation];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Calculation]', N'Calculation';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Calculation]', N'PK_dbo.Calculation', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

GO
CREATE NONCLUSTERED INDEX [IX_Calculation_ClientId]
    ON [dbo].[Calculation]([ClientId] ASC);



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Calculation_ApplicationHistoryId]
    ON [dbo].[Calculation]([ApplicationHistoryId] ASC);






GO
CREATE TABLE [dbo].[Event] (
    [Id]               BIGINT             IDENTITY (1, 1) NOT NULL,
    [StateId]          INT                NOT NULL,
    [StateIdTimestamp] DATETIMEOFFSET (7) NOT NULL,
    [PartitionId]      INT                NOT NULL,
    [EventTypeId]      INT                NOT NULL,
    [Data]             VARBINARY (MAX)    NULL,
    CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED ([Id] ASC)
);



GO
ALTER TABLE [dbo].[Event]
    ADD CONSTRAINT [DF_Event_StateIdTimestamp] DEFAULT (GETUTCDATE()) FOR [StateIdTimestamp];



GO
ALTER TABLE [dbo].[Calculation] WITH NOCHECK
    ADD CONSTRAINT [FK_Calculation_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);




GO
ALTER PROCEDURE [dbo].[EventEmailRecipient_Set]
	@EventTypeId INT,
	@Recipients [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[EventEmailRecipient]
		WHERE [EventTypeId] = @EventTypeId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Recipients)
		
		INSERT [dbo].[EventEmailRecipient] ([EventTypeId], [RoleId])
		SELECT @EventTypeId AS [StateId], r.[Id] AS [RoleId]
		FROM @Recipients r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[EventEmailRecipient] a WHERE a.[EventTypeId] = @EventTypeId)

	COMMIT

END


GO
CREATE PROCEDURE [dbo].[Calculation_Add]
	@ClientId BIGINT,
	@ApplicationHistoryId BIGINT,
	@AirWaybillDisplay NVARCHAR(320),
    @ApplicationDisplay NVARCHAR(320),
    @MarkName NVARCHAR(320),
    @Weight REAL,
    @TariffPerKg MONEY,
    @ScotchCost MONEY,
    @InsuranceCost MONEY,
    @FactureCost MONEY,
    @TransitCost MONEY,
    @PickupCost MONEY,
    @FactoryName NVARCHAR(320),
	@CreationTimestamp DATETIMEOFFSET

AS BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Calculation]
           ([ClientId]
           ,[ApplicationHistoryId]
           ,[AirWaybillDisplay]
           ,[ApplicationDisplay]
           ,[MarkName]
           ,[Weight]
           ,[TariffPerKg]
           ,[ScotchCost]
           ,[InsuranceCost]
           ,[FactureCost]
           ,[TransitCost]
           ,[PickupCost]
           ,[FactoryName]
		   ,[CreationTimestamp])
     VALUES
           (@ClientId
           ,@ApplicationHistoryId
           ,@AirWaybillDisplay
           ,@ApplicationDisplay
           ,@MarkName
           ,@Weight
           ,@TariffPerKg
           ,@ScotchCost
           ,@InsuranceCost
           ,@FactureCost
           ,@TransitCost
           ,@PickupCost
           ,@FactoryName
		   ,@CreationTimestamp)
END


GO
CREATE PROCEDURE [dbo].[Calculation_GetByApplication]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		[ClientId],
		[AirWaybillDisplay],
		[ApplicationDisplay],
		[MarkName],
		[Weight],
		[TariffPerKg],
		[ScotchCost],
		[InsuranceCost],
		[FactureCost],
		[TransitCost],
		[PickupCost],
		[FactoryName],
		[CreationTimestamp]
	FROM [dbo].[Calculation]
	WHERE [ApplicationHistoryId] = @ApplicationId

END


GO
CREATE PROCEDURE [dbo].[Calculation_RemoveByApplication]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[Calculation]
    WHERE [ApplicationHistoryId] = @ApplicationId

END


GO
CREATE PROCEDURE [dbo].[Client_SumBalance]
AS BEGIN
	SET NOCOUNT ON;

	SELECT SUM(c.[Balance])
	FROM [dbo].[Client] c

END


GO
CREATE PROCEDURE [dbo].[Event_Add]
	@StateId INT,
	@PartitionId INT,
	@EventTypeId INT,
	@Data VARBINARY(MAX)

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Event] ([EventTypeId], [StateId], [Data], [PartitionId])
	VALUES (@EventTypeId, @StateId, @Data, @PartitionId)

END


GO
CREATE PROCEDURE [dbo].[Event_Delete]
	@Id BIGINT
AS
	SET NOCOUNT ON;

	DELETE TOP(1)
	FROM [dbo].[Event]
	WHERE [Id] = @Id


GO
CREATE PROCEDURE [dbo].[Event_GetNext]
	@EventTypeId INT,
	@PartitionId INT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			e.[Id],
			e.[StateId] AS [State],
			e.[Data]
	FROM [dbo].[Event] e
	WHERE e.[EventTypeId] = @EventTypeId AND e.[PartitionId] = @PartitionId AND e.[StateId] <> -1
	ORDER BY e.[Id]

END


GO
CREATE PROCEDURE [dbo].[Event_SetState]
	@Id BIGINT,
	@State INT
AS
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[Event]
	SET [StateId] = @State, [StateIdTimestamp] = GETUTCDATE()
	WHERE [Id] = @Id


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EventEmailRecipient_Get]';




GO
ALTER TABLE [dbo].[Calculation] WITH CHECK CHECK CONSTRAINT [FK_Calculation_Client];


ALTER TABLE [dbo].[EventEmailRecipient] ADD [_EventTypeId] INT NULL
GO

UPDATE [dbo].[EventEmailRecipient] SET [_EventTypeId] = [EventTypeId]
GO

ALTER TABLE [dbo].[EventEmailRecipient] DROP CONSTRAINT [PK_dbo.EventEmailRecipient]
ALTER TABLE [dbo].[EventEmailRecipient] DROP COLUMN [EventTypeId]
GO

ALTER TABLE [dbo].[EventEmailRecipient] ALTER COLUMN [_EventTypeId] INT NOT NULL;
GO

EXEC sp_rename 'EventEmailRecipient._EventTypeId', 'EventTypeId', 'COLUMN'
GO

ALTER TABLE [dbo].[EventEmailRecipient] ADD
CONSTRAINT [PK_dbo.EventEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [EventTypeId] ASC)
GO
GO
CREATE TABLE [dbo].[ClientBalanceHistory] (
    [Id]                BIGINT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp] DATETIMEOFFSET (7) NOT NULL,
    [Timestamp]         DATETIMEOFFSET (7) NOT NULL,
    [ClientId]          BIGINT             NOT NULL,
    [EventTypeId]       INT                NOT NULL,
    [Balance]           MONEY              NOT NULL,
    [Money]             MONEY              NOT NULL,
    [Comment]           NVARCHAR (MAX)     NULL,
    CONSTRAINT [PK_dbo.ClientBalanceHistory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ClientBalanceHistory].[IX_ClientId]...';


GO
CREATE NONCLUSTERED INDEX [IX_ClientId]
    ON [dbo].[ClientBalanceHistory]([ClientId] ASC);


GO
PRINT N'Creating DF_ClientBalanceHistory_CreationTimestamp...';


GO
ALTER TABLE [dbo].[ClientBalanceHistory]
    ADD CONSTRAINT [DF_ClientBalanceHistory_CreationTimestamp] DEFAULT (GETUTCDATE()) FOR [CreationTimestamp];


GO
PRINT N'Creating FK_dbo.ClientBalanceHistory_dbo.Client_ClientId...';


GO
ALTER TABLE [dbo].[ClientBalanceHistory] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.ClientBalanceHistory_dbo.Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[ClientBalanceHistory_Add]...';


GO
CREATE PROCEDURE [dbo].[ClientBalanceHistory_Add]
	@ClientId BIGINT,
	@Balance MONEY,
	@Money MONEY,
	@Comment NVARCHAR(MAX),
	@Timestamp DATETIMEOFFSET,
	@EventTypeId INT

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[ClientBalanceHistory] ([Balance], [Comment], [ClientId], [Money], [Timestamp], [EventTypeId])
	VALUES (@Balance, @Comment, @ClientId, @Money, @Timestamp, @EventTypeId)

END
GO
PRINT N'Creating [dbo].[ClientBalanceHistory_Get]...';


GO
CREATE PROCEDURE [dbo].[ClientBalanceHistory_Get]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [Balance], [Comment], [Timestamp], [Money], [CreationTimestamp], [EventTypeId] AS [EventType]
	FROM [dbo].[ClientBalanceHistory]
	WHERE [ClientId] = @ClientId
	ORDER BY [Id] DESC

END

GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EventEmailRecipient_Get]';

GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EventEmailRecipient_Set]';

GO
ALTER TABLE [dbo].[ClientBalanceHistory] WITH CHECK CHECK CONSTRAINT [FK_dbo.ClientBalanceHistory_dbo.Client_ClientId];
GO