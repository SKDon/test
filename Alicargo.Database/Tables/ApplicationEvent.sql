﻿CREATE TABLE [dbo].[ApplicationEvent]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL,
	[RowVersion] ROWVERSION NOT NULL,
	[ApplicationId] BIGINT NOT NULL,
	[EventType] INT NOT NULL,

	CONSTRAINT [PK_dbo.ApplicationEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ApplicationEvent_dbo.Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id])
)
GO

CREATE UNIQUE INDEX [IX_ApplicationEvent_ApplicationId_EventType] ON [dbo].[ApplicationEvent] ([ApplicationId], [EventType])
GO