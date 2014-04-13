CREATE PROCEDURE [dbo].[GetNextDisplayNumber]
AS BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[DisplayNumberCounter]	
	SET [Number] = [Number] + 1
	OUTPUT INSERTED.[Number]

END
GO