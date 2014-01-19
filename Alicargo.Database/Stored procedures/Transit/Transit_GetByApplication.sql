CREATE PROCEDURE [dbo].[Transit_GetByApplication]
	@ApplicationId BIGINT

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
	  JOIN [dbo].[Application] a
		ON t.[Id] = a.[TransitId]
	 WHERE a.[Id] = @ApplicationId

END
GO