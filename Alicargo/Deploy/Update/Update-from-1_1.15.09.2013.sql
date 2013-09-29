GO
ALTER TABLE [dbo].[AirWaybill] DROP COLUMN [ForwarderCost];


GO
ALTER TABLE [dbo].[AirWaybill]
    ADD [TotalCostOfSenderForWeight] MONEY NULL;


GO
PRINT N'Altering [dbo].[Application]...';


GO
ALTER TABLE [dbo].[Application]
    ADD [FactureCost]        MONEY NULL,
        [ScotchCost]         MONEY NULL,
        [WithdrawCost]       MONEY NULL,
        [FactureCostEdited]  MONEY NULL,
        [ScotchCostEdited]   MONEY NULL,
        [WithdrawCostEdited] MONEY NULL,
        [ForwarderCost]      MONEY NULL,
        [TariffPerKg]        MONEY NULL,
        [TransitCost]        MONEY NULL,
		[ClassId]			 INT NULL,
		[Certificate]		 NVARCHAR(MAX) NULL, 
		[SenderRate]		 MONEY NULL,
		[SenderId]           BIGINT NULL;


GO
PRINT N'Altering [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD [ContractFileData] VARBINARY (MAX) NULL,
        [ContractFileName] NVARCHAR (MAX)  NULL;
GO

DROP  PROCEDURE [dbo].[Client_DeleteForce];
GO

CREATE PROCEDURE [dbo].[Client_DeleteForce]
	@ClientId BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@UserId BIGINT, @TransitId BIGINT;
	SELECT TOP(1) @UserId = c.[UserId], @TransitId = c.[TransitId]
	FROM [dbo].[Client] c
	WHERE c.Id = @ClientId

	BEGIN TRAN
		
		DELETE
		FROM	[dbo].[Application]
		WHERE	[ClientId] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Client]
		WHERE	[Id] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Transit]
		WHERE	[Id] = @TransitId

		DELETE	TOP(1)
		FROM	[dbo].[User]
		WHERE	[Id] = @UserId

	COMMIT
END
GO


PRINT N'Update complete.'
GO