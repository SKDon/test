USE [$(MainDbName)] 
GO

UPDATE [dbo].[EmailMessage]
	SET [StateId] = 0
	WHERE [StateId] = 500
		AND [CreationTimestamp] > GETUTCDATE() - 1
GO