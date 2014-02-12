CREATE PROCEDURE [dbo].[Sender_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			s.[Name],
			s.[Email],
			s.[TariffOfTapePerBox],
			u.[Login],
			s.[Contact],
			s.[Phone],
			s.[Address],
			u.[TwoLetterISOLanguageName] AS [Language]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]
	WHERE	s.[Id] = @Id

END
GO