CREATE PROCEDURE [dbo].[ClientBalanceHistory_Add]
	@ClientId BIGINT,
	@Balance MONEY,
	@Input MONEY,
	@Comment NVARCHAR(MAX),
	@Timestamp DATETIMEOFFSET
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT [dbo].[ClientBalanceHistory] ([Balance], [Comment], [ClientId], [Input], [Timestamp])
	VALUES (@Balance, @Comment, @ClientId, @Input, @Timestamp)

END
GO