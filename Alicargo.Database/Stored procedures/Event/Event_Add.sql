CREATE PROCEDURE [dbo].[Event_Add]
	@StateId INT,
	@PartitionId INT,
	@EventTypeId INT,
	@Data VARBINARY(MAX)

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Event] ([EventTypeId], [StateId], [Data], [PartitionId])
	VALUES (@EventTypeId, @StateId, @Data, @PartitionId)

END
GO