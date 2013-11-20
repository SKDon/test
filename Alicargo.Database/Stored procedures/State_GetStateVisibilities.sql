CREATE PROCEDURE [dbo].[State_GetStateVisibilities]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT s.[RoleId], s.[StateId]
	FROM [dbo].[StateVisibility] s
	ORDER BY s.[RoleId], s.[StateId]

END