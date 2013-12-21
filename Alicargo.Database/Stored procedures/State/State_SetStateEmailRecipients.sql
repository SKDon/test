CREATE PROCEDURE [dbo].[State_SetStateEmailRecipients]
	@StateId BIGINT,
	@Roles [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[StateEmailRecipient]
		WHERE [StateId] = @StateId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Roles)
		
		INSERT [dbo].[StateEmailRecipient] ([StateId], [RoleId])
		SELECT @StateId AS [StateId], r.[Id] AS [RoleId]
		FROM @Roles r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[StateEmailRecipient] a WHERE a.[StateId] = @StateId)

	COMMIT

END
GO