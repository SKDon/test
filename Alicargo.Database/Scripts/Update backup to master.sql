USE [master]
GO

RESTORE DATABASE [Alicargo_2_1] FROM  DISK = N'A:\Projects\Alicargo_2_0_11032013_010003.bak' WITH  FILE = 1, 
	MOVE N'Alicargo' TO N'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Alicargo_2_1.mdf',  
	MOVE N'Alicargo_log' TO N'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Alicargo_2_1_log.ldf',  
	NOUNLOAD,  REPLACE,  STATS = 5
GO

USE [Alicargo_2_1]
GO

