CREATE TABLE [dbo].[StateEmailTemplate]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL CONSTRAINT [PK_dbo.StateEmailTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),

	[StateId] BIGINT NOT NULL,
	[EmailTemplateId] BIGINT NOT NULL,

	[EnableEmailSend] BIT NOT NULL CONSTRAINT [DF_StateEmailTemplate_EnableEmailSend] DEFAULT 1,
	
	CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.StateEmailTemplate_dbo.EmailTemplate_EmailTemplateId] FOREIGN KEY
		([EmailTemplateId]) REFERENCES [dbo].[EmailTemplate] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_StateEmailTemplate_StateId_EmailTemplateId] ON [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId])
GO