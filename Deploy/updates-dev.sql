USE [$(MainDbName)] 


GO
PRINT N'Dropping DF_Application_CreationTimestamp...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [DF_Application_CreationTimestamp];


GO
PRINT N'Dropping DF_Application_InsuranceRate...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [DF_Application_InsuranceRate];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Country_CountryId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Sender_SenderId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.State_StateId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Forwarder_ForwarderId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Forwarder_ForwarderId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Client_ClientId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.Transit_TransitId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId];


GO
PRINT N'Dropping FK_dbo.Application_dbo.AirWaybill_AirWaybillId...';


GO
ALTER TABLE [dbo].[Application] DROP CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId];


GO
PRINT N'Starting rebuilding table [dbo].[Application]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Application] (
    [Id]                         BIGINT             IDENTITY (1, 1) NOT NULL,
    [CreationTimestamp]          DATETIMEOFFSET (7) CONSTRAINT [DF_Application_CreationTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,
    [DisplayNumber]              INT                NOT NULL,
    [Invoice]                    NVARCHAR (MAX)     NOT NULL,
    [Characteristic]             NVARCHAR (MAX)     NULL,
    [AddressLoad]                NVARCHAR (MAX)     NULL,
    [WarehouseWorkingTime]       NVARCHAR (MAX)     NULL,
    [TransitReference]           NVARCHAR (MAX)     NULL,
    [Weight]                     REAL               NULL,
    [Count]                      INT                NULL,
    [Volume]                     REAL               NOT NULL,
    [TermsOfDelivery]            NVARCHAR (MAX)     NULL,
    [MethodOfDeliveryId]         INT                NOT NULL,
    [DateOfCargoReceipt]         DATETIMEOFFSET (7) NULL,
    [DateInStock]                DATETIMEOFFSET (7) NULL,
    [ClassId]                    INT                NULL,
    [Certificate]                NVARCHAR (MAX)     NULL,
    [Value]                      MONEY              NOT NULL,
    [CurrencyId]                 INT                NOT NULL,
    [FactoryName]                NVARCHAR (320)     NOT NULL,
    [FactoryPhone]               NVARCHAR (MAX)     NULL,
    [FactoryContact]             NVARCHAR (MAX)     NULL,
    [FactoryEmail]               NVARCHAR (320)     NULL,
    [MarkName]                   NVARCHAR (320)     NOT NULL,
    [StateChangeTimestamp]       DATETIMEOFFSET (7) NOT NULL,
    [StateId]                    BIGINT             NOT NULL,
    [ClientId]                   BIGINT             NOT NULL,
    [TransitId]                  BIGINT             NOT NULL,
    [CountryId]                  BIGINT             NOT NULL,
    [AirWaybillId]               BIGINT             NULL,
    [SenderId]                   BIGINT             NOT NULL,
    [ForwarderId]                BIGINT             NOT NULL,
    [FactureCost]                MONEY              NULL,
    [FactureCostEdited]          MONEY              NULL,
    [FactureCostEx]              MONEY              NULL,
    [FactureCostExEdited]        MONEY              NULL,
    [PickupCost]                 MONEY              NULL,
    [PickupCostEdited]           MONEY              NULL,
    [TransitCost]                MONEY              NULL,
    [TransitCostEdited]          MONEY              NULL,
    [ScotchCostEdited]           MONEY              NULL,
    [TariffPerKg]                MONEY              NULL,
    [SenderRate]                 MONEY              NULL,
    [InsuranceRate]              REAL               CONSTRAINT [DF_Application_InsuranceRate] DEFAULT 0.01 NOT NULL,
    [CalculationTotalTariffCost] MONEY              NULL,
    [CalculationProfit]          MONEY              NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Application] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Application])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Application] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Application] ([Id], [CreationTimestamp], [Invoice], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Weight], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClassId], [Certificate], [ClientId], [TransitId], [CountryId], [AirWaybillId], [SenderId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName], [FactureCost], [PickupCost], [FactureCostEdited], [ScotchCostEdited], [PickupCostEdited], [TariffPerKg], [SenderRate], [TransitCost], [TransitCostEdited], [ForwarderId], [FactureCostEx], [FactureCostExEdited], [InsuranceRate], [CalculationTotalTariffCost], [CalculationProfit], [DisplayNumber])
        SELECT   [Id],
                 [CreationTimestamp],
                 [Invoice],
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
                 [ClassId],
                 [Certificate],
                 [ClientId],
                 [TransitId],
                 [CountryId],
                 [AirWaybillId],
                 [SenderId],
                 [FactoryName],
                 [FactoryPhone],
                 [FactoryContact],
                 [FactoryEmail],
                 [MarkName],
                 [FactureCost],
                 [PickupCost],
                 [FactureCostEdited],
                 [ScotchCostEdited],
                 [PickupCostEdited],
                 [TariffPerKg],
                 [SenderRate],
                 [TransitCost],
                 [TransitCostEdited],
                 [ForwarderId],
                 [FactureCostEx],
                 [FactureCostExEdited],
                 [InsuranceRate],
                 [CalculationTotalTariffCost],
                 [CalculationProfit],
                 [DisplayNumber]
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
PRINT N'Altering [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD [ContractNumber] NVARCHAR (MAX)     NULL,
        [ContractDate]   DATETIMEOFFSET (7) NULL;


GO
PRINT N'Creating [dbo].[Bill]...';


GO
CREATE TABLE [dbo].[Bill] (
    [Id]                        BIGINT             IDENTITY (1, 1) NOT NULL,
    [ApplicationId]             BIGINT             NOT NULL,
    [SaveDate]                  DATETIMEOFFSET (7) NOT NULL,
    [SendDate]                  DATETIMEOFFSET (7) NULL,
    [Number]                    INT                NOT NULL,
    [Bank]                      NVARCHAR (MAX)     NOT NULL,
    [BIC]                       NVARCHAR (MAX)     NOT NULL,
    [CorrespondentAccount]      NVARCHAR (MAX)     NOT NULL,
    [TIN]                       NVARCHAR (MAX)     NOT NULL,
    [TaxRegistrationReasonCode] NVARCHAR (MAX)     NOT NULL,
    [CurrentAccount]            NVARCHAR (MAX)     NOT NULL,
    [Payee]                     NVARCHAR (MAX)     NOT NULL,
    [Shipper]                   NVARCHAR (MAX)     NOT NULL,
    [Head]                      NVARCHAR (MAX)     NOT NULL,
    [Accountant]                NVARCHAR (MAX)     NOT NULL,
    [HeaderText]                NVARCHAR (MAX)     NOT NULL,
    [Client]                    NVARCHAR (MAX)     NOT NULL,
    [Goods]                     NVARCHAR (MAX)     NOT NULL,
    [Count]                     SMALLINT           NOT NULL,
    [Price]                     MONEY              NOT NULL,
    [VAT]                       MONEY              NOT NULL,
    [EuroToRuble]               MONEY              NOT NULL,
    CONSTRAINT [PK_dbo.Bill] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Bill].[IX_Bill_ApplicationId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Bill_ApplicationId]
    ON [dbo].[Bill]([ApplicationId] ASC);


GO
PRINT N'Creating [dbo].[Setting]...';


GO
CREATE TABLE [dbo].[Setting] (
    [Type]       INT             NOT NULL,
    [RowVersion] ROWVERSION      NOT NULL,
    [Data]       VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Setting] PRIMARY KEY CLUSTERED ([Type] ASC)
);


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
PRINT N'Creating FK_dbo.Application_dbo.State_StateId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Forwarder_ForwarderId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Forwarder_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [dbo].[Forwarder] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Client_ClientId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.Transit_TransitId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId] FOREIGN KEY ([TransitId]) REFERENCES [dbo].[Transit] ([Id]);


GO
PRINT N'Creating FK_dbo.Application_dbo.AirWaybill_AirWaybillId...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId] FOREIGN KEY ([AirWaybillId]) REFERENCES [dbo].[AirWaybill] ([Id]);


GO
PRINT N'Altering [dbo].[User_Add]...';


GO
ALTER PROCEDURE [dbo].[User_Add]
	@Login						NVARCHAR(320),
	@PasswordHash				VARBINARY(MAX),
	@PasswordSalt				VARBINARY(MAX),
	@TwoLetterISOLanguageName	CHAR(2)

AS BEGIN
	SET NOCOUNT ON;

	INSERT	[dbo].[User] ([Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
	OUTPUT	INSERTED.[Id]
	VALUES	(@Login, @PasswordHash, @PasswordSalt, @TwoLetterISOLanguageName)

END
GO
PRINT N'Creating [dbo].[Bill_AddOrReplace]...';


GO
CREATE PROCEDURE [dbo].[Bill_AddOrReplace]
	@ApplicationId BIGINT,
	@Number INT,
	@Bank NVARCHAR(MAX),
	@BIC NVARCHAR(MAX),
	@CorrespondentAccount NVARCHAR(MAX),
	@TIN NVARCHAR(MAX),
	@TaxRegistrationReasonCode NVARCHAR(MAX),
	@CurrentAccount NVARCHAR(MAX),
	@Payee NVARCHAR(MAX),
	@Shipper NVARCHAR(MAX),
	@Head NVARCHAR(MAX),
	@Accountant NVARCHAR(MAX),
	@HeaderText NVARCHAR(MAX),
	@Client NVARCHAR(MAX),
	@Goods NVARCHAR(MAX),
	@Count SMALLINT,
	@Price MONEY,
	@VAT MONEY,
	@EuroToRuble MONEY,
	@SaveDate DATETIMEOFFSET,
	@SendDate DATETIMEOFFSET

AS BEGIN
	SET NOCOUNT ON;

	MERGE [dbo].[Bill] AS target
		USING (SELECT @ApplicationId,
					@Number,
					@Bank,
					@BIC,
					@CorrespondentAccount,
					@TIN,
					@TaxRegistrationReasonCode,
					@CurrentAccount,
					@Payee,
					@Shipper,
					@Head,
					@Accountant,
					@HeaderText,
					@Client,
					@Goods,
					@Count,
					@Price,
					@VAT,
					@EuroToRuble,
					@SaveDate,
					@SendDate)
			AS source ([ApplicationId],
					[Number],
					[Bank],
					[BIC],
					[CorrespondentAccount],
					[TIN],
					[TaxRegistrationReasonCode],
					[CurrentAccount],
					[Payee],
					[Shipper],
					[Head],
					[Accountant],
					[HeaderText],
					[Client],
					[Goods],
					[Count],
					[Price],
					[VAT],
					[EuroToRuble],
					[SaveDate],
					[SendDate])
			ON (target.[ApplicationId] = source.[ApplicationId])
		WHEN MATCHED THEN 
			UPDATE SET [Bank] = source.[Bank],
						[Number] = source.[Number],
						[BIC] = source.[BIC],
						[CorrespondentAccount] = source.[CorrespondentAccount],
						[TIN] = source.[TIN],
						[TaxRegistrationReasonCode] = source.[TaxRegistrationReasonCode],
						[CurrentAccount] = source.[CurrentAccount],
						[Payee] = source.[Payee],
						[Shipper] = source.[Shipper],
						[Head] = source.[Head],
						[Accountant] = source.[Accountant],
						[HeaderText] = source.[HeaderText],
						[Client] = source.[Client],
						[Goods] = source.[Goods],
						[Count] = source.[Count],
						[Price] = source.[Price],
						[VAT] = source.[VAT],
						[EuroToRuble] = source.[EuroToRuble],
						[SaveDate] = source.[SaveDate],
						[SendDate] = source.[SendDate]
		WHEN NOT MATCHED THEN
			INSERT ([ApplicationId],
					[Number],
					[Bank],
					[BIC],
					[CorrespondentAccount],
					[TIN],
					[TaxRegistrationReasonCode],
					[CurrentAccount],
					[Payee],
					[Shipper],
					[Head],
					[Accountant],
					[HeaderText],
					[Client],
					[Goods],
					[Count],
					[Price],
					[VAT],
					[EuroToRuble],
					[SaveDate],
					[SendDate])
			VALUES (source.[ApplicationId],
					source.[Number],
					source.[Bank],
					source.[BIC],
					source.[CorrespondentAccount],
					source.[TIN],
					source.[TaxRegistrationReasonCode],
					source.[CurrentAccount],
					source.[Payee],
					source.[Shipper],
					source.[Head],
					source.[Accountant],
					source.[HeaderText],
					source.[Client],
					source.[Goods],
					source.[Count],
					source.[Price],
					source.[VAT],
					source.[EuroToRuble],
					source.[SaveDate],
					source.[SendDate])
		OUTPUT INSERTED.[Id];
END
GO
PRINT N'Creating [dbo].[Bill_GetByApplicationId]...';


GO
CREATE PROCEDURE [dbo].[Bill_GetByApplicationId]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1
		[Bank],
		[BIC],
		[CorrespondentAccount],
		[TIN],
		[TaxRegistrationReasonCode],
		[CurrentAccount],
		[Payee],
		[Shipper],
		[Head],
		[Accountant],
		[HeaderText],
		[Client],
		[Goods],
		[Count],
		[Price],
		[VAT],
		[EuroToRuble],
		[Number],
		[SaveDate],
		[SendDate]
	FROM [dbo].[Bill]
	WHERE [ApplicationId] = @ApplicationId

END
GO
PRINT N'Creating [dbo].[Client_Add]...';


GO
CREATE PROCEDURE [dbo].[Client_Add]
	@UserId BIGINT,
    @Emails NVARCHAR(MAX),
    @Nic NVARCHAR(MAX),
    @LegalEntity NVARCHAR(MAX),
    @Contacts NVARCHAR(MAX),
    @Phone NVARCHAR(MAX),
    @INN NVARCHAR(MAX),
    @KPP NVARCHAR(MAX),
    @OGRN NVARCHAR(MAX),
    @Bank NVARCHAR(MAX),
    @BIC NVARCHAR(MAX),
    @LegalAddress NVARCHAR(MAX),
    @MailingAddress NVARCHAR(MAX),
    @RS NVARCHAR(MAX),
    @KS NVARCHAR(MAX),
    @ContractNumber NVARCHAR(MAX),
    @ContractDate DATETIMEOFFSET,
    @TransitId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Client]
		([UserId]
		,[Emails]
		,[Nic]
		,[LegalEntity]
		,[Contacts]
		,[Phone]
		,[INN]
		,[KPP]
		,[OGRN]
		,[Bank]
		,[BIC]
		,[LegalAddress]
		,[MailingAddress]
		,[RS]
		,[KS]
		,[TransitId]
		,[ContractNumber]
		,[ContractDate])
	OUTPUT INSERTED.[Id]
	VALUES
		(@UserId,
		@Emails,
		@Nic,
		@LegalEntity,
		@Contacts,
		@Phone,
		@INN,
		@KPP,
		@OGRN,
		@Bank,
		@BIC,
		@LegalAddress,
		@MailingAddress,
		@RS,
		@KS,
		@TransitId,
		@ContractNumber,
		@ContractDate)

END
GO
PRINT N'Creating [dbo].[Client_Get]...';


GO
CREATE PROCEDURE [dbo].[Client_Get]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		c.[Id] AS [ClientId],
		u.[Id] AS [UserId],
		c.[Emails],
		c.[Nic],
		c.[LegalEntity],
		c.[Contacts],
		c.[Phone],
		c.[INN],
		c.[KPP],
		c.[OGRN],
		c.[Bank],
		c.[BIC],
		c.[LegalAddress],
		c.[MailingAddress],
		c.[RS],
		c.[KS],
		c.[TransitId],
		c.[Balance],
		u.[Login],
		u.[TwoLetterISOLanguageName] AS [Language],
		c.[ContractDate],
		c.[ContractNumber]
	  FROM [dbo].[Client] c
	  JOIN [dbo].[User] u
	  ON c.[UserId] = u.[Id]
	  WHERE c.[Id] = @ClientId

END
GO
PRINT N'Creating [dbo].[Client_GetAll]...';


GO
CREATE PROCEDURE [dbo].[Client_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT
		c.[Id] AS [ClientId],
		u.[Id] AS [UserId],
		c.[Emails],
		c.[Nic],
		c.[LegalEntity],
		c.[Contacts],
		c.[Phone],
		c.[INN],
		c.[KPP],
		c.[OGRN],
		c.[Bank],
		c.[BIC],
		c.[LegalAddress],
		c.[MailingAddress],
		c.[RS],
		c.[KS],
		c.[TransitId],
		c.[Balance],
		u.[Login],
		u.[TwoLetterISOLanguageName] AS [Language],
		c.[ContractDate],
		c.[ContractNumber]
	  FROM [dbo].[Client] c
	  JOIN [dbo].[User] u
	  ON c.[UserId] = u.[Id]

END
GO
PRINT N'Creating [dbo].[Client_GetByUserId]...';


GO
CREATE PROCEDURE [dbo].[Client_GetByUserId]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		c.[Id] AS [ClientId],
		u.[Id] AS [UserId],
		c.[Emails],
		c.[Nic],
		c.[LegalEntity],
		c.[Contacts],
		c.[Phone],
		c.[INN],
		c.[KPP],
		c.[OGRN],
		c.[Bank],
		c.[BIC],
		c.[LegalAddress],
		c.[MailingAddress],
		c.[RS],
		c.[KS],
		c.[TransitId],
		c.[Balance],
		u.[Login],
		u.[TwoLetterISOLanguageName] AS [Language],
		c.[ContractDate],
		c.[ContractNumber]
	  FROM [dbo].[Client] c
	  JOIN [dbo].[User] u
	  ON c.[UserId] = u.[Id]
	  WHERE u.[Id] = @UserId

END
GO
PRINT N'Creating [dbo].[Client_Update]...';


GO
CREATE PROCEDURE [dbo].[Client_Update]
	@ClientId BIGINT,
    @Emails NVARCHAR(MAX),
    @Nic NVARCHAR(MAX),
    @LegalEntity NVARCHAR(MAX),
    @Contacts NVARCHAR(MAX),
    @Phone NVARCHAR(MAX),
    @INN NVARCHAR(MAX),
    @KPP NVARCHAR(MAX),
    @OGRN NVARCHAR(MAX),
    @Bank NVARCHAR(MAX),
    @BIC NVARCHAR(MAX),
    @LegalAddress NVARCHAR(MAX),
    @MailingAddress NVARCHAR(MAX),
    @RS NVARCHAR(MAX),
    @KS NVARCHAR(MAX),
    @ContractNumber NVARCHAR(MAX),
    @ContractDate DATETIMEOFFSET

AS BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Client]
	SET [Emails] = @Emails,
		[Nic] = @Nic,
		[LegalEntity] = @LegalEntity,
		[Contacts] = @Contacts,
		[Phone] = @Phone,
		[INN] = @INN,
		[KPP] = @KPP,
		[OGRN] = @OGRN,
		[Bank] = @Bank,
		[BIC] = @BIC,
		[LegalAddress] = @LegalAddress,
		[MailingAddress] = @MailingAddress,
		[RS] = @RS,
		[KS] = @KS,
		[ContractNumber] = @ContractNumber,
		[ContractDate] = @ContractDate
	WHERE [Id] = @ClientId

END
GO
PRINT N'Creating [dbo].[Setting_Get]...';


GO
CREATE PROCEDURE [dbo].[Setting_Get]
	@Type INT
AS BEGIN
	SET NOCOUNT ON

	SELECT s.[Data], s.[RowVersion], s.[Type]
	FROM [dbo].[Setting] s
	WHERE s.[Type] = @Type

END
GO
PRINT N'Creating [dbo].[Setting_Merge]...';


GO
CREATE PROCEDURE [dbo].[Setting_Merge]
	@Type INT,
	@RowVersion ROWVERSION,
	@Data VARBINARY(MAX)
AS BEGIN
	SET NOCOUNT ON

	MERGE [dbo].[Setting] AS target
		USING (SELECT @Type, @RowVersion, @Data) AS source ([Type], [RowVersion], [Data])
			ON (target.[RowVersion] = source.[RowVersion] AND target.[Type] = source.[Type])
		WHEN MATCHED THEN 
			UPDATE SET [Data] = source.[Data]
		WHEN NOT MATCHED THEN
			INSERT ([Type], [Data])
			VALUES (source.[Type], source.[Data])
		OUTPUT INSERTED.[RowVersion];

END
GO
PRINT N'Creating [dbo].[User_SetLogin]...';


GO
CREATE PROCEDURE [dbo].[User_SetLogin]
	@UserId BIGINT,
	@Login NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[User]
	SET [Login] = @Login
	WHERE [Id] = @UserId

END
GO
PRINT N'Altering [dbo].[Carrier_Add]...';


GO
ALTER PROCEDURE [dbo].[Carrier_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login,
			@PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @Language

		INSERT [dbo].[Carrier] ([UserId], [Name], [Email], [Contact], [Phone], [Address])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email, @Contact, @Phone, @Address
		FROM @Table

	COMMIT

END
GO
PRINT N'Altering [dbo].[Forwarder_Add]...';


GO
ALTER PROCEDURE [dbo].[Forwarder_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login, @PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @Language

		INSERT [dbo].[Forwarder] ([UserId], [Name], [Email])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email
		FROM @Table

	COMMIT

END
GO
PRINT N'Altering [dbo].[Sender_Add]...';


GO
ALTER PROCEDURE [dbo].[Sender_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@TwoLetterISOLanguageName CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TariffOfTapePerBox MONEY,
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login, @PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @TwoLetterISOLanguageName

		INSERT [dbo].[Sender] ([UserId], [Name], [Email], [TariffOfTapePerBox], [Contact], [Phone], [Address])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email, @TariffOfTapePerBox, @Contact, @Phone, @Address
		FROM @Table

	COMMIT

END
GO
PRINT N'Refreshing [dbo].[Client_DeleteForce]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_DeleteForce]';


GO
PRINT N'Refreshing [dbo].[Transit_GetByApplication]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Transit_GetByApplication]';


GO
PRINT N'Refreshing [dbo].[Client_GetBalance]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_GetBalance]';


GO
PRINT N'Refreshing [dbo].[Client_SetBalance]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_SetBalance]';


GO
PRINT N'Refreshing [dbo].[Client_SumBalance]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Client_SumBalance]';


GO
PRINT N'Refreshing [dbo].[Transit_GetByClient]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Transit_GetByClient]';


GO
PRINT N'Refreshing [dbo].[User_GetUserIdByEmail]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_GetUserIdByEmail]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(MainDbName)] 


GO
ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Sender_SenderId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.State_StateId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Forwarder_ForwarderId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId];

ALTER TABLE [dbo].[Application] WITH CHECK CHECK CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId];

GO
INSERT [dbo].[Setting] ([Type], [Data]) VALUES
(1, 0xEFBBBF7B2242616E6B223A22D090D09AD091205C22D0A0D09ED0A1D095D092D0A0D09ED091D090D09DD09A5C222028D09ED090D09E2920D0932E20D09CD09ED0A1D09AD092D090222C22424943223A22303434353835373737222C22436F72726573706F6E64656E744163636F756E74223A223330313031383130383030303030303030373737222C2254494E223A2237373138393533383230222C22546178526567697374726174696F6E526561736F6E436F6465223A22373731383031303031222C2243757272656E744163636F756E74223A223430373032383130393030303030333430333430222C225061796565223A22D0A2D180D18DD0B9D0B420D098D0BDD0B2D0B5D181D18220D09ED09ED09E222C2253686970706572223A22D0A2D180D18DD0B9D0B420D098D0BDD0B2D0B5D181D18220D09ED09ED09E2C20D098D09DD09D20373731383935333832302C20D09AD09FD09F203737313830313030312C203130373131332C20D09CD0BED181D0BAD0B2D0B020D0B32C20D0A1D0BED0BAD0BED0BBD18CD0BDD0B8D187D0B5D181D0BAD0B0D18F20D0BFD0BBD0BED189D0B0D0B4D18C2C20D0B4D0BED0BC20E2849620342C20D0BAD0BED180D0BFD183D18120612C20D0BED184D0B8D181203330392C20D182D0B5D0BB2E3A203838303035353538353233222C2248656164223A22D09DD0B0D0B4D0B8D0BD20D0AF2ED09D2E222C224163636F756E74616E74223A22D09DD0B0D0B4D0B8D0BD20D0AF2ED09D2E222C2248656164657254657874223A22D092D0BDD0B8D0BCD0B0D0BDD0B8D0B52120D09ED0BFD0BBD0B0D182D0B020D0B4D0B0D0BDD0BDD0BED0B3D0BE20D181D187D0B5D182D0B020D0BED0B7D0BDD0B0D187D0B0D0B5D18220D181D0BED0B3D0BBD0B0D181D0B8D0B520D18120D183D181D0BBD0BED0B2D0B8D18FD0BCD0B820D0BFD0BED181D182D0B0D0B2D0BAD0B820D182D0BED0B2D0B0D180D0B02E20D0A3D0B2D0B5D0B4D0BED0BCD0BBD0B5D0BDD0B8D0B520D0BED0B120D0BED0BFD0BBD0B0D182D0B520D0BED0B1D18FD0B7D0B0D182D0B5D0BBD18CD0BDD0BE2C20D0B220D0BFD180D0BED182D0B8D0B2D0BDD0BED0BC20D181D0BBD183D187D0B0D0B520D0BDD0B520D0B3D0B0D180D0B0D0BDD182D0B8D180D183D0B5D182D181D18F20D0BDD0B0D0BBD0B8D187D0B8D0B520D182D0BED0B2D0B0D180D0B020D0BDD0B020D181D0BAD0BBD0B0D0B4D0B52E20D0A2D0BED0B2D0B0D18020D0BED182D0BFD183D181D0BAD0B0D0B5D182D181D18F20D0BFD0BE20D184D0B0D0BAD182D18320D0BFD180D0B8D185D0BED0B4D0B020D0B4D0B5D0BDD0B5D0B320D0BDD0B020D1802FD18120D09FD0BED181D182D0B0D0B2D189D0B8D0BAD0B02C20D181D0B0D0BCD0BED0B2D18BD0B2D0BED0B7D0BED0BC2C20D0BFD180D0B820D0BDD0B0D0BBD0B8D187D0B8D0B820D0B4D0BED0B2D0B5D180D0B5D0BDD0BDD0BED181D182D0B820D0B820D0BFD0B0D181D0BFD0BED180D182D0B02E222C22564154223A302E31382C224575726F546F5275626C65223A35302E307D)

GO
UPDATE [dbo].[Client]
SET [ContractNumber] = [Id],
	[ContractDate] = GETUTCDATE();

GO
ALTER TABLE [dbo].[Client] ALTER COLUMN [ContractNumber] NVARCHAR (MAX) NOT NULL;
ALTER TABLE [dbo].[Client] ALTER COLUMN [ContractDate] DATETIMEOFFSET (7) NOT NULL;

GO
PRINT N'Update complete.';


GO