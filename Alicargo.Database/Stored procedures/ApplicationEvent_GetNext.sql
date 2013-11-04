CREATE PROCEDURE [dbo].[ApplicationEvent_GetNext]
	@OlderThan DATETIMEOFFSET
AS
	SET NOCOUNT ON;

	SELECT TOP(1) e.[Id], e.[ApplicationId], e.[EventType], e.[RowVersion]
	FROM [dbo].[ApplicationEvent] e
	WHERE e.[UpdateTimestamp] < @OlderThan
	ORDER BY e.[Id]