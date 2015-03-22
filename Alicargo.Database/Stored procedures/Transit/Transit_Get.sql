CREATE PROCEDURE [dbo].[Transit_Get]
	@Ids [dbo].[IdsTable] READONLY

AS BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
		  ,[CityId]
		  ,[Address]
		  ,[RecipientName]
		  ,[Phone]
		  ,[WarehouseWorkingTime]
		  ,[MethodOfTransitId] AS [MethodOfTransit]
		  ,[DeliveryTypeId] AS [DeliveryType]
		  ,[CarrierId]
	  FROM [dbo].[Transit]
	 WHERE [Id] IN (SELECT [Id] FROM @Ids)

END
GO