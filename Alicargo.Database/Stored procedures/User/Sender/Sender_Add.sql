CREATE PROCEDURE [dbo].[Sender_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@TwoLetterISOLanguageName CHAR(2),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TariffOfTapePerBox MONEY,
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC	@UserId = [dbo].[User_Add]
				@Login = @Login, @PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @TwoLetterISOLanguageName

		INSERT	[dbo].[Sender] ([UserId], [Name], [Email], [TariffOfTapePerBox], [Contact], [Phone], [Address])
		OUTPUT	INSERTED.[Id]
		VALUES	(@UserId, @Name, @Email, @TariffOfTapePerBox, @Contact, @Phone, @Address)

	COMMIT

END
GO