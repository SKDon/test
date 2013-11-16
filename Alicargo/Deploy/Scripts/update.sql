USE [master] 
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
 
ALTER PROCEDURE [dbo].[sp_RestoreDatabase]
	@newDb NVARCHAR(50),
	@oldDb NVARCHAR(50),
	@fromFile NVARCHAR(200),
	@dataFolder NVARCHAR(200)
AS

BEGIN
 
	SET NOCOUNT ON;

	DECLARE @sqlRestoreDb NVARCHAR(1000) 
		= N'RESTORE DATABASE ' + @newDb + N' FROM '
		+ N'DISK = N''' + @fromFile + N''' WITH FILE = 1, '
		+ N'MOVE N''' + @oldDb + N''' TO N''' + @dataFolder +  @newDb + N'.mdf'', '
		+ N'MOVE N''' + @oldDb + N'_log'' TO N''' + @dataFolder + @newDb + N'_log.ldf'', '
		+ N'NOUNLOAD, REPLACE, STATS = 5;'
		+ N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N''', NEWNAME=N''' + @newDb + N''');'
		+ N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N'_log'', NEWNAME=N''' + @newDb + N'_log'');'

	EXEC(@sqlRestoreDb)

END
GO

PRINT N'[sp_RestoreDatabase] updated...'
GO