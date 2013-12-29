CREATE TABLE [dbo].[ApplicationEventEmailRecipient] (
	[RoleId] INT NOT NULL,
	[EventTypeId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.ApplicationEventEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [EventTypeId] ASC),
);
GO