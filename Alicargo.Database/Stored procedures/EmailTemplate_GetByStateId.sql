CREATE PROCEDURE [dbo].[EmailTemplate_GetByStateId]
	@StateId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT t.[Id], st.[EnableEmailSend]
	FROM [dbo].[EmailTemplate] t
	JOIN [dbo].[StateEmailTemplate] st
	ON t.[Id] = st.[EmailTemplateId]
	AND st.[StateId] = @StateId

END