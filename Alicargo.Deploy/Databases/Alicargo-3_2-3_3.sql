GO
PRINT N'Altering [dbo].[EmailMessage]...';


GO
ALTER TABLE [dbo].[EmailMessage]
    ADD [EmailSenderUserId] BIGINT NULL;


GO
PRINT N'Altering [dbo].[Event]...';


GO
ALTER TABLE [dbo].[Event]
    ADD [UserId] BIGINT NULL;


GO
PRINT N'Creating [dbo].[FK_dbo.EmailMessage_dbo.User_Id]...';


GO
ALTER TABLE [dbo].[EmailMessage] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.EmailMessage_dbo.User_Id] FOREIGN KEY ([EmailSenderUserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.Event_dbo.User_Id]...';


GO
ALTER TABLE [dbo].[Event] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Event_dbo.User_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Altering [dbo].[EmailMessage_Add]...';


GO
ALTER PROCEDURE [dbo].[EmailMessage_Add]
	@State INT,
	@PartitionId INT,
	@From NVARCHAR (MAX),
	@To NVARCHAR (MAX),
	@CopyTo NVARCHAR (MAX),
	@Subject NVARCHAR (MAX),
	@Body NVARCHAR (MAX),
	@IsBodyHtml BIT,
	@Files VARBINARY (MAX),
	@EmailSenderUserId BIGINT 

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[EmailMessage] ([Body], [CopyTo], [Files], [From], [IsBodyHtml], [PartitionId], [StateId], [To], [Subject], [EmailSenderUserId])
	VALUES (@Body, @CopyTo, @Files, @From, @IsBodyHtml, @PartitionId, @State, @To, @Subject, @EmailSenderUserId)

END
GO
PRINT N'Altering [dbo].[EmailMessage_GetNext]...';


GO
ALTER PROCEDURE [dbo].[EmailMessage_GetNext]
	@State INT,
	@PartitionId INT
AS
	SET NOCOUNT ON;

	SELECT	TOP(1)
			m.[Id],
			m.[From],
			m.[To],
			m.[CopyTo],
			m.[Subject],
			m.[Body],
			m.[IsBodyHtml],
			m.[Files],
			m.[EmailSenderUserId]
	FROM [dbo].[EmailMessage] m
	WHERE m.[StateId] = @State AND m.[PartitionId] = @PartitionId
	ORDER BY m.[Id]
GO
PRINT N'Altering [dbo].[Event_Add]...';


GO
ALTER PROCEDURE [dbo].[Event_Add]
	@StateId INT,
	@PartitionId INT,
	@EventTypeId INT,
	@Data VARBINARY(MAX),
	@UserId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[Event] ([EventTypeId], [StateId], [Data], [PartitionId], [UserId])
	VALUES (@EventTypeId, @StateId, @Data, @PartitionId, @UserId)

END
GO
PRINT N'Altering [dbo].[Event_GetNext]...';


GO
ALTER PROCEDURE [dbo].[Event_GetNext]
	@EventTypeId INT,
	@PartitionId INT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	TOP(1) 
			e.[Id],
			e.[StateId] AS [State],
			e.[Data],
			e.[UserId]
	FROM [dbo].[Event] e
	WHERE e.[EventTypeId] = @EventTypeId AND e.[PartitionId] = @PartitionId AND e.[StateId] <> -1
	ORDER BY e.[Id]

END
GO
PRINT N'Refreshing [dbo].[EmailMessage_SetState]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[EmailMessage_SetState]';


GO
PRINT N'Refreshing [dbo].[Event_Delete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Event_Delete]';


GO
PRINT N'Refreshing [dbo].[Event_SetState]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Event_SetState]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[EmailMessage] WITH CHECK CHECK CONSTRAINT [FK_dbo.EmailMessage_dbo.User_Id];

ALTER TABLE [dbo].[Event] WITH CHECK CHECK CONSTRAINT [FK_dbo.Event_dbo.User_Id];


GO
PRINT N'Update complete.';


GO
