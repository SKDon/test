CREATE TABLE [dbo].[StateEmailTemplate]
(
	[StateId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_StateEmailTemplate_EnableEmailSend] DEFAULT 1,
	[UseApplicationEventTemplate] BIT NOT NULL CONSTRAINT [DF_StateEmailTemplate_UseApplicationEventTemplate] DEFAULT 0,
	
	CONSTRAINT [PK_dbo.StateEmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC, [StateId] ASC),
	CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO