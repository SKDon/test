CREATE PROCEDURE [dbo].[Setting_Get]
	@Type INT
AS BEGIN
	SET NOCOUNT ON

	SELECT s.[Data], s.[RowVersion], s.[Type]
	FROM [dbo].[Setting] s
	WHERE s.[Type] = @Type

END
GO