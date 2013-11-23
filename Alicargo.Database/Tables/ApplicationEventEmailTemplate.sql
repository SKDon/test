CREATE TABLE [dbo].[ApplicationEventEmailTemplate]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL CONSTRAINT [PK_dbo.ApplicationEventEmailTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),

	[EventTypeId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_ApplicationEventEmailTemplate_EnableEmailSend] DEFAULT 1,
	
	CONSTRAINT [FK_dbo.ApplicationEventEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_ApplicationEventEmailTemplate_EventTypeId_EmailTemplateId] 
	ON [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId])
GO