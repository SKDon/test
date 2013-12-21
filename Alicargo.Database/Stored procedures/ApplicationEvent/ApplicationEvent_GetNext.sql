CREATE PROCEDURE [dbo].[ApplicationEvent_GetNext]
	@State INT,
	@Index INT,
	@Total INT
AS
	SET NOCOUNT ON;

	SELECT TOP(1) 
			e.[Id],
			e.[ApplicationId],
			e.[EventTypeId] AS EventType,
			e.[Data]
	FROM [dbo].[ApplicationEvent] e
	WHERE e.[StateId] = @State AND e.[ApplicationId] % @Total = @Index
	ORDER BY e.[Id]

GO