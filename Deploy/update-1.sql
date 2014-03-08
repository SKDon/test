USE [master] 
GO

ALTER PROCEDURE [dbo].[sp_RestoreDatabase]
	@newDb NVARCHAR(50),
	@oldDb NVARCHAR(50),
	@fromFile NVARCHAR(200),
	@dataFolder NVARCHAR(200)

AS BEGIN
	SET NOCOUNT ON;

	DECLARE @sqlRestoreDb NVARCHAR(1000)
		= N'RESTORE DATABASE ' + @newDb + N' FROM '
		+ N'DISK = N''' + @fromFile + N''' WITH FILE = 1, '
		+ N'MOVE N''' + @oldDb + N''' TO N''' + @dataFolder +  @newDb + N'.mdf'', '
		+ N'MOVE N''' + @oldDb + N'_log'' TO N''' + @dataFolder + @newDb + N'_log.ldf'', '
		+ N'NOUNLOAD, REPLACE, STATS = 5;';
	EXEC(@sqlRestoreDb)	

	IF @newDb <> @oldDb BEGIN
		DECLARE @sqlModifyName NVARCHAR(1000)
			= N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N''', NEWNAME=N''' + @newDb + N''');'
			+ N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N'_log'', NEWNAME=N''' + @newDb + N'_log'');'

		EXEC(@sqlModifyName)	
	END

END
GO

USE [$(MainDbName)]
GO

GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application]
    ADD [FactureCostEx]          MONEY NULL,
        [FactureCostExEdited]    MONEY NULL,
        [InsuranceRate]          REAL  CONSTRAINT [DF_Application_InsuranceRate] DEFAULT 0.01 NOT NULL,
		[CalculationTotalTariffCost] MONEY NULL,
        [CalculationProfit]          MONEY NULL;


GO
PRINT N'Altering [dbo].[Calculation]...';


GO
ALTER TABLE [dbo].[Calculation] DROP COLUMN [InsuranceCost];


GO
ALTER TABLE [dbo].[Calculation]
    ADD [FactureCostEx]          MONEY CONSTRAINT [DF_Calculation_FactureCostEx] DEFAULT 0 NOT NULL,
        [InsuranceRate]          REAL  CONSTRAINT [DF_Calculation_InsuranceRate] DEFAULT 0.01 NOT NULL,
		[TotalTariffCost] MONEY NULL,
        [Profit]          MONEY NULL;

GO
ALTER TABLE [dbo].[Calculation] DROP CONSTRAINT [DF_Calculation_FactureCostEx];

GO
PRINT N'Altering [dbo].[Carrier]...';


GO
ALTER TABLE [dbo].[Carrier]
    ADD [Contact] NVARCHAR (MAX) NULL,
        [Address] NVARCHAR (MAX) NULL,
        [Phone]   NVARCHAR (MAX) NULL;


GO
PRINT N'Altering [dbo].[Sender]...';


GO
ALTER TABLE [dbo].[Sender]
    ADD [Contact] NVARCHAR (MAX) NULL,
        [Phone]   NVARCHAR (MAX) NULL,
        [Address] NVARCHAR (MAX) NULL;