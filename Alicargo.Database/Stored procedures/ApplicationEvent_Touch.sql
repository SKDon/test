CREATE PROCEDURE [dbo].[ApplicationEvent_Touch]
	@Id BIGINT,
	@RowVersion ROWVERSION
AS
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[ApplicationEvent]
	SET [UpdateTimestamp] = GETUTCDATE()
	OUTPUT [INSERTED].[RowVersion]
	WHERE [Id] = @Id AND [RowVersion] = @RowVersion