CREATE PROCEDURE [dbo].[EmailTemplate_GetByStateId]
	@StateId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT st.[EmailTemplateId], st.[EnableEmailSend], st.[UseApplicationEventTemplate]
	FROM  [dbo].[StateEmailTemplate] st
	WHERE st.[StateId] = @StateId

END
GO