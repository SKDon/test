CREATE TABLE [dbo].[Country](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_En] [nvarchar](128) NOT NULL,
	[Name_Ru] [nvarchar](128) NOT NULL,
	[Code] [char](2) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))