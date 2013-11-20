CREATE PROCEDURE [dbo].[State_Add]
	@Id BIGINT,
	@Name  NVARCHAR (320),
	@Position INT
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE [dbo].[State] ([Name], [Position])
	SET [Name] = @Name, [Position] = @Position
	WHERE [Id] = @Id

END
GO