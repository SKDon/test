USE [master]
drop database [Alicargo_Dev]
GO

print '[Alicargo_Dev] dropped'
GO

exec [dbo].[sp_RestoreDatabase]
	@newDb = N'Alicargo_Dev',
	@oldDb = N'Alicargo',
	@fromFile = N'C:\AlicargoDbBackups\Alicargo_2_0_10312013_194138.bak'
GO

drop database [Alicargo_Files_Dev]
GO

print '[Alicargo_Files_Dev] dropped'
GO

exec [dbo].[sp_RestoreDatabase]
	@newDb = N'Alicargo_Files_Dev',
	@oldDb = N'Alicargo_Files_2_0',
	@fromFile = N'C:\AlicargoDbBackups\Alicargo_Files_2_0_10312013_194141.bak'
GO