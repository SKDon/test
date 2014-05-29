CREATE PROCEDURE [dbo].[AwbFile_Add]
	@AwbId BIGINT,
	@TypeId INT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[AwbFile] ([AwbId], [Data], [Name], [TypeId])
	OUTPUT INSERTED.[Id]
	VALUES (@AwbId, @Data, @Name, @TypeId)

END
GO