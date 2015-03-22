CREATE PROCEDURE [dbo].[Carrier_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(8000),
	@PasswordSalt VARBINARY(8000),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @Table TABLE([UserId] BIGINT);
		INSERT @Table EXEC [dbo].[User_Add]
			@Login = @Login,
			@PasswordHash = @PasswordHash, 
			@PasswordSalt = @PasswordSalt, 
			@TwoLetterISOLanguageName = @Language

		INSERT [dbo].[Carrier] ([UserId], [Name], [Email], [Contact], [Phone], [Address])
		OUTPUT INSERTED.[Id]
		SELECT [UserId], @Name, @Email, @Contact, @Phone, @Address
		FROM @Table

	COMMIT

END
GO