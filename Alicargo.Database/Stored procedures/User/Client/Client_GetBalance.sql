CREATE PROCEDURE [dbo].[Client_GetBalance]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1) c.[Balance]
		FROM [dbo].[Client] c
			WHERE c.[Id] = @ClientId

END
GO