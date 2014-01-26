CREATE PROCEDURE [dbo].[ForwarderCity_Set]
	@CityIds [dbo].[IdsTable] READONLY,
	@ForwarderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[ForwarderCity]
		WHERE [ForwarderId] = @ForwarderId
		AND [CityId] NOT IN (SELECT [Id] FROM @CityIds)
		
		INSERT [dbo].[ForwarderCity] ([CityId], [ForwarderId])
		SELECT [Id] AS [CityId],  @ForwarderId AS [ForwarderId]
		FROM @CityIds
		WHERE [Id] NOT IN (SELECT [CityId] FROM [ForwarderCity] WHERE [ForwarderId] = @ForwarderId)

	COMMIT

END
GO