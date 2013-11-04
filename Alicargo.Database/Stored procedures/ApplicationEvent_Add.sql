CREATE PROCEDURE [dbo].[ApplicationEvent_Add]
	@ApplicationId BIGINT,
	@EventType INT
AS
	SET NOCOUNT ON;

	INSERT [dbo].[ApplicationEvent] ([ApplicationId], [EventType])
	OUTPUT [INSERTED].[RowVersion]
	VALUES (@ApplicationId, @EventType)