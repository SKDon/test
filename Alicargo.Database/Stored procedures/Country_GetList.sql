CREATE PROCEDURE [dbo].[Country_GetList]
	@Language CHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT c.[Id], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]) AS [Name], c.[Position]
	FROM [dbo].[Country] c
	ORDER BY c.[Position], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]), c.[Id]

END
GO