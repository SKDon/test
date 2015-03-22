CREATE PROCEDURE [dbo].[EmailTemplate_GetByEvent]
	@EventTypeId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [EmailTemplateId], [EnableEmailSend]
	FROM [dbo].[EventEmailTemplate]
	WHERE [EventTypeId] = @EventTypeId

END
GO