CREATE PROCEDURE [dbo].[State_Add]
	@Name  NVARCHAR (320),
	@Position INT,
	@IsSystem BIT
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT [dbo].[State] ([Name], [Position], [IsSystem])
	OUTPUT INSERTED.[Id]
	VALUES (@Name, @Position, @IsSystem)

END
GO