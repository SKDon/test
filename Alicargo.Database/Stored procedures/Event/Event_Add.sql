CREATE PROCEDURE [dbo].[Event_Add]
	@StateId INT,
	@PartitionId INT,
	@EventTypeId INT,
	@Data VARBINARY(MAX),
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Event] ([EventTypeId], [StateId], [Data], [PartitionId], [UserId])
	VALUES (@EventTypeId, @StateId, @Data, @PartitionId, @UserId)

END
GO