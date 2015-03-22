CREATE PROCEDURE [dbo].[Forwarder_Get]
	@Id BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			f.[Id],
			u.[Id] AS [UserId],
			f.[Email],
			u.[TwoLetterISOLanguageName] AS [Language],
			u.[Login],
			f.[Name]
	FROM	[dbo].[Forwarder] f
	JOIN	[dbo].[User] u
	ON		u.[Id] = f.[UserId]
	WHERE	f.[Id] = @Id

END
GO