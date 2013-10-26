CREATE PROCEDURE [dbo].[Sender_GetTariffs]
	@Ids [dbo].[IdsTable] READONLY
AS
	SET NOCOUNT ON;

	SELECT	s.[Id],
			s.[TariffOfTapePerBox]
	FROM	[dbo].[Sender] s
	WHERE	s.[Id] IN (SELECT [Id] FROM @Ids)