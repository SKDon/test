USE [master] 
GO

ALTER TABLE [dbo].[Application] ADD [DisplayNumber] INT NULL
GO

UPDATE [dbo].[Application]
SET [DisplayNumber] = [Id]
ALTER TABLE [dbo].[Application] ALTER COLUMN [DisplayNumber] INT NOT NULL
GO

CREATE SEQUENCE [dbo].[DisplayNumberCounter]
    AS INT
    START WITH 695
    INCREMENT BY 1
    CYCLE
    CACHE 5;
GO

CREATE PROCEDURE [dbo].[GetNextDisplayNumber]
AS BEGIN
	SET NOCOUNT ON;

	SELECT NEXT VALUE FOR [dbo].[DisplayNumberCounter]

END
GO