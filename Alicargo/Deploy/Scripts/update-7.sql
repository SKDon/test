--:setvar MainDbName "Alicargo_2_1"
GO

USE [$(MainDbName)]
GO

ALTER TABLE [dbo].[Calculation]
    ADD [ClassId] INT            NULL,
        [Invoice] NVARCHAR (MAX) NOT NULL CONSTRAINT [DF_Temp1] DEFAULT(N'{B0101347-AA93-499F-B1C5-A46138F443DD}'),
        [Value]   MONEY          NOT NULL CONSTRAINT [DF_Temp2] DEFAULT(0),
        [Count]   INT            NULL;
GO

UPDATE [dbo].[Calculation]
SET [ClassId] = a.[ClassId],
	[Invoice] = a.[Invoice],
	[Value] = a.[Value],
	[Count] = a.[Count]
FROM [dbo].[Calculation] c
JOIN [dbo].[Application] a
ON a.[Id] = c.[ApplicationHistoryId]
WHERE c.[Invoice] = N'{B0101347-AA93-499F-B1C5-A46138F443DD}'
GO

UPDATE [dbo].[Calculation]
SET [Invoice] = N''
WHERE [Invoice] = N'{B0101347-AA93-499F-B1C5-A46138F443DD}'
GO

ALTER TABLE dbo.Calculation DROP CONSTRAINT [DF_Temp1]
GO

ALTER TABLE dbo.Calculation DROP CONSTRAINT [DF_Temp2]
GO

ALTER TABLE [dbo].[ClientBalanceHistory]
    ADD [IsCalculation] BIT NOT NULL;
GO


GO
ALTER PROCEDURE [dbo].[Calculation_Add]
	@ClientId BIGINT,
	@ApplicationHistoryId BIGINT,
	@AirWaybillDisplay NVARCHAR(320),
	@ApplicationDisplay NVARCHAR(320),
	@MarkName NVARCHAR(320),
	@Weight REAL,
	@TariffPerKg MONEY,
	@ScotchCost MONEY,
	@InsuranceCost MONEY,
	@FactureCost MONEY,
	@TransitCost MONEY,
	@PickupCost MONEY,
	@FactoryName NVARCHAR(320),
	@CreationTimestamp DATETIMEOFFSET,
	@ClassId INT,
	@Invoice NVARCHAR,
	@Value MONEY,
	@Count INT

AS BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Calculation]
		([ClientId]
		,[ApplicationHistoryId]
		,[AirWaybillDisplay]
		,[ApplicationDisplay]
		,[MarkName]
		,[Weight]
		,[TariffPerKg]
		,[ScotchCost]
		,[InsuranceCost]
		,[FactureCost]
		,[TransitCost]
		,[PickupCost]
		,[FactoryName]
		,[CreationTimestamp]
		,[ClassId]
		,[Invoice]
		,[Value]
		,[Count])
     VALUES
		(@ClientId
		,@ApplicationHistoryId
		,@AirWaybillDisplay
		,@ApplicationDisplay
		,@MarkName
		,@Weight
		,@TariffPerKg
		,@ScotchCost
		,@InsuranceCost
		,@FactureCost
		,@TransitCost
		,@PickupCost
		,@FactoryName
		,@CreationTimestamp
		,@ClassId
		,@Invoice
		,@Value
		,@Count)
END


GO
ALTER PROCEDURE [dbo].[Calculation_GetByApplication]
	@ApplicationId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1)
		[ClientId],
		[AirWaybillDisplay],
		[ApplicationDisplay],
		[MarkName],
		[Weight],
		[TariffPerKg],
		[ScotchCost],
		[InsuranceCost],
		[FactureCost],
		[TransitCost],
		[PickupCost],
		[FactoryName],
		[CreationTimestamp],
		[ClassId],
		[Invoice],
		[Value],
		[Count]
	FROM [dbo].[Calculation]
	WHERE [ApplicationHistoryId] = @ApplicationId

END


GO
ALTER PROCEDURE [dbo].[ClientBalanceHistory_Add]
	@ClientId BIGINT,
	@Balance MONEY,
	@Money MONEY,
	@Comment NVARCHAR(MAX),
	@Timestamp DATETIMEOFFSET,
	@EventTypeId INT,
	@IsCalculation BIT

AS BEGIN
	SET NOCOUNT ON;

	INSERT [dbo].[ClientBalanceHistory] ([Balance], [Comment], [ClientId], [Money], [Timestamp], [EventTypeId], [IsCalculation])
	VALUES (@Balance, @Comment, @ClientId, @Money, @Timestamp, @EventTypeId, @IsCalculation)

END


GO
ALTER PROCEDURE [dbo].[ClientBalanceHistory_Get]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT [Balance], [Comment], [Timestamp], [Money], [CreationTimestamp], [EventTypeId] AS [EventType], [IsCalculation]
	FROM [dbo].[ClientBalanceHistory]
	WHERE [ClientId] = @ClientId
	ORDER BY [Id] DESC

END


GO
ALTER PROCEDURE [dbo].[City_GetList]
	@Language CHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT c.[Id], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]) AS [Name], c.[Position]
	FROM [dbo].[City] c
	ORDER BY c.[Position], IIF(@Language = 'ru', c.[Name_Ru], c.[Name_En]), c.[Id]

END


GO
CREATE PROCEDURE [dbo].[Calculation_GetByClient]
	@ClientId BIGINT

AS BEGIN
	SET NOCOUNT ON;

	SELECT	[ClientId],
			[AirWaybillDisplay],
			[ApplicationDisplay],
			[MarkName],
			[Weight],
			[TariffPerKg],
			[ScotchCost],
			[InsuranceCost],
			[FactureCost],
			[TransitCost],
			[PickupCost],
			[FactoryName],
			[CreationTimestamp],
			[ClassId],
			[Invoice],
			[Value],
			[Count]
	FROM [dbo].[Calculation]
	WHERE [ClientId] = @ClientId

END


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Calculation_RemoveByApplication]';