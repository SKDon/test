CREATE PROCEDURE [dbo].[EventEmailRecipient_Set]
	@EventTypeId INT,
	@Recipients [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[EventEmailRecipient]
		WHERE [EventTypeId] = @EventTypeId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Recipients)
		
		INSERT [dbo].[EventEmailRecipient] ([EventTypeId], [RoleId])
		SELECT @EventTypeId AS [StateId], r.[Id] AS [RoleId]
		FROM @Recipients r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[EventEmailRecipient] a WHERE a.[EventTypeId] = @EventTypeId)

	COMMIT

END
GO