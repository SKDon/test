CREATE PROCEDURE [dbo].[ClientBalanceHistory_Add]
	@ClientId BIGINT,
	@Balance MONEY,
	@Input MONEY,
	@Comment NVARCHAR(MAX)
AS
BEGIN
	
	SET NOCOUNT ON;

	INSERT [dbo].[ClientBalanceHistory] ([Balance], [Comment], [ClientId], [Input])
	VALUES (@Balance, @Comment, @ClientId, @Input)

END
GO