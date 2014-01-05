CREATE PROCEDURE [dbo].[Calculation_GetCalculatedSum]

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @Result MONEY;

	SELECT @Result
		= SUM([Weight] * [TariffPerKg])
		+ SUM([ScotchCost])
		+ SUM([InsuranceCost])
		+ SUM([FactureCost])
		+ SUM([TransitCost])
		+ SUM([PickupCost])
	FROM [dbo].[Calculation]

	SELECT COALESCE(@Result, 0);

END
GO