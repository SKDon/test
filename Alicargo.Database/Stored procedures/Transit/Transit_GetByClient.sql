CREATE PROCEDURE [dbo].[Transit_GetByClient]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT t.[Id]
		  ,t.[CityId]
		  ,t.[Address]
		  ,t.[RecipientName]
		  ,t.[Phone]
		  ,t.[WarehouseWorkingTime]
		  ,t.[MethodOfTransitId] AS [MethodOfTransit]
		  ,t.[DeliveryTypeId] AS [DeliveryType]
		  ,t.[CarrierId]
	  FROM [dbo].[Transit] t
	  JOIN [dbo].[Client] c
		ON t.[Id] = c.[TransitId]
	 WHERE c.[Id] = @ClientId

END
GO