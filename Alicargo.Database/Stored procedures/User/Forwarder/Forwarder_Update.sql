CREATE PROCEDURE [dbo].[Forwarder_Update]
	@Id BIGINT,
	@Name NVARCHAR (MAX),
	@Login NVARCHAR(320),
	@Email NVARCHAR (320)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @Table TABLE ([UserId] BIGINT);

	BEGIN TRAN

		UPDATE	TOP(1) [dbo].[Forwarder]
		SET		[Name] = @Name,
				[Email] = @Email
		OUTPUT	INSERTED.[UserId] INTO @Table
		WHERE	[Id] = @Id

		UPDATE	TOP(1) [dbo].[User]
		SET		[Login] = @Login
		WHERE	[Id] IN (SELECT [UserId] FROM @Table);

	COMMIT

END
GO