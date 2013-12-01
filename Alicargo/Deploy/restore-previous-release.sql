USE [master]
GO

--EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Alicargo_2_1'
--GO
--USE [master]
--GO
--ALTER DATABASE [Alicargo_2_1] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
--GO
--USE [master]
--GO
--/****** Object:  Database [Alicargo_2_1]    Script Date: 12/1/2013 9:53:44 PM ******/
--DROP DATABASE [Alicargo_2_1]
--GO

exec [dbo].[sp_RestoreDatabase] 
	'Alicargo_2_1', 
	'Alicargo', 
	'A:\Projects\Alicargo_2_1_12012013_010001.bak', 
	'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\'
GO

exec [dbo].[sp_RestoreDatabase] 
	'Alicargo_Files_2_1', 
	'Alicargo_Files_2_0', 
	'A:\Projects\Alicargo_Files_2_1_12012013_010006.bak', 
	'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\'
GO