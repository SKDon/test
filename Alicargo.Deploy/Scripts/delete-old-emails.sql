USE [$(MainDbName)] 
GO

DELETE [dbo].[EmailMessage]
	WHERE [StateId] = 1
		AND [CreationTimestamp] > (GETUTCDATE() - 5)
GO