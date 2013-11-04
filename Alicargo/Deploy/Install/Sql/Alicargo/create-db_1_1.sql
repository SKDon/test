USE [$(DatabaseName)]
GO

/****** Object:  StoredProcedure [dbo].[Client_DeleteForce]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Client_DeleteForce]
	@ClientId BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@UserId BIGINT, @TransitId BIGINT;
	SELECT TOP(1) @UserId = c.[UserId], @TransitId = c.[TransitId]
	FROM [dbo].[Client] c
	WHERE c.Id  = @ClientId

	BEGIN TRAN
		
		DELETE
		FROM	[dbo].[Application]
		WHERE	[ClientId] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Client]
		WHERE 	[Id] = @ClientId

		DELETE	TOP(1)
		FROM	[dbo].[Transit]
		WHERE 	[Id] = @TransitId

		DELETE	TOP(1)
		FROM	[dbo].[User]
		WHERE 	[Id] = @UserId

	COMMIT
END
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AirWaybill]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AirWaybill](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationTimestamp] [datetimeoffset](7) NOT NULL,
	[Bill] [nvarchar](320) NOT NULL,
	[ArrivalAirport] [nvarchar](max) NOT NULL,
	[DepartureAirport] [nvarchar](max) NOT NULL,
	[DateOfDeparture] [datetimeoffset](7) NOT NULL,
	[DateOfArrival] [datetimeoffset](7) NOT NULL,
	[BrokerId] [bigint] NOT NULL,
	[GTD] [nvarchar](320) NULL,
	[GTDFileData] [varbinary](max) NULL,
	[GTDFileName] [nvarchar](320) NULL,
	[GTDAdditionalFileData] [varbinary](max) NULL,
	[GTDAdditionalFileName] [nvarchar](320) NULL,
	[PackingFileData] [varbinary](max) NULL,
	[PackingFileName] [nvarchar](320) NULL,
	[InvoiceFileData] [varbinary](max) NULL,
	[InvoiceFileName] [nvarchar](320) NULL,
	[AWBFileData] [varbinary](max) NULL,
	[AWBFileName] [nvarchar](320) NULL,
	[StateId] [bigint] NOT NULL,
	[StateChangeTimestamp] [datetimeoffset](7) NOT NULL,
	[FlightCost] [money] NULL,
	[CustomCost] [money] NULL,
	[BrokerCost] [money] NULL,
	[ForwarderCost] [money] NULL,
	[AdditionalCost] [money] NULL,
 CONSTRAINT [PK_dbo.Reference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Application]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Application](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationTimestamp] [datetimeoffset](7) NOT NULL,
	[Invoice] [nvarchar](max) NOT NULL,
	[InvoiceFileData] [varbinary](max) NULL,
	[InvoiceFileName] [nvarchar](max) NULL,
	[SwiftFileData] [varbinary](max) NULL,
	[SwiftFileName] [nvarchar](max) NULL,
	[DeliveryBillFileData] [varbinary](max) NULL,
	[DeliveryBillFileName] [nvarchar](max) NULL,
	[Torg12FileData] [varbinary](max) NULL,
	[Torg12FileName] [nvarchar](max) NULL,
	[CPFileData] [varbinary](max) NULL,
	[CPFileName] [nvarchar](max) NULL,
	[PackingFileData] [varbinary](max) NULL,
	[PackingFileName] [nvarchar](320) NULL,
	[Characteristic] [nvarchar](max) NULL,
	[AddressLoad] [nvarchar](max) NULL,
	[WarehouseWorkingTime] [nvarchar](max) NULL,
	[TransitReference] [nvarchar](max) NULL,
	[Weight] [real] NULL,
	[Count] [int] NULL,
	[Volume] [real] NOT NULL,
	[TermsOfDelivery] [nvarchar](max) NULL,
	[MethodOfDeliveryId] [int] NOT NULL,
	[DateOfCargoReceipt] [datetimeoffset](7) NULL,
	[DateInStock] [datetimeoffset](7) NULL,
	[StateChangeTimestamp] [datetimeoffset](7) NOT NULL,
	[StateId] [bigint] NOT NULL,
	[Value] [money] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[TransitId] [bigint] NOT NULL,
	[CountryId] [bigint] NULL,
	[AirWaybillId] [bigint] NULL,
	[FactoryName] [nvarchar](320) NOT NULL,
	[FactoryPhone] [nvarchar](max) NULL,
	[FactoryContact] [nvarchar](max) NULL,
	[FactoryEmail] [nvarchar](320) NULL,
	[MarkName] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AvailableState]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvailableState](
	[RoleId] [int] NOT NULL,
	[StateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.AvailableState] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Broker]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Broker](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Brocker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Carrier]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Carrier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[Nic] [nvarchar](max) NOT NULL,
	[LegalEntity] [nvarchar](max) NOT NULL,
	[Contacts] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[INN] [nvarchar](max) NULL,
	[KPP] [nvarchar](max) NULL,
	[OGRN] [nvarchar](max) NULL,
	[Bank] [nvarchar](max) NULL,
	[BIC] [nvarchar](max) NULL,
	[LegalAddress] [nvarchar](max) NULL,
	[MailingAddress] [nvarchar](max) NULL,
	[RS] [nvarchar](max) NULL,
	[KS] [nvarchar](max) NULL,
	[TransitId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_En] [nvarchar](128) NOT NULL,
	[Name_Ru] [nvarchar](128) NOT NULL,
	[Code] [char](2) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Forwarder]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forwarder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Forwarder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sender]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sender](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Sender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[State]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_dbo.State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateLocalization]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StateLocalization](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[TwoLetterISOLanguageName] [char](2) NOT NULL,
	[StateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.StateLocalization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transit]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[RecipientName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[WarehouseWorkingTime] [nvarchar](max) NULL,
	[MethodOfTransitId] [int] NOT NULL,
	[DeliveryTypeId] [int] NOT NULL,
	[CarrierId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Transit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](320) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[TwoLetterISOLanguageName] [char](2) NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VisibleState]    Script Date: 10/27/2013 1:34:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisibleState](
	[RoleId] [int] NOT NULL,
	[StateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.VisibleState] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Admin]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BrockerId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_BrockerId] ON [dbo].[AirWaybill]
(
	[BrokerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Reference_Bill]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Reference_Bill] ON [dbo].[AirWaybill]
(
	[Bill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Application_TransitId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Application_TransitId] ON [dbo].[Application]
(
	[TransitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ClientId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ClientId] ON [dbo].[Application]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReferenceId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_ReferenceId] ON [dbo].[Application]
(
	[AirWaybillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_StateId] ON [dbo].[Application]
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AvailableState_RoleId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AvailableState_RoleId] ON [dbo].[AvailableState]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AvailableState_StateId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AvailableState_StateId] ON [dbo].[AvailableState]
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Broker]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Carrier_Name]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Carrier_Name] ON [dbo].[Carrier]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TransitId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_TransitId] ON [dbo].[Client]
(
	[TransitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Client]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Forwarder]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Sender]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateLocalization_StateId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_StateLocalization_StateId] ON [dbo].[StateLocalization]
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarrierId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarrierId] ON [dbo].[Transit]
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_User_Login]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Login] ON [dbo].[User]
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VisibleState_RoleId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_VisibleState_RoleId] ON [dbo].[VisibleState]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VisibleState_StateId]    Script Date: 10/27/2013 1:34:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_VisibleState_StateId] ON [dbo].[VisibleState]
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AirWaybill] ADD  CONSTRAINT [DF_AirWaybillCreationTimestamp]  DEFAULT (getdate()) FOR [CreationTimestamp]
GO
ALTER TABLE [dbo].[AirWaybill] ADD  CONSTRAINT [DF__AirWaybil__State__1B0907CE]  DEFAULT (getdate()) FOR [StateChangeTimestamp]
GO
ALTER TABLE [dbo].[Application] ADD  CONSTRAINT [DF_Application_CreationTimestamp]  DEFAULT (getdate()) FOR [CreationTimestamp]
GO
ALTER TABLE [dbo].[StateLocalization] ADD  DEFAULT ('ru') FOR [TwoLetterISOLanguageName]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ('ru') FOR [TwoLetterISOLanguageName]
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Admin_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_dbo.Admin_dbo.User_UserId]
GO
ALTER TABLE [dbo].[AirWaybill]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reference_dbo.Brocker_BrockerId] FOREIGN KEY([BrokerId])
REFERENCES [dbo].[Broker] ([Id])
GO
ALTER TABLE [dbo].[AirWaybill] CHECK CONSTRAINT [FK_dbo.Reference_dbo.Brocker_BrockerId]
GO
ALTER TABLE [dbo].[AirWaybill]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reference_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[AirWaybill] CHECK CONSTRAINT [FK_dbo.Reference_dbo.State_StateId]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId] FOREIGN KEY([AirWaybillId])
REFERENCES [dbo].[AirWaybill] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.AirWaybill_AirWaybillId]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.State_StateId]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId] FOREIGN KEY([TransitId])
REFERENCES [dbo].[Transit] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId]
GO
ALTER TABLE [dbo].[AvailableState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AvailableState_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AvailableState] CHECK CONSTRAINT [FK_dbo.AvailableState_dbo.State_StateId]
GO
ALTER TABLE [dbo].[Broker]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Brocker_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Broker] CHECK CONSTRAINT [FK_dbo.Brocker_dbo.User_UserId]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Client_dbo.Transit_Transit_Id] FOREIGN KEY([TransitId])
REFERENCES [dbo].[Transit] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_dbo.Client_dbo.Transit_Transit_Id]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Client_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_dbo.Client_dbo.User_UserId]
GO
ALTER TABLE [dbo].[Forwarder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forwarder] CHECK CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId]
GO
ALTER TABLE [dbo].[Sender]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sender_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sender] CHECK CONSTRAINT [FK_dbo.Sender_dbo.User_UserId]
GO
ALTER TABLE [dbo].[StateLocalization]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StateLocalization_dbo.State_State_Id] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StateLocalization] CHECK CONSTRAINT [FK_dbo.StateLocalization_dbo.State_State_Id]
GO
ALTER TABLE [dbo].[Transit]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transit_dbo.Carrier_Carrier_Id] FOREIGN KEY([CarrierId])
REFERENCES [dbo].[Carrier] ([Id])
GO
ALTER TABLE [dbo].[Transit] CHECK CONSTRAINT [FK_dbo.Transit_dbo.Carrier_Carrier_Id]
GO
ALTER TABLE [dbo].[VisibleState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.VisibleState_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VisibleState] CHECK CONSTRAINT [FK_dbo.VisibleState_dbo.State_StateId]
GO