CREATE PROCEDURE [dbo].[Transit_Add]
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

	INSERT INTO [dbo].[Transit]
           ([CityId]
           ,[Address]
           ,[RecipientName]
           ,[Phone]
           ,[WarehouseWorkingTime]
           ,[MethodOfTransitId]
           ,[DeliveryTypeId]
           ,[CarrierId])
	OUTPUT INSERTED.[Id]
	VALUES (@CityId
           ,@Address
           ,@RecipientName
           ,@Phone
           ,@WarehouseWorkingTime
           ,@MethodOfTransit
           ,@DeliveryType
           ,@CarrierId)

END
GO