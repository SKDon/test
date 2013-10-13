CREATE PROCEDURE [dbo].[Calculation_SetState]
	@Id	BIGINT,
	@RowVersion ROWVERSION,
	@State INT
AS
	SET NOCOUNT ON;

	UPDATE	[dbo].[Calculation]
	SET		[StateId] = @State,
			[StateIdTimestamp] = GETUTCDATE()
	OUTPUT	[INSERTED].[RowVersion], [INSERTED].[StateIdTimestamp] AS [StateTimestamp]
	WHERE	[Id] = @Id AND [RowVersion] = @RowVersion
