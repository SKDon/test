CREATE PROCEDURE [dbo].[Client_SumBalance]

AS BEGIN
	SET NOCOUNT ON;

	SELECT SUM(c.[Balance])
		FROM [dbo].[Client] c

END
GO