CREATE PROCEDURE [dbo].[StateLocalization_Get]
	@IDs [dbo].[IdsTable] READONLY,
	@Localizations [dbo].[StringsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	i.[Id] AS [StateId],
			COALESCE(l.[Name], s.[Name]) AS [Name],
			n.[Value] AS [TwoLetterISOLanguageName]
	FROM @IDs i CROSS JOIN @Localizations n
	JOIN [dbo].[State] s ON s.[Id] = i.[Id]
	LEFT JOIN [dbo].[StateLocalization] l
	ON s.[Id] = l.[StateId] AND l.[TwoLetterISOLanguageName] = n.[Value]
	ORDER BY i.[Id], n.[Value]

END
GO