CREATE PROCEDURE [dbo].[ApplicationFile_Delete]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[ApplicationFile]
	WHERE [Id] = @Id

END
GO