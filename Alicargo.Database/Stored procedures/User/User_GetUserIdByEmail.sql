CREATE PROCEDURE [dbo].[User_GetUserIdByEmail]
	@Email NVARCHAR (320)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT COALESCE(
		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Admin] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Manager] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Client] a ON a.[UserId] = u.[Id]
		WHERE a.[Emails] LIKE @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Sender] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Broker] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Carrier] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email),

		(SELECT TOP 1 u.[Id]
		FROM [dbo].[User] u
		JOIN [dbo].[Forwarder] a ON a.[UserId] = u.[Id]
		WHERE a.[Email] = @Email)) AS [Id]

END
GO