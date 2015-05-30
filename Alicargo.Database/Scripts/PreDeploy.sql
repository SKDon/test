USE [master]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO



IF NOT OBJECT_ID('master.dbo.sp_BackupDatabase') IS NULL
	DROP PROCEDURE [dbo].[sp_BackupDatabase]
GO

CREATE PROCEDURE [dbo].[sp_BackupDatabase]
	@databaseName SYSNAME,
	@backupLocation NVARCHAR(200) 
AS BEGIN
	SET NOCOUNT ON;

	DECLARE @dateTime NVARCHAR(20) = REPLACE(CONVERT(VARCHAR, GETDATE(),101),'/','') + '_' +  REPLACE(CONVERT(VARCHAR, GETDATE(),108),':','')
	DECLARE @BackupName VARCHAR(100) =  REPLACE(REPLACE(@databaseName,'[',''),']','') +' full backup for '+ @dateTime
	DECLARE @BackupFile VARCHAR(100) = @backupLocation + REPLACE(REPLACE(@databaseName, '[',''),']','') + '_' + @dateTime + '.bak'

	DECLARE @sqlCommand NVARCHAR(1000) = 'BACKUP DATABASE ' + @databaseName
											+ ' TO DISK = ''' + @BackupFile + ''' WITH INIT, NAME= '''
											+ @BackupName + ''', SKIP, NOFORMAT'
	EXEC(@sqlCommand)
END
GO



IF NOT OBJECT_ID('master.dbo.sp_RestoreDatabase') IS NULL
	DROP PROCEDURE [dbo].[sp_RestoreDatabase]
GO

CREATE PROCEDURE [dbo].[sp_RestoreDatabase]
	@newDb NVARCHAR(50),
	@oldDb NVARCHAR(50),
	@fromFile NVARCHAR(200),
	@dataFolder NVARCHAR(200)

AS BEGIN
	SET NOCOUNT ON;

	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = @newDb
	
	EXEC(N'ALTER DATABASE [' + @newDb + N'] SET SINGLE_USER WITH ROLLBACK IMMEDIATE')

	DECLARE @sqlRestoreDb NVARCHAR(1000)
		= N'RESTORE DATABASE ' + @newDb + N' FROM '
		+ N'DISK = N''' + @fromFile + N''' WITH FILE = 1, '
		+ N'MOVE N''' + @oldDb + N''' TO N''' + @dataFolder +  @newDb + N'.mdf'', '
		+ N'MOVE N''' + @oldDb + N'_log'' TO N''' + @dataFolder + @newDb + N'_log.ldf'', '
		+ N'NOUNLOAD, REPLACE, STATS = 5';
	EXEC(@sqlRestoreDb)	

	IF @newDb <> @oldDb BEGIN
		DECLARE @sqlModifyName NVARCHAR(1000)
			= N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N''', NEWNAME=N''' + @newDb + N''');'
			+ N'ALTER DATABASE ' + @newDb + N' MODIFY FILE (NAME=N'''+ @oldDb + N'_log'', NEWNAME=N''' + @newDb + N'_log'');'

		EXEC(@sqlModifyName)	
	END
END
GO



USE [$(DatabaseName)];
GO


CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

ALTER USER [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA=[dbo]
GO

ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\DefaultAppPool]
GO