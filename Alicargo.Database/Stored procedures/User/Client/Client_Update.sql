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
    @ContractDate DATETIMEOFFSET,
	@DefaultSenderId BIGINT,
	@FactureCost MONEY,
	@FactureCostEx MONEY,
	@TransitCost MONEY,
	@PickupCost MONEY,
	@InsuranceRate REAL,
	@TariffPerKg MONEY,
	@ScotchCostEdited MONEY,
	@Comments NVARCHAR(MAX)

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
			[InsuranceRate] = @InsuranceRate,
			[TariffPerKg] = @TariffPerKg,
			[ScotchCostEdited] = @ScotchCostEdited,
			[Comments] = @Comments
		WHERE [Id] = @ClientId

END
GO