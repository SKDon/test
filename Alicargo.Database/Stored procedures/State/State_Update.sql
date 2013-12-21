CREATE PROCEDURE [dbo].[State_Update]
	@Id BIGINT,
	@Name  NVARCHAR (320),
	@Position INT
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [dbo].[State]
	SET [Name] = @Name, [Position] = @Position
	WHERE [Id] = @Id

END
GO