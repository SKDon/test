USE [master] 
GO 
SET ANSI_NULLS ON 
GO 
SET QUOTED_IDENTIFIER ON 
GO 
 
CREATE PROCEDURE [dbo].[sp_BackupDatabases]
	@databaseName SYSNAME,
	@backupLocation NVARCHAR(200) 
AS 
 
SET NOCOUNT ON; 
           
DECLARE @dateTime NVARCHAR(20) = REPLACE(CONVERT(VARCHAR, GETDATE(),101),'/','') + '_' +  REPLACE(CONVERT(VARCHAR, GETDATE(),108),':','')  
DECLARE @BackupName VARCHAR(100) =  REPLACE(REPLACE(@databaseName,'[',''),']','') +' full backup for '+ @dateTime
DECLARE @BackupFile VARCHAR(100) = @backupLocation + REPLACE(REPLACE(@databaseName, '[',''),']','') + '_' + @dateTime + '.bak'
DECLARE @sqlCommand NVARCHAR(1000) = 'BACKUP DATABASE ' + @databaseName 
										+ ' TO DISK = ''' + @BackupFile + ''' WITH INIT, NAME= ''' 
										+ @BackupName + ''', SKIP, NOFORMAT'

EXEC(@sqlCommand)