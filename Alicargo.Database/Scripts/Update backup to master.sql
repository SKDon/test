USE [master]
GO

RESTORE DATABASE [Alicargo_2_0] FROM  DISK = N'A:\Projects\Alicargo_2_0_11032013_010003.bak' WITH  FILE = 1, 
	MOVE N'Alicargo' TO N'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Alicargo_2_0.mdf',  
	MOVE N'Alicargo_log' TO N'A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Alicargo_2_0_log.ldf',  
	NOUNLOAD,  REPLACE,  STATS = 5
GO

USE [Alicargo_2_0]
GO

ALTER TABLE [dbo].[Calculation] ADD
	FactoryName nvarchar(320) NOT NULL CONSTRAINT DF_Calculation_FactoryName DEFAULT '';
GO

BEGIN TRAN
	UPDATE [dbo].[Calculation]
	SET [dbo].[Calculation].[FactoryName] = a.[FactoryName]
	FROM [dbo].[Application] a
	JOIN [dbo].[Calculation] c
	ON a.[Id] = [ApplicationHistoryId]
	AND a.[ClientId] = c.[ClientId]
	GO
	
	ALTER TABLE [dbo].[Calculation] DROP CONSTRAINT DF_Calculation_FactoryName;
	GO
COMMIT
PRINT '[dbo].[Calculation] UPDATED...'
GO


ALTER PROCEDURE [dbo].[Client_DeleteForce]
	@ClientId BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@UserId BIGINT, @TransitId BIGINT;
	SELECT TOP(1) @UserId = c.[UserId], @TransitId = c.[TransitId]
	FROM [dbo].[Client] c
	WHERE c.Id = @ClientId

	BEGIN TRAN
		
		DELETE
		FROM	[dbo].[Application]
		WHERE	[ClientId] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Client]
		WHERE	[Id] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Transit]
		WHERE	[Id] = @TransitId

		DELETE	TOP(1)
		FROM	[dbo].[User]
		WHERE	[Id] = @UserId

	COMMIT
END
PRINT N'Refreshing [dbo].[Calculation_SetState]...';
GO

USE [master]
GO