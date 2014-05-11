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
		,[TransitId])
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
		@TransitId)

END
GO
