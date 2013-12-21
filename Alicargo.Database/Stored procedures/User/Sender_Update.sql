CREATE PROCEDURE [dbo].[Sender_Update]
	@Id					BIGINT,
	@Login				NVARCHAR(320),
	@Name				NVARCHAR (MAX),
	@Email				NVARCHAR (320),
	@TariffOfTapePerBox	MONEY
AS
	SET NOCOUNT ON;

	DECLARE @Table TABLE(UserId BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Sender]
		SET		[Name] = @Name,
				[Email] = @Email,
				[TariffOfTapePerBox] = @TariffOfTapePerBox
		OUTPUT	inserted.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login
		WHERE	[Id] IN(SELECT [UserId] FROM @Table);

	COMMIT