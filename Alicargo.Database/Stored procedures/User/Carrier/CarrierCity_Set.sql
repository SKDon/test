CREATE PROCEDURE [dbo].[CarrierCity_Set]
	@CityIds [dbo].[IdsTable] READONLY,
	@CarrierId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[CarrierCity]
		WHERE [CarrierId] = @CarrierId
		AND [CityId] NOT IN (SELECT [Id] FROM @CityIds)
		
		INSERT [dbo].[CarrierCity] ([CityId], [CarrierId])
		SELECT [Id] AS [CityId],  @CarrierId AS [CarrierId]
		FROM @CityIds
		WHERE [Id] NOT IN (SELECT [CityId] FROM [CarrierCity] WHERE [CarrierId] = @CarrierId)

	COMMIT

END
GO