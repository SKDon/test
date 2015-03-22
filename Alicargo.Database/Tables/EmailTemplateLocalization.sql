CREATE TABLE [dbo].[EmailTemplateLocalization]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,
	[TwoLetterISOLanguageName] CHAR(2) NOT NULL,

	[Subject] NVARCHAR (MAX) NULL,
	[Body] NVARCHAR (MAX) NULL,
	[IsBodyHtml] BIT CONSTRAINT [DF_EmailTemplateLocalization_IsBodyHtml] DEFAULT (0) NOT NULL,

	CONSTRAINT [PK_dbo.EmailTemplateLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),

	CONSTRAINT [FK_dbo.EmailTemplateLocalization_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY 
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_EmailTemplateLocalization_EmailTemplateId_TwoLetterISOLanguageName] 
	ON [dbo].[EmailTemplateLocalization] ([EmailTemplateId], [TwoLetterISOLanguageName])
GO