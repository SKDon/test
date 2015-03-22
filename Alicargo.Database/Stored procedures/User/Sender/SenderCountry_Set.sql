CREATE PROCEDURE [dbo].[SenderCountry_Set]
	@CountryIds [dbo].[IdsTable] READONLY,
	@SenderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[SenderCountry]
		WHERE [SenderId] = @SenderId
		AND [CountryId] NOT IN (SELECT [Id] FROM @CountryIds)
		
		INSERT [dbo].[SenderCountry] ([CountryId], [SenderId])
		SELECT [Id] AS [CountryId],  @SenderId AS [SenderId]
		FROM @CountryIds
		WHERE [Id] NOT IN (SELECT [CountryId] FROM [SenderCountry] WHERE [SenderId] = @SenderId)

	COMMIT

END
GO