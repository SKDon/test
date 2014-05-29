CREATE PROCEDURE [dbo].[ApplicationFile_Add]
	@ApplicationId BIGINT,
	@TypeId INT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX) 

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[ApplicationFile] ([ApplicationId], [Data], [Name], [TypeId])
	OUTPUT INSERTED.[Id]
	VALUES (@ApplicationId, @Data, @Name, @TypeId)

END
GO