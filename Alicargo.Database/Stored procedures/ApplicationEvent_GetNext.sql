CREATE PROCEDURE [dbo].[ApplicationEvent_GetNext]
AS
	SET NOCOUNT ON;

	SELECT TOP(1) e.[Id], e.[UpdateTimestamp], e.[ApplicationId], e.[EventType]
	FROM [dbo].[SenderApplicationEvent] e
	ORDER BY e.[Id]