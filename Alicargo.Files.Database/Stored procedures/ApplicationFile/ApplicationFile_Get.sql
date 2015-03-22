CREATE PROCEDURE [dbo].[ApplicationFile_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1) [Name], [Data]
	FROM [dbo].[ApplicationFile]
	WHERE [Id] = @Id

END
GO