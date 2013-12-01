USE [master]
GO

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