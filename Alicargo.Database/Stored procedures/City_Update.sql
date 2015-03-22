CREATE PROCEDURE [dbo].[City_Update]
	@EnglishName NVARCHAR(128),
	@RussianName NVARCHAR(128),
	@Position INT,
	@Id BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE TOP (1) [dbo].[City]
	SET [Name_En] = @EnglishName,
		[Name_Ru] = @RussianName,
		[Position] = @Position
	WHERE [Id] = @Id

END
GO