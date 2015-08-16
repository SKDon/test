CREATE PROCEDURE [dbo].[ClientBalanceHistory_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT h.[Balance], h.[Comment], h.[Timestamp], h.[Money], h.[EventTypeId] AS [EventType], c.[Nic] AS [ClientNic]
		FROM [dbo].[ClientBalanceHistory] h
			JOIN [dbo].[Client] c
				ON c.[Id] = h.[ClientId]
		ORDER BY h.[Id] DESC

END
GO