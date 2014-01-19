CREATE PROCEDURE [dbo].[Transit_Delete]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[Transit]
	WHERE [Id] = @Id

END
GO