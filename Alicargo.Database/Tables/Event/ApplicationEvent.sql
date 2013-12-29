CREATE TABLE [dbo].[Event]
(
	[Id] BIGINT IDENTITY(1, 1) NOT NULL,

	[StateId] INT NOT NULL,
	[StateIdTimestamp] DATETIMEOFFSET CONSTRAINT [DF_Event_StateIdTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,
	[CreationTimestamp] DATETIMEOFFSET CONSTRAINT [DF_Event_CreationTimestamp] DEFAULT (GETUTCDATE()) NOT NULL,

	[ApplicationId] BIGINT NOT NULL,
	[EventTypeId] INT NOT NULL,
	[Data] VARBINARY(MAX) NULL

	CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Event_dbo.Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id])
)
GO