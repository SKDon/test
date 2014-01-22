CREATE PROCEDURE [dbo].[Forwarder_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@CityId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC	@UserId = [dbo].[User_Add]
				@Login = @Login, @PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @Language

		INSERT	[dbo].[Forwarder]
				([UserId], [Name], [Email], [CityId])
		OUTPUT	INSERTED.[Id]
		VALUES	(@UserId, @Name, @Email, @CityId)

	COMMIT

END
GO