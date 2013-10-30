use Alicargo_Dev


GO
DROP INDEX [IX_BrockerId]
    ON [dbo].[AirWaybill];


GO
PRINT N'Dropping [dbo].[AirWaybill].[IX_Reference_Bill]...';


GO
DROP INDEX [IX_Reference_Bill]
    ON [dbo].[AirWaybill];


GO
PRINT N'Dropping [dbo].[Application].[IX_ReferenceId]...';


GO
DROP INDEX [IX_ReferenceId]
    ON [dbo].[Application];


GO
PRINT N'Dropping DF_AirWaybillCreationTimestamp...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF_AirWaybillCreationTimestamp];


GO
PRINT N'Dropping DF__AirWaybil__State__1B0907CE...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [DF__AirWaybil__State__1B0907CE];


GO
PRINT N'Dropping DF_Application_CreationTimestamp...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [DF_Application_CreationTimestamp];


GO
PRINT N'Dropping DF__User__TwoLetterI__1DE57479...';


GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF__User__TwoLetterI__6C190EBB];


GO
PRINT N'Dropping FK_dbo.Application_dbo.AirWaybill_AirWaybillId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId];


GO
PRINT N'Dropping FK_dbo.Reference_dbo.Brocker_BrockerId...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [FK_dbo.Reference_dbo.Brocker_BrockerId];


GO
PRINT N'Dropping FK_dbo.Reference_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[AirWaybill] DROP CONSTRAINT [FK_dbo.Reference_dbo.State_StateId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Client_ClientId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.State_StateId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Transit_TransitId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Country_CountryId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId];


GO
PRINT N'Dropping FK_dbo.Brocker_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Broker] DROP CONSTRAINT [FK_dbo.Brocker_dbo.User_UserId];


GO
PRINT N'Starting rebuilding table [dbo].[AirWaybill]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AirWaybill] (
    [Id]                         BIGINT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp]          DATETIMEOFFSET (7) CONSTRAINT [DF_AirWaybillCreationTimestamp] DEFAULT (getutcdate()) NOT NULL,
    [ArrivalAirport]             NVARCHAR (MAX)     NOT NULL,
    [DepartureAirport]           NVARCHAR (MAX)     NOT NULL,
    [DateOfDeparture]            DATETIMEOFFSET (7) NOT NULL,
    [DateOfArrival]              DATETIMEOFFSET (7) NOT NULL,
    [BrokerId]                   BIGINT             NOT NULL,
    [GTD]                        NVARCHAR (320)     NULL,
    [Bill]                       NVARCHAR (320)     NOT NULL,
    [GTDFileData]                VARBINARY (MAX)    NULL,
    [GTDFileName]                NVARCHAR (320)     NULL,
    [GTDAdditionalFileData]      VARBINARY (MAX)    NULL,
    [GTDAdditionalFileName]      NVARCHAR (320)     NULL,
    [PackingFileData]            VARBINARY (MAX)    NULL,
    [PackingFileName]            NVARCHAR (320)     NULL,
    [InvoiceFileData]            VARBINARY (MAX)    NULL,
    [InvoiceFileName]            NVARCHAR (320)     NULL,
    [AWBFileData]                VARBINARY (MAX)    NULL,
    [AWBFileName]                NVARCHAR (320)     NULL,
    [StateId]                    BIGINT             NOT NULL,
    [StateChangeTimestamp]       DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [FlightCost]                 MONEY              NULL,
    [CustomCost]                 MONEY              NULL,
    [BrokerCost]                 MONEY              NULL,
    [AdditionalCost]             MONEY              NULL,
    [TotalCostOfSenderForWeight] MONEY              NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.AirWaybill] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AirWaybill])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AirWaybill] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AirWaybill] ([Id], [CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [GTDFileData], [GTDFileName], [GTDAdditionalFileData], [GTDAdditionalFileName], [PackingFileData], [PackingFileName], [InvoiceFileData], [InvoiceFileName], [AWBFileData], [AWBFileName], [StateId], [StateChangeTimestamp], [FlightCost], [CustomCost], [BrokerCost], [AdditionalCost])
        SELECT   [Id],
                 [CreationTimestamp],
                 [Bill],
                 [ArrivalAirport],
                 [DepartureAirport],
                 [DateOfDeparture],
                 [DateOfArrival],
                 [BrokerId],
                 [GTD],
                 [GTDFileData],
                 [GTDFileName],
                 [GTDAdditionalFileData],
                 [GTDAdditionalFileName],
                 [PackingFileData],
                 [PackingFileName],
                 [InvoiceFileData],
                 [InvoiceFileName],
                 [AWBFileData],
                 [AWBFileName],
                 [StateId],
                 [StateChangeTimestamp],
                 [FlightCost],
                 [CustomCost],
                 [BrokerCost],
                 [AdditionalCost]
        FROM     [dbo].[AirWaybill]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AirWaybill] OFF;
    END

DROP TABLE [dbo].[AirWaybill];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AirWaybill]', N'AirWaybill';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.AirWaybill]', N'PK_dbo.AirWaybill', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AirWaybill].[IX_BrokerId]...';


GO
CREATE NONCLUSTERED INDEX [IX_BrokerId]
    ON [dbo].[AirWaybill]([BrokerId] ASC);


GO
PRINT N'Creating [dbo].[AirWaybill].[IX_AirWaybill_Bill]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AirWaybill_Bill]
    ON [dbo].[AirWaybill]([Bill] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[Application]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Application] (
    [Id]                   BIGINT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp]    DATETIMEOFFSET (7) CONSTRAINT [DF_Application_CreationTimestamp] DEFAULT (getutcdate()) NOT NULL,
    [Invoice]              NVARCHAR (MAX)     NOT NULL,
    [InvoiceFileData]      VARBINARY (MAX)    NULL,
    [InvoiceFileName]      NVARCHAR (MAX)     NULL,
    [SwiftFileData]        VARBINARY (MAX)    NULL,
    [SwiftFileName]        NVARCHAR (MAX)     NULL,
    [DeliveryBillFileData] VARBINARY (MAX)    NULL,
    [DeliveryBillFileName] NVARCHAR (MAX)     NULL,
    [Torg12FileData]       VARBINARY (MAX)    NULL,
    [Torg12FileName]       NVARCHAR (MAX)     NULL,
    [CPFileData]           VARBINARY (MAX)    NULL,
    [CPFileName]           NVARCHAR (MAX)     NULL,
    [PackingFileData]      VARBINARY (MAX)    NULL,
    [PackingFileName]      NVARCHAR (320)     NULL,
    [Characteristic]       NVARCHAR (MAX)     NULL,
    [AddressLoad]          NVARCHAR (MAX)     NULL,
    [WarehouseWorkingTime] NVARCHAR (MAX)     NULL,
    [TransitReference]     NVARCHAR (MAX)     NULL,
    [Weight]               REAL               NULL,
    [Count]                INT                NULL,
    [Volume]               REAL               NOT NULL,
    [TermsOfDelivery]      NVARCHAR (MAX)     NULL,
    [MethodOfDeliveryId]   INT                NOT NULL,
    [DateOfCargoReceipt]   DATETIMEOFFSET (7) NULL,
    [DateInStock]          DATETIMEOFFSET (7) NULL,
    [StateChangeTimestamp] DATETIMEOFFSET (7) NOT NULL,
    [StateId]              BIGINT             NOT NULL,
    [Value]                MONEY              NOT NULL,
    [CurrencyId]           INT                NOT NULL,
    [ClassId]              INT                NULL,
    [Certificate]          NVARCHAR (MAX)     NULL,
    [ClientId]             BIGINT             NOT NULL,
    [TransitId]            BIGINT             NOT NULL,
    [CountryId]            BIGINT             NULL,
    [AirWaybillId]         BIGINT             NULL,
    [SenderId]             BIGINT             NULL,
    [FactoryName]          NVARCHAR (320)     NOT NULL,
    [FactoryPhone]         NVARCHAR (MAX)     NULL,
    [FactoryContact]       NVARCHAR (MAX)     NULL,
    [FactoryEmail]         NVARCHAR (320)     NULL,
    [MarkName]             NVARCHAR (320)     NOT NULL,
    [FactureCost]          MONEY              NULL,
    [PickupCost]         MONEY              NULL,
    [FactureCostEdited]    MONEY              NULL,
    [ScotchCostEdited]     MONEY              NULL,
    [PickupCostEdited]   MONEY              NULL,
    [TariffPerKg]          MONEY              NULL,
    [SenderRate]           MONEY              NULL,
    [TransitCost]          MONEY              NULL,
    [TransitCostEdited]    MONEY              NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Application] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Application])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Application] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Weight], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName])
        SELECT   [Id],
                 [CreationTimestamp],
                 [Invoice],
                 [InvoiceFileData],
                 [InvoiceFileName],
                 [SwiftFileData],
                 [SwiftFileName],
                 [DeliveryBillFileData],
                 [DeliveryBillFileName],
                 [Torg12FileData],
                 [Torg12FileName],
                 [CPFileData],
                 [CPFileName],
                 [PackingFileData],
                 [PackingFileName],
                 [Characteristic],
                 [AddressLoad],
                 [WarehouseWorkingTime],
                 [TransitReference],
                 [Weight],
                 [Count],
                 [Volume],
                 [TermsOfDelivery],
                 [MethodOfDeliveryId],
                 [DateOfCargoReceipt],
                 [DateInStock],
                 [StateChangeTimestamp],
                 [StateId],
                 [Value],
                 [CurrencyId],
                 [ClientId],
                 [TransitId],
                 [CountryId],
                 [AirWaybillId],
                 [FactoryName],
                 [FactoryPhone],
                 [FactoryContact],
                 [FactoryEmail],
                 [MarkName]
        FROM     [dbo].[Application]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Application] OFF;
    END

DROP TABLE [dbo].[Application];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Application]', N'Application';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Application]', N'PK_dbo.Application', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Application].[IX_ClientId]...';


GO
CREATE NONCLUSTERED INDEX [IX_ClientId]
    ON [dbo].[Application]([ClientId] ASC);


GO
PRINT N'Creating [dbo].[Application].[IX_StateId]...';


GO
CREATE NONCLUSTERED INDEX [IX_StateId]
    ON [dbo].[Application]([StateId] ASC);


GO
PRINT N'Creating [dbo].[Application].[IX_Application_TransitId]...';


GO
CREATE NONCLUSTERED INDEX [IX_Application_TransitId]
    ON [dbo].[Application]([TransitId] ASC);


GO
PRINT N'Creating [dbo].[Application].[IX_AirWaybillId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AirWaybillId]
    ON [dbo].[Application]([AirWaybillId] ASC);


GO
PRINT N'Altering [dbo].[Sender]...';


GO
ALTER TABLE [dbo].[Sender]
    ADD [TariffOfTapePerBox] MONEY DEFAULT ((4)) NOT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[Broker]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Broker] (
    [Id]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId] BIGINT         NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [Email]  NVARCHAR (320) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Broker] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Broker])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Broker] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Broker] ([Id], [UserId], [Name], [Email])
        SELECT   [Id],
                 [UserId],
                 [Name],
                 [Email]
        FROM     [dbo].[Broker]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Broker] OFF;
    END

DROP TABLE [dbo].[Broker];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Broker]', N'Broker';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Broker]', N'PK_dbo.Broker', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Broker].[IX_UserId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Broker]([UserId] ASC);


GO
PRINT N'Creating [dbo].[Calculation]...';


GO
CREATE TABLE [dbo].[Calculation] (
    [Id]                   BIGINT             IDENTITY (1, 1) NOT NULL,
    [RowVersion]           ROWVERSION         NOT NULL,
    [StateId]              INT                NOT NULL,
    [StateIdTimestamp]     DATETIMEOFFSET (7) NOT NULL,
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
	[TransitCost]			MONEY			NOT NULL,
	[PickupCost]			MONEY			NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Calculation].[IX_Calculation_StateId]...';


GO
CREATE NONCLUSTERED INDEX [IX_Calculation_StateId]
    ON [dbo].[Calculation]([StateId] ASC);


GO
PRINT N'Creating [dbo].[Calculation].[IX_Calculation_ClientId]...';


GO
CREATE NONCLUSTERED INDEX [IX_Calculation_ClientId]
    ON [dbo].[Calculation]([ClientId] ASC);


GO
PRINT N'Creating [dbo].[Calculation].[IX_Calculation_ApplicationHistoryId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Calculation_ApplicationHistoryId]
    ON [dbo].[Calculation]([ApplicationHistoryId] ASC);


GO
PRINT N'Creating [dbo].[IdsTable]...';


GO
CREATE TYPE [dbo].[IdsTable] AS TABLE (
    [Id] BIGINT NOT NULL);


GO
PRINT N'Creating DF__User__TwoLetterI__24927208...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT ('en') FOR [TwoLetterISOLanguageName];


GO
PRINT N'Creating DF__Calculati__State__21B6055D...';


GO
ALTER TABLE [dbo].[Calculation]
    ADD DEFAULT (getutcdate()) FOR [StateIdTimestamp];


GO
PRINT N'Creating FK_dbo.Application_dbo.AirWaybill_AirWaybillId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId] FOREIGN KEY ([AirWaybillId]) REFERENCES [dbo].[AirWaybill] ([Id]);


GO
PRINT N'Creating FK_dbo.AirWaybill_dbo.Broker_BrokerId...';


GO
ALTER TABLE [dbo].[AirWaybill] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AirWaybill_dbo.Broker_BrokerId] FOREIGN KEY ([BrokerId]) REFERENCES [dbo].[Broker] ([Id]);


GO
PRINT N'Creating FK_dbo.AirWaybill_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[AirWaybill] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Client_ClientId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Transit_TransitId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId] FOREIGN KEY ([TransitId]) REFERENCES [dbo].[Transit] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Country_CountryId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Sender_SenderId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Sender] ([Id]);


GO
PRINT N'Creating FK_dbo.Broker_dbo.User_UserId...';


GO
ALTER TABLE [dbo].[Broker] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Broker_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating FK_Calculation_Client...';


GO
ALTER TABLE [dbo].[Calculation] WITH NOCHECK
    ADD CONSTRAINT [FK_Calculation_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating [dbo].[Calculation_SetState]...';


GO
CREATE PROCEDURE [dbo].[Calculation_SetState]
	@Id	BIGINT,
	@RowVersion ROWVERSION,
	@State INT
AS
	SET NOCOUNT ON;

	UPDATE	[dbo].[Calculation]
	SET		[StateId] = @State,
			[StateIdTimestamp] = GETUTCDATE()
	OUTPUT	[INSERTED].[RowVersion], [INSERTED].[StateIdTimestamp] AS [StateTimestamp]
	WHERE	[Id] = @Id AND [RowVersion] = @RowVersion
GO
PRINT N'Creating [dbo].[Sender_Get]...';


GO
CREATE PROCEDURE [dbo].[Sender_Get]
	@Id BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[Name],
			s.[Email],
			s.[TariffOfTapePerBox],
			u.[Login]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]
	WHERE	s.[Id] = @Id
GO
PRINT N'Creating [dbo].[Sender_GetTariffs]...';


GO
CREATE PROCEDURE [dbo].[Sender_GetTariffs]
	@Ids [dbo].[IdsTable] READONLY
AS
	SET NOCOUNT ON;

	SELECT	s.[Id],
			s.[TariffOfTapePerBox]
	FROM	[dbo].[Sender] s
	WHERE	s.[Id] IN (SELECT [Id] FROM @Ids)
GO
PRINT N'Creating [dbo].[Sender_GetUserId]...';


GO
CREATE PROCEDURE [dbo].[Sender_GetUserId]
	@Id BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[UserId]
	FROM	[dbo].[Sender] s
	WHERE	s.[Id] = @Id
GO
PRINT N'Creating [dbo].[Sender_Update]...';


GO
CREATE PROCEDURE [dbo].[Sender_Update]
	@Id					BIGINT,
	@Login				NVARCHAR(320),
	@Name				NVARCHAR (MAX),
	@Email				NVARCHAR (320),
	@TariffOfTapePerBox	MONEY
AS
	SET NOCOUNT ON;

	DECLARE @Table TABLE(UserId BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Sender]
		SET		[Name] = @Name,
				[Email] = @Email,
				[TariffOfTapePerBox] = @TariffOfTapePerBox
		OUTPUT	inserted.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login
		WHERE	[Id] IN(SELECT [UserId] FROM @Table);

	COMMIT
GO
PRINT N'Creating [dbo].[User_Add]...';


GO
CREATE PROCEDURE [dbo].[User_Add]
	@Login						NVARCHAR(320),
	@PasswordHash				VARBINARY(MAX),
	@PasswordSalt				VARBINARY(MAX),
	@TwoLetterISOLanguageName	CHAR(2)
AS
	SET NOCOUNT ON;

	DECLARE @Table TABLE([UserId] BIGINT);
	DECLARE @UserId	BIGINT;

	INSERT	[dbo].[User] ([Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
	OUTPUT	inserted.[Id] INTO @Table
	VALUES	(@Login, @PasswordHash, @PasswordSalt, @TwoLetterISOLanguageName)

	SELECT TOP 1 @UserId = [UserId] FROM @Table
	RETURN @UserId
GO
PRINT N'Creating [dbo].[User_SetPassword]...';


GO
CREATE PROCEDURE [dbo].[User_SetPassword]
	@UserId BIGINT,
	@Salt VARBINARY(MAX),
	@Hash VARBINARY(MAX)
AS
	SET NOCOUNT ON;

	UPDATE	TOP(1) [dbo].[User]
	SET		[PasswordHash] = @Hash,
			[PasswordSalt] = @Salt
	WHERE	[Id] = @UserId
GO
PRINT N'Creating [dbo].[Sender_Add]...';


GO
CREATE PROCEDURE [dbo].[Sender_Add]
	@Login						NVARCHAR(320),
	@PasswordHash				VARBINARY(MAX),
	@PasswordSalt				VARBINARY(MAX),
	@TwoLetterISOLanguageName	CHAR(2),
	@Name						NVARCHAR (MAX),
	@Email						NVARCHAR (320),
	@TariffOfTapePerBox			MONEY
AS
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC	@UserId = [dbo].[User_Add]
				@Login = @Login, @PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @TwoLetterISOLanguageName

		INSERT	[dbo].[Sender]
				([UserId], [Name], [Email], [TariffOfTapePerBox])
		OUTPUT	inserted.[Id]
		VALUES	(@UserId, @Name, @Email, @TariffOfTapePerBox)

	COMMIT
GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.Client_DeleteForce';


GO
PRINT N'Checking existing data against newly created constraints';



GO
ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId];

ALTER TABLE [dbo].[AirWaybill] WITH CHECK CHECK CONSTRAINT [FK_dbo.AirWaybill_dbo.Broker_BrokerId];

ALTER TABLE [dbo].[AirWaybill] WITH CHECK CHECK CONSTRAINT [FK_dbo.AirWaybill_dbo.State_StateId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.State_StateId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId];

ALTER TABLE [dbo].[Broker] WITH CHECK CHECK CONSTRAINT [FK_dbo.Broker_dbo.User_UserId];

ALTER TABLE [dbo].[Calculation] WITH CHECK CHECK CONSTRAINT [FK_Calculation_Client];


GO
PRINT N'Update complete.'
GO

update [dbo].[Application]
set [SenderId] = 1
where [SenderId] is null

GO

use master