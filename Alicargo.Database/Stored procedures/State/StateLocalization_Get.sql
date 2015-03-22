CREATE PROCEDURE [dbo].[StateLocalization_Get]
	@Ids [dbo].[IdsTable] READONLY,
	@Localizations [dbo].[StringsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM @Ids)
		SELECT	i.[Id] AS [StateId],
				COALESCE(l.[Name], s.[Name]) AS [Name],
				n.[Value] AS [TwoLetterISOLanguageName]
		FROM @Ids i CROSS JOIN @Localizations n
		JOIN [dbo].[State] s ON s.[Id] = i.[Id]
		LEFT JOIN [dbo].[StateLocalization] l
		ON s.[Id] = l.[StateId] AND l.[TwoLetterISOLanguageName] = n.[Value]
		ORDER BY i.[Id], n.[Value]
	ELSE
		SELECT	s.[Id] AS [StateId],
				COALESCE(l.[Name], s.[Name]) AS [Name],
				n.[Value] AS [TwoLetterISOLanguageName]
		FROM [dbo].[State] s CROSS JOIN @Localizations n
		LEFT JOIN [dbo].[StateLocalization] l
		ON s.[Id] = l.[StateId] AND l.[TwoLetterISOLanguageName] = n.[Value]
		ORDER BY s.[Id], n.[Value]

END
GO