CREATE PROCEDURE [dbo].[EmailMessage_SetState]
	@Id BIGINT,
	@State INT
AS
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[EmailMessage]
	SET [StateId] = @State, [StateIdTimestamp] = GETUTCDATE()
	WHERE [Id] = @Id

GO