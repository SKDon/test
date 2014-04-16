CREATE PROCEDURE [dbo].[GetNextDisplayNumber]
AS BEGIN
	SET NOCOUNT ON;

	SELECT NEXT VALUE FOR [dbo].[DisplayNumberCounter]

END
GO