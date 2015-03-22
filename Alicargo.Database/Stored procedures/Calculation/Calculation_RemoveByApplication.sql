CREATE PROCEDURE [dbo].[Calculation_RemoveByApplication]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	DELETE TOP(1) [dbo].[Calculation]
    WHERE [ApplicationHistoryId] = @ApplicationId

END
GO