﻿CREATE PROCEDURE [dbo].[Event_SetState]
	@Id BIGINT,
	@State INT
AS
	SET NOCOUNT ON;

	UPDATE TOP(1) [dbo].[ApplicationEvent]
	SET [StateId] = @State, [StateIdTimestamp] = GETUTCDATE()
	WHERE [Id] = @Id

GO