CREATE PROCEDURE [dbo].[Event_Add]
	@State INT,
	@ApplicationId BIGINT,
	@EventType INT,
	@Data VARBINARY(MAX)
AS
	SET NOCOUNT ON;

	INSERT [dbo].[ApplicationEvent] ([ApplicationId], [EventTypeId], [StateId], [Data])
	VALUES (@ApplicationId, @EventType, @State, @Data)

GO