CREATE PROCEDURE [dbo].[Carrier_Add]
	@Login NVARCHAR(320),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Language CHAR(2),
	@Name NVARCHAR (MAX),
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN

		DECLARE @UserId BIGINT;
		EXEC @UserId = [dbo].[User_Add]
				@Login = @Login,
				@PasswordHash = @PasswordHash, 
				@PasswordSalt = @PasswordSalt, 
				@TwoLetterISOLanguageName = @Language

		INSERT [dbo].[Carrier] ([UserId], [Name], [Email], [Contact], [Phone])
		OUTPUT INSERTED.[Id]
		VALUES (@UserId, @Name, @Email, @Contact, @Phone)

	COMMIT

END
GO