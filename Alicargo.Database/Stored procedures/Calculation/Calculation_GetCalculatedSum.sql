CREATE PROCEDURE [dbo].[Calculation_GetCalculatedSum]

AS BEGIN
	SET NOCOUNT ON;

	SELECT SUM([Weight] * [TariffPerKg])
		+ SUM([ScotchCost])
		+ SUM([InsuranceCost])
		+ SUM([FactureCost])
		+ SUM([TransitCost])
		+ SUM([PickupCost])
	FROM [dbo].[Calculation]

END
GO