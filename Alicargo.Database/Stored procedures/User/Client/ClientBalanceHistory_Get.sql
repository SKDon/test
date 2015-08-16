CREATE PROCEDURE [dbo].[ClientBalanceHistory_Get]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [Balance], [Comment], [Timestamp], [Money], [EventTypeId] AS [EventType], [IsCalculation]
		FROM [dbo].[ClientBalanceHistory]
			WHERE [ClientId] = @ClientId
		ORDER BY [Id] DESC

END
GO