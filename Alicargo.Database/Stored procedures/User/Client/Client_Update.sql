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
    @KS NVARCHAR(MAX)

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
		[KS] = @KS
	WHERE [Id] = @ClientId

END
GO