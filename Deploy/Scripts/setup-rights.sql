USE [master]
GO
CREATE LOGIN [$(PoolName)] FROM WINDOWS WITH DEFAULT_DATABASE=[$(MainDbName)]
GO

USE [$(MainDbName)]
GO
CREATE USER [$(PoolName)] FOR LOGIN [$(PoolName)]
ALTER USER [$(PoolName)] WITH DEFAULT_SCHEMA=[dbo]
ALTER ROLE [db_owner] ADD MEMBER [$(PoolName)]
GO

USE [$(FilesDbName)]
CREATE USER [$(PoolName)] FOR LOGIN [$(PoolName)]
ALTER USER [$(PoolName)] WITH DEFAULT_SCHEMA=[dbo]
ALTER ROLE [db_owner] ADD MEMBER [$(PoolName)]
GO