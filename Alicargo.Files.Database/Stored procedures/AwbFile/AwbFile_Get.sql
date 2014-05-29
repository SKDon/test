CREATE PROCEDURE [dbo].[AwbFile_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1) [Name], [Data]
	FROM [dbo].[AwbFile]
	WHERE [Id] = @Id

END
GO