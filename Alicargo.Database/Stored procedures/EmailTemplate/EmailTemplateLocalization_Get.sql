CREATE PROCEDURE [dbo].[EmailTemplateLocalization_Get]
	@TemplateId BIGINT,
	@Localizations [dbo].[StringsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	l.[Subject],
			l.[Body],
			l.[IsBodyHtml],
			n.[Value] AS [TwoLetterISOLanguageName]
	FROM [dbo].[EmailTemplate] t CROSS JOIN @Localizations n
	LEFT JOIN [dbo].[EmailTemplateLocalization] l
	ON t.[Id] = l.[EmailTemplateId] AND l.[TwoLetterISOLanguageName] = n.[Value]
	WHERE t.[Id] = @TemplateId
	ORDER BY t.[Id], n.[Value]

END
GO