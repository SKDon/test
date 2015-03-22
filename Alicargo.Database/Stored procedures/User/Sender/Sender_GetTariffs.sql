CREATE PROCEDURE [dbo].[Sender_GetTariffs]
	@Ids [dbo].[IdsTable] READONLY

AS BEGIN
	SET NOCOUNT ON;

	SELECT	s.[Id],
			s.[TariffOfTapePerBox]
	FROM	[dbo].[Sender] s
	WHERE	s.[Id] IN (SELECT [Id] FROM @Ids)

END
GO