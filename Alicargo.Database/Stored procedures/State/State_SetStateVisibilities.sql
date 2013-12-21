CREATE PROCEDURE [dbo].[State_SetStateVisibilities]
	@StateId BIGINT,
	@Roles [dbo].[IdsTable] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRAN

		DELETE [dbo].[StateVisibility]
		WHERE [StateId] = @StateId
		AND [RoleId] NOT IN (SELECT [Id] FROM @Roles)
		
		INSERT [dbo].[StateVisibility] ([StateId], [RoleId])
		SELECT @StateId AS [StateId], r.[Id] AS [RoleId]
		FROM @Roles r
		WHERE r.[Id] NOT IN (SELECT a.[RoleId] FROM [dbo].[StateVisibility] a WHERE a.[StateId] = @StateId)

	COMMIT

END
GO