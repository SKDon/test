CREATE PROCEDURE [dbo].[State_Delete]
	@Id BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[State]
	WHERE [Id] = @Id
	AND [IsSystem] = 0

END
GO