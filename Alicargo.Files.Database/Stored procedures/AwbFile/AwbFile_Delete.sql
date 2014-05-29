CREATE PROCEDURE [dbo].[AwbFile_Delete]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[AwbFile]
	WHERE [Id] = @Id

END
GO