CREATE PROCEDURE [dbo].[Client_SetBalance]
	@ClientId BIGINT,
	@Balance MONEY

AS BEGIN
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[Client]
		SET [Balance] = @Balance
		WHERE [Id] = @ClientId

END
GO