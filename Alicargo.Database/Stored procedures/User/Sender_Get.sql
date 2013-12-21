CREATE PROCEDURE [dbo].[Sender_Get]
	@Id BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[Name],
			s.[Email],
			s.[TariffOfTapePerBox],
			u.[Login],
			u.[TwoLetterISOLanguageName]
	FROM	[dbo].[Sender] s
	JOIN	[dbo].[User] u
	ON		u.[Id] = s.[UserId]
	WHERE	s.[Id] = @Id