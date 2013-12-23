CREATE TABLE [dbo].[StateLocalization] (
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[Name] NVARCHAR (320) NOT NULL,
	[TwoLetterISOLanguageName] CHAR(2) NOT NULL,
	[StateId] BIGINT NOT NULL,

	CONSTRAINT [PK_dbo.StateLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.StateLocalization_dbo.State_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_StateLocalization_StateId]
	ON [dbo].[StateLocalization]([StateId] ASC);