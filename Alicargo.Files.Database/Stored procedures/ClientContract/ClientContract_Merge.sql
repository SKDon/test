CREATE PROCEDURE [dbo].[ClientContract_Merge]
	@ClientId BIGINT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	MERGE	[dbo].[ClientContract] AS target

	USING	(VALUES (@ClientId, @Name, @Data))
			AS source ([ClientId], [Name], [Data])

		ON	target.[ClientId] = source.[ClientId]

	WHEN MATCHED AND source.[Name] IS NULL THEN
		DELETE

	WHEN MATCHED THEN
		UPDATE SET [Name] = Source.[Name], [Data] = source.[Data]

	WHEN NOT MATCHED THEN
		INSERT ([ClientId], [Name], [Data]) 
		VALUES (source.[ClientId], source.[Name], source.[Data]);
END
GO