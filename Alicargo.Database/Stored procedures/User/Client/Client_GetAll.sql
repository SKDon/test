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
		c.[ContractNumber],
		C.[DefaultSenderId],
		c.[FactureCost],
		c.[FactureCostEx],
		c.[TransitCost],
		c.[PickupCost],
		c.[InsuranceRate],
		c.[TariffPerKg],
		c.[ScotchCostEdited],
		c.[Comments]
		FROM [dbo].[Client] c
			JOIN [dbo].[User] u
				ON c.[UserId] = u.[Id]

END
GO