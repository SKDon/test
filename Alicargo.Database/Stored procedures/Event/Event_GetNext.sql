CREATE PROCEDURE [dbo].[Event_GetNext]
	@EventTypeId INT,
	@PartitionId INT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			e.[Id],
			e.[StateId] AS [State],
			e.[Data],
			e.[UserId]
	FROM [dbo].[Event] e
	WHERE e.[EventTypeId] = @EventTypeId AND e.[PartitionId] = @PartitionId AND e.[StateId] <> -1
	ORDER BY e.[Id]

END
GO