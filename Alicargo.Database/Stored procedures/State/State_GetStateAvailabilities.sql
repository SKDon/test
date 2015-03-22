CREATE PROCEDURE [dbo].[State_GetStateAvailabilities]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT s.[RoleId] AS [Role], s.[StateId]
	FROM [dbo].[StateAvailability] s
	ORDER BY s.[RoleId], s.[StateId]

END
GO