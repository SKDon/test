CREATE PROCEDURE [dbo].[State_GetOrderedList]
	@Ids [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM @Ids)
		SELECT s.[Id], s.[Name], s.[Position], s.[IsSystem]
		FROM [dbo].[State] s
		WHERE s.[Id] IN (SELECT [Id] FROM @Ids)
		ORDER BY s.[Position], s.[Name], s.[Id]
	ELSE
		SELECT s.[Id], s.[Name], s.[Position], s.[IsSystem]
		FROM [dbo].[State] s
		ORDER BY s.[Position], s.[Name], s.[Id]

END
GO