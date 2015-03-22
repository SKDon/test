CREATE PROCEDURE [dbo].[Sender_GetAll]

AS BEGIN
	SET NOCOUNT ON;

	SELECT	s.[Id] AS [EntityId],
			u.[Id] AS [UserId],
			s.[Name],
			u.[Login],
			s.[Email],
			s.[Contact],
			s.[Phone],
			s.[Address],
			u.[TwoLetterISOLanguageName] AS [Language]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]

END
GO