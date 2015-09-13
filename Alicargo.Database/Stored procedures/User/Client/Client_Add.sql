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
    @TransitId BIGINT,
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
		,[InsuranceRate]
		,[TariffPerKg]
		,[ScotchCostEdited]
		,[Comments])
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
		@InsuranceRate,
		@TariffPerKg,
		@ScotchCostEdited,
		@Comments)

END
GO