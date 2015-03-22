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