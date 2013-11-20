CREATE PROCEDURE [dbo].[State_GetStateEmailRecipients]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT s.[RoleId] AS [Role], s.[StateId]
	FROM [dbo].[StateEmailRecipient] s
	ORDER BY s.[RoleId], s.[StateId]

END