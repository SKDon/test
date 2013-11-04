USE [master] 
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
 
CREATE PROCEDURE [dbo].[sp_RestoreDatabase]
	@newDb NVARCHAR(50),
	@oldDb NVARCHAR(50),
	@fromFile NVARCHAR(200) 
AS

BEGIN
 
	SET NOCOUNT ON;

	DECLARE @sqlRestoreDb NVARCHAR(1000) 
		= N'RESTORE DATABASE ' + @newDb + N' FROM '
		+ N'DISK = N''' + @fromFile + N''' WITH  FILE = 1, '
		+ N'MOVE N''' + @oldDb + N''' TO N''c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\' + @newDb + N'.mdf'', '
		+ N'MOVE N''' + @oldDb + N'_log'' TO N''c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\' + @newDb + N'_log.ldf'', '
		+ N'NOUNLOAD, REPLACE, STATS = 5'

	EXEC(@sqlRestoreDb)

END

