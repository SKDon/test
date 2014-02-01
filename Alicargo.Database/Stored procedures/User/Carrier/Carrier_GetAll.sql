CREATE PROCEDURE [dbo].[Carrier_GetAll]
AS BEGIN
	SET NOCOUNT ON;

	SELECT	f.[Id],
			u.[Id] AS [UserId],
			f.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			f.[Name]
	FROM	[dbo].[Carrier] f
	JOIN	[dbo].[User] u
	ON		u.[Id] = f.[UserId]

END
GO