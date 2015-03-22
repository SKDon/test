CREATE PROCEDURE [dbo].[EventEmailRecipient_Get]
	@EventTypeId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [RoleId]
	FROM [dbo].[EventEmailRecipient]
	WHERE [EventTypeId] = @EventTypeId
END
GO