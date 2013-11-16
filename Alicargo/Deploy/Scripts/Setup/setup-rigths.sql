USE [master]
GO

CREATE LOGIN [$(PoolName)] FROM WINDOWS WITH DEFAULT_DATABASE=[$(DatabaseName)]
GO

USE [$(DatabaseName)]
GO

CREATE USER [$(PoolName)] FOR LOGIN [$(PoolName)]
GO

ALTER USER [$(PoolName)] WITH DEFAULT_SCHEMA=[dbo]
ALTER ROLE [db_owner] ADD MEMBER [$(PoolName)]
GO