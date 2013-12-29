CREATE TABLE [dbo].[EventEmailTemplate]
(
	[EventTypeId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_EventEmailTemplate_EnableEmailSend] DEFAULT 1,
	
	CONSTRAINT [PK_dbo.EventEmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC, [EventTypeId] ASC),
	CONSTRAINT [FK_dbo.EventEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO