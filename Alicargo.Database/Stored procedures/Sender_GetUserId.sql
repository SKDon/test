﻿CREATE PROCEDURE [dbo].[Sender_GetUserId]
	@Id BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP(1) s.[UserId]
	FROM	[dbo].[Sender] s
	WHERE	s.[Id] = @Id