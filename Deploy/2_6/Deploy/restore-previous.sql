USE [master]
GO

EXEC [dbo].[sp_RestoreDatabase] 
	N'$(DbNew)', 
	N'$(DbOld)', 
	N'$(DbPath)', 
	N'$(DbFolder)'
GO