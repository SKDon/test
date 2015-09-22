GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application]
    ADD [DocumentWeight] REAL NULL;


GO
PRINT N'Altering [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD [FactureCost]   MONEY NULL,
        [FactureCostEx] MONEY NULL,
        [TransitCost]   MONEY NULL,
        [PickupCost]    MONEY NULL,
        [InsuranceRate] REAL  NULL;


GO
PRINT N'Altering [dbo].[Client_Add]...';


GO
ALTER PROCEDURE [dbo].[Client_Add]
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
    @TransitId BIGINT,
	@DefaultSenderId BIGINT,
	@FactureCost MONEY,
	@FactureCostEx MONEY,
	@TransitCost MONEY,
	@PickupCost MONEY,
	@InsuranceRate REAL	

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
		,[ContractDate]
		,[DefaultSenderId]
		,[FactureCost]
		,[FactureCostEx]
		,[TransitCost]
		,[PickupCost]
		,[InsuranceRate])
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
		@ContractDate,
		@DefaultSenderId,
		@FactureCost,
		@FactureCostEx,
		@TransitCost,
		@PickupCost,
		@InsuranceRate)

END
GO
PRINT N'Altering [dbo].[Client_Get]...';


GO
ALTER PROCEDURE [dbo].[Client_Get]
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
		c.[ContractNumber],
		c.[DefaultSenderId],
		c.[FactureCost],
		c.[FactureCostEx],
		c.[TransitCost],
		c.[PickupCost],
		c.[InsuranceRate]
		FROM [dbo].[Client] c
			JOIN [dbo].[User] u
				ON c.[UserId] = u.[Id]
			WHERE c.[Id] = @ClientId

END
GO
PRINT N'Altering [dbo].[Client_GetAll]...';


GO
ALTER PROCEDURE [dbo].[Client_GetAll]

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
		c.[ContractNumber],
		C.[DefaultSenderId],
		c.[FactureCost],
		c.[FactureCostEx],
		c.[TransitCost],
		c.[PickupCost],
		c.[InsuranceRate]
		FROM [dbo].[Client] c
			JOIN [dbo].[User] u
				ON c.[UserId] = u.[Id]

END
GO
PRINT N'Altering [dbo].[Client_GetByUserId]...';


GO
ALTER PROCEDURE [dbo].[Client_GetByUserId]
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
		c.[ContractNumber],
		C.[DefaultSenderId],
		c.[FactureCost],
		c.[FactureCostEx],
		c.[TransitCost],
		c.[PickupCost],
		c.[InsuranceRate]
		FROM [dbo].[Client] c
			JOIN [dbo].[User] u
				ON c.[UserId] = u.[Id]
			WHERE u.[Id] = @UserId

END
GO
PRINT N'Altering [dbo].[Client_Update]...';


GO
ALTER PROCEDURE [dbo].[Client_Update]
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
    @ContractDate DATETIMEOFFSET,
	@DefaultSenderId BIGINT,
	@FactureCost MONEY,
	@FactureCostEx MONEY,
	@TransitCost MONEY,
	@PickupCost MONEY,
	@InsuranceRate REAL

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
		[ContractDate] = @ContractDate,
		[DefaultSenderId] = @DefaultSenderId,
		[FactureCost] = @FactureCost,
		[FactureCostEx] = @FactureCostEx,
		[TransitCost] = @TransitCost,
		[PickupCost] = @PickupCost,
		[InsuranceRate] = @InsuranceRate
	WHERE [Id] = @ClientId

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
PRINT N'Refreshing [dbo].[ClientBalanceHistory_GetAll]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ClientBalanceHistory_GetAll]';


GO
PRINT N'Refreshing [dbo].[Transit_GetByClient]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Transit_GetByClient]';


GO
PRINT N'Refreshing [dbo].[User_Get]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_Get]';


GO
PRINT N'Refreshing [dbo].[User_GetUserIdByEmail]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[User_GetUserIdByEmail]';


GO
PRINT N'Update complete.';


GO
