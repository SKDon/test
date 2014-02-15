CREATE PROCEDURE [dbo].[Carrier_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT	c.[Id],
			u.[Id] AS [UserId],
			c.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			c.[Name],
			c.[Contact],
			c.[Phone],
			c.[Address]
	FROM	[dbo].[Carrier] c
	JOIN	[dbo].[User] u
	ON		u.[Id] = c.[UserId]

END
GO