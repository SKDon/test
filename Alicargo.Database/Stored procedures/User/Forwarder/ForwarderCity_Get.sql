CREATE PROCEDURE [dbo].[ForwarderCity_Get]
	@ForwarderId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [CityId]
	FROM [dbo].[ForwarderCity]
	WHERE [ForwarderId] = @ForwarderId

END
GO