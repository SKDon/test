CREATE PROCEDURE [dbo].[Event_GetNext]
	@State INT,
	@ShardIndex INT,
	@ShardCount INT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			e.[Id],
			e.[ApplicationId],
			e.[EventTypeId] AS EventType,
			e.[Data]
	FROM [dbo].[Event] e
	WHERE e.[StateId] = @State AND e.[ApplicationId] % @ShardCount = @ShardIndex
	ORDER BY e.[Id]

END
GO