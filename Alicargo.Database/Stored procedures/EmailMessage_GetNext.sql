CREATE PROCEDURE [dbo].[EmailMessage_GetNext]
	@State INT,
	@PartitionId INT
AS
	SET NOCOUNT ON;

	SELECT	TOP(1)
			m.[Id],
			m.[From],
			m.[To],
			m.[CopyTo],
			m.[Subject],
			m.[Body],
			m.[IsBodyHtml],
			m.[Files],
			m.[EmailSenderUserId]
	FROM [dbo].[EmailMessage] m
	WHERE m.[StateId] = @State AND m.[PartitionId] = @PartitionId
	ORDER BY m.[Id]

GO