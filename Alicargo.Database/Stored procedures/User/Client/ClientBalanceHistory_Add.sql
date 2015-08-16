CREATE PROCEDURE [dbo].[ClientBalanceHistory_Add]
	@ClientId BIGINT,
	@Balance MONEY,
	@Money MONEY,
	@Comment NVARCHAR(MAX),
	@Timestamp DATETIMEOFFSET,
	@EventTypeId INT,
	@IsCalculation BIT

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[ClientBalanceHistory]
		([Balance], [Comment], [ClientId], [Money], [Timestamp], [EventTypeId], [IsCalculation])
		VALUES (@Balance, @Comment, @ClientId, @Money, @Timestamp, @EventTypeId, @IsCalculation)

END
GO