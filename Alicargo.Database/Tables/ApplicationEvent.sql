CREATE TABLE [dbo].[ApplicationEvent]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL,

	[StateId] INT NOT NULL,
	[StateIdTimestamp] DATETIMEOFFSET CONSTRAINT [DF_ApplicationEvent_StateIdTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,
	[CreationTimestamp] DATETIMEOFFSET CONSTRAINT [DF_ApplicationEvent_CreationTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,

	[ApplicationId] BIGINT NOT NULL,
	[EventTypeId] INT NOT NULL,
	[Data] VARBINARY(MAX) NULL

	CONSTRAINT [PK_dbo.ApplicationEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ApplicationEvent_dbo.Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id])
)
GO