﻿CREATE TABLE [dbo].[User] (
	[Id]						BIGINT			IDENTITY (1, 1) NOT NULL,
	[Login]						NVARCHAR (320)	NOT NULL,
	[PasswordHash]				VARBINARY (MAX)	NOT NULL,
	[PasswordSalt]				VARBINARY (MAX)	NOT NULL,
	[TwoLetterISOLanguageName]	CHAR(2)			NOT NULL CONSTRAINT [DF_User_TwoLetterISOLanguageName] DEFAULT 'en', 

	CONSTRAINT [PK_dbo.User]	PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Login] ON [dbo].[User]([Login] ASC);