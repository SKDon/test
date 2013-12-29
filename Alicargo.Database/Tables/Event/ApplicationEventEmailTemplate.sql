CREATE TABLE [dbo].[ApplicationEventEmailTemplate]
(
	[EventTypeId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_ApplicationEventEmailTemplate_EnableEmailSend] DEFAULT 1,
	
	CONSTRAINT [PK_dbo.ApplicationEventEmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC, [EventTypeId] ASC),
	CONSTRAINT [FK_dbo.ApplicationEventEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO