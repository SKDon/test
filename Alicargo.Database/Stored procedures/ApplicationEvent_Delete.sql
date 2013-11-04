CREATE PROCEDURE [dbo].[ApplicationEvent_Delete]
	@Id BIGINT,
	@RowVersion ROWVERSION
AS
	SET NOCOUNT ON;

	DELETE TOP(1)
	FROM [dbo].[ApplicationEvent]
	WHERE [Id] = @Id AND [RowVersion] = @RowVersion