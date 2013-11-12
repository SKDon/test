USE [master]
GO

exec [dbo].[sp_RestoreDatabase] 'Alicargo_2_1', 'Alicargo', 'A:\Projects\Alicargo_2_1_11122013_182701.bak', 'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\'
GO

exec [dbo].[sp_RestoreDatabase] 'Alicargo_Files_2_1', 'Alicargo_Files_2_0', 'A:\Projects\Alicargo_Files_2_1_11122013_182706.bak', 'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\'
GO