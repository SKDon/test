CREATE PROCEDURE [dbo].[ApplicationEventEmailRecipient_Get]
	@EventTypeId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [RoleId]
	FROM [dbo].[ApplicationEventEmailRecipient]
	WHERE [EventTypeId] = @EventTypeId
END
GO