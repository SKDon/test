CREATE TABLE [dbo].[EventEmailRecipient] (
	[RoleId] INT NOT NULL,
	[EventTypeId] INT NOT NULL,

	CONSTRAINT [PK_dbo.EventEmailRecipient] PRIMARY KEY CLUSTERED ([RoleId] ASC, [EventTypeId] ASC),
);
GO