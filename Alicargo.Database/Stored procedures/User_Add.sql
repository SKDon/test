CREATE PROCEDURE [dbo].[User_Add]
	@Login						NVARCHAR(320),
	@PasswordHash				VARBINARY(MAX),
	@PasswordSalt				VARBINARY(MAX),
	@TwoLetterISOLanguageName	CHAR(2)
AS
	SET NOCOUNT ON;

	DECLARE @Table TABLE([UserId] BIGINT);
	DECLARE @UserId	BIGINT;

	INSERT	[dbo].[User] ([Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
	OUTPUT	inserted.[Id] INTO @Table
	VALUES	(@Login, @PasswordHash, @PasswordSalt, @TwoLetterISOLanguageName)

	SELECT TOP 1 @UserId = [UserId] FROM @Table
	RETURN @UserId