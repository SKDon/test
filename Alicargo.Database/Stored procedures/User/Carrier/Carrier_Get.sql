CREATE PROCEDURE [dbo].[Carrier_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			c.[Id],
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
	WHERE	c.[Id] = @Id

END
GO