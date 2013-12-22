CREATE PROCEDURE [dbo].[ClientBalanceHistory_Get]
	@ClientId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT [Balance], [Comment], [Timestamp], [Input]
	FROM [dbo].[ClientBalanceHistory]
	WHERE [ClientId] = @ClientId

END
GO