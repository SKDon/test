CREATE PROCEDURE [dbo].[Sender_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(8000),
	@PasswordSalt VARBINARY(8000),
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

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login, @PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @TwoLetterISOLanguageName

		INSERT [dbo].[Sender] ([UserId], [Name], [Email], [TariffOfTapePerBox], [Contact], [Phone], [Address])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email, @TariffOfTapePerBox, @Contact, @Phone, @Address
		FROM @Table

	COMMIT

END
GO