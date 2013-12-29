CREATE PROCEDURE [dbo].[Event_Delete]
	@Id BIGINT
AS
	SET NOCOUNT ON;

	DELETE TOP(1)
	FROM [dbo].[ApplicationEvent]
	WHERE [Id] = @Id

GO