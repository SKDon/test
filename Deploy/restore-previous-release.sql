USE [master]
GO

EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Alicargo_2_3'
GO
USE [master]
GO
ALTER DATABASE [Alicargo_2_3] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
DROP DATABASE [Alicargo_2_3]
GO

EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Alicargo_Files_2_3'
GO
USE [master]
GO
ALTER DATABASE [Alicargo_Files_2_3] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
DROP DATABASE [Alicargo_Files_2_3]
GO

exec [dbo].[sp_RestoreDatabase] 
	'Alicargo_2_3', 
	'Alicargo_2_3', 
	'A:\Projects\Alicargo_2_3.bak', 
	'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\'
GO

exec [dbo].[sp_RestoreDatabase] 
	'Alicargo_Files_2_3', 
	'Alicargo_Files_2_3', 
	'A:\Projects\Alicargo_Files_2_3.bak', 
	'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\'
GO