CREATE PROCEDURE [dbo].[User_Get]
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		u.[Id],
		[TwoLetterISOLanguageName] AS [Language],
		COALESCE(a.[Name], cl.[Nic], s.[Name], f.[Name], b.[Name], cr.[Name], m.[Name], u.[Login]) AS Name
		FROM [dbo].[User] u
			LEFT JOIN [dbo].[Admin] a ON u.[Id] = a.[UserId]
			LEFT JOIN [dbo].[Carrier] cr ON u.[Id] = cr.[UserId]
			LEFT JOIN [dbo].[Client] cl ON u.[Id] = cl.[UserId]
			LEFT JOIN [dbo].[Forwarder] f ON u.[Id] = f.[UserId]
			LEFT JOIN [dbo].[Sender] s ON u.[Id] = s.[UserId]
			LEFT JOIN [dbo].[Broker] b ON u.[Id] = b.[UserId]
			LEFT JOIN [dbo].[Manager] m ON u.[Id] = m.[UserId]
		WHERE u.[Id] = @UserId

END
GO