CREATE PROCEDURE [dbo].[Sender_Update]
	@Id BIGINT,
	@Login NVARCHAR(320),
	@Name NVARCHAR (MAX),
	@Email NVARCHAR (320),
	@TwoLetterISOLanguageName CHAR(2),
	@TariffOfTapePerBox	MONEY,
	@Contact NVARCHAR (MAX),
	@Phone NVARCHAR (MAX),
	@Address NVARCHAR (MAX)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @Table TABLE ([UserId] BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Sender]
		SET		[Name] = @Name,
				[Email] = @Email,
				[TariffOfTapePerBox] = @TariffOfTapePerBox,
				[Contact] = @Contact,
				[Phone] = @Phone,
				[Address] = @Address
		OUTPUT	inserted.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login,
				[TwoLetterISOLanguageName] = @TwoLetterISOLanguageName
		WHERE	[Id] IN (SELECT [UserId] FROM @Table);

	COMMIT

END
GO