﻿CREATE TABLE [dbo].[ApplicationFile]
(
	[Id] BIGINT NOT NULL IDENTITY (1, 1),

	[ApplicationId] BIGINT NOT NULL,
	[TypeId] INT NOT NULL,

	[Name] NVARCHAR(320) NOT NULL,
	[Data] VARBINARY(MAX) NOT NULL

	CONSTRAINT [PK_dbo.ApplicationFile] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO