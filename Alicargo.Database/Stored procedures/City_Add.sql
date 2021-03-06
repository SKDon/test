﻿CREATE PROCEDURE [dbo].[City_Add]
	@EnglishName NVARCHAR(128),
	@RussianName NVARCHAR(128),
	@Position INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[City] ([Name_En], [Name_Ru], [Position])
	OUTPUT INSERTED.[Id]
	VALUES (@EnglishName, @RussianName, @Position)

END
GO