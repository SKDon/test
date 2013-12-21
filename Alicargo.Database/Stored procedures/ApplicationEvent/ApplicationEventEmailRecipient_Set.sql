CREATE PROCEDURE [dbo].[ApplicationEventEmailRecipient_Set]
	@EventTypeId BIGINT,
	@Recipients [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[ApplicationEventEmailRecipient]
		WHERE [EventTypeId] = @EventTypeId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Recipients)
		
		INSERT [dbo].[ApplicationEventEmailRecipient] ([EventTypeId], [RoleId])
		SELECT @EventTypeId AS [StateId], r.[Id] AS [RoleId]
		FROM @Recipients r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[ApplicationEventEmailRecipient] a WHERE a.[EventTypeId] = @EventTypeId)

	COMMIT

END
GO