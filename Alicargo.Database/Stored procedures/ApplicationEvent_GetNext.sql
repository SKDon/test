CREATE PROCEDURE [dbo].[ApplicationEvent_GetNext]
AS
	SET NOCOUNT ON;

	SELECT TOP(1) e.[Id], e.[ApplicationId], e.[EventType], e.[RowVersion]
	FROM [dbo].[ApplicationEvent] e
	ORDER BY e.[Id]