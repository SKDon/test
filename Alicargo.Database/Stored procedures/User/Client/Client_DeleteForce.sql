CREATE PROCEDURE [dbo].[Client_DeleteForce]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DECLARE	@UserId BIGINT, @TransitId BIGINT;
	SELECT TOP(1) @UserId = c.[UserId], @TransitId = c.[TransitId]
		FROM [dbo].[Client] c
			WHERE c.Id = @ClientId

	BEGIN TRAN
		
		DELETE
		FROM	[dbo].[Application]
		WHERE	[ClientId] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Client]
		WHERE	[Id] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Transit]
		WHERE	[Id] = @TransitId

		DELETE	TOP(1)
		FROM	[dbo].[User]
		WHERE	[Id] = @UserId

	COMMIT
END