USE [master]
GO

EXEC [dbo].[sp_RestoreDatabase] 
	N'$(MainDbNew)', 
	N'$(MainDbOld)', 
	N'$(MainDbPath)', 
	N'$(DbFolder)'
GO

EXEC [dbo].[sp_RestoreDatabase] 
	N'$(FilesDbNew)', 
	N'$(FilesDbOld)', 
	N'$(FilesDbPath)', 
	N'$(DbFolder)'
GO