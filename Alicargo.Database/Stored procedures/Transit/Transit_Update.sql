CREATE PROCEDURE [dbo].[Transit_Update]
	@Id BIGINT,
	@CityId BIGINT,
	@CarrierId BIGINT,
	@Address NVARCHAR(MAX),	
	@RecipientName NVARCHAR(MAX),
	@Phone NVARCHAR(MAX),
	@MethodOfTransit INT,
	@DeliveryType INT,
	@WarehouseWorkingTime NVARCHAR(MAX)

AS BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Transit]
	   SET [CityId] = @CityId
		  ,[Address] = @Address
		  ,[RecipientName] = @RecipientName
		  ,[Phone] = @Phone
		  ,[WarehouseWorkingTime] = @WarehouseWorkingTime
		  ,[MethodOfTransitId] = @MethodOfTransit
		  ,[DeliveryTypeId] = @DeliveryType
		  ,[CarrierId] = @CarrierId
	 WHERE [Id] = @Id
 
END
GO