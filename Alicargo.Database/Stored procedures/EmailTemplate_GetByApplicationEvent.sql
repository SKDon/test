CREATE PROCEDURE [dbo].[EmailTemplate_GetByApplicationEvent]
	@EventTypeId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [EmailTemplateId], [EnableEmailSend]
	FROM [dbo].[ApplicationEventEmailTemplate]
	WHERE [EventTypeId] = @EventTypeId

END