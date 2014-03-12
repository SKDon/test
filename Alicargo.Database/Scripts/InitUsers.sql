/****** Object:  Table [dbo].[User]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[User] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES
(1, N'Admin', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru'),
(2, N'Broker1', NEWID(), NEWID(), N'ru'),
(14, N'Broker2', NEWID(), NEWID(), N'ru'),
(3, N'Forwarder1', NEWID(), NEWID(), N'ru'),
(4, N'Sender1', NEWID(), NEWID(), N'ru'),
(5, N'c1', NEWID(), NEWID(), N'ru'),
(6, N'c2', NEWID(), NEWID(), N'ru'),
(7, N'c3', NEWID(), NEWID(), N'ru'),
(8, N'c4', NEWID(), NEWID(), N'ru'),
(9, N'c5', NEWID(), NEWID(), N'ru'),
(10, N'Forwarder2', NEWID(), NEWID(), N'ru'),
(11, N'Sender2', NEWID(), NEWID(), N'ru'),
(12, N'Carrier1', NEWID(), NEWID(), N'ru'),
(13, N'Carrier2', NEWID(), NEWID(), N'ru')
COMMIT;
SET IDENTITY_INSERT [dbo].[User] OFF;

/****** Object:  Table [dbo].[Carrier]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Carrier] ON
INSERT [dbo].[Carrier] ([Id], [UserId], [Name], [Email], [Contact], [Address], [Phone]) VALUES 
(1, 12, N'Carrier1', N'Carrier1@timez.org', N'Contact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50))),
(2, 13, N'Carrier2', N'Carrier2@timez.org', N'Contact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50)))
SET IDENTITY_INSERT [dbo].[Carrier] OFF

INSERT [dbo].[CarrierCity] ([CityId], [CarrierId])
SELECT [Id] AS [CityId], 1 AS [CarrierId]
FROM [dbo].[City]
WHERE [Id] < 8

INSERT [dbo].[CarrierCity] ([CityId], [CarrierId])
SELECT [Id] AS [CityId], 2 AS [CarrierId]
FROM [dbo].[City]
WHERE [Id] >= 8


/****** Object:  Table [dbo].[Transit]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Transit] ON
INSERT [dbo].[Transit] ([Id], [CityId], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES
(1, 10, N'Address 0', N'Recipient 0', N'Phone 0', 0, 0, 1),
(2, 1, N'Address 1', N'Recipient 1', N'Phone 1', 1, 1, 2),
(3, 2, N'Address 2', N'Recipient 2', N'Phone 2', 2, 0, 1),
(4, 3, N'Address 3', N'Recipient 3', N'Phone 3', 0, 1, 1),
(5, 4, N'Address 4', N'Recipient 4', N'Phone 4', 1, 0, 1),
(6, 10, N'Address 0', N'RecipientName 0', N'Phone 0', 0, 0, 1),
(7, 1, N'Address 1', N'RecipientName 1', N'Phone 1', 1, 1, 2),
(8, 2, N'Address 2', N'RecipientName 2', N'Phone 2', 2, 0, 2),
(9, 3, N'Address 3', N'RecipientName 3', N'Phone 3', 0, 1, 2),
(10, 4, N'Address 4', N'RecipientName 4', N'Phone 4', 1, 0, 1),
(11, 5, N'Address 5', N'RecipientName 5', N'Phone 5', 2, 1, 1),
(12, 6, N'Address 6', N'RecipientName 6', N'Phone 6', 0, 0, 1),
(13, 7, N'Address 7', N'RecipientName 7', N'Phone 7', 1, 1, 1),
(14, 8, N'Address 8', N'RecipientName 8', N'Phone 8', 2, 0, 1),
(15, 9, N'Address 9', N'RecipientName 9', N'Phone 9', 0, 1, 1),
(16, 10, N'Address 10', N'RecipientName 10', N'Phone 10', 1, 0, 2),
(17, 11, N'Address 11', N'RecipientName 11', N'Phone 11', 2, 1, 2),
(18, 12, N'Address 12', N'RecipientName 12', N'Phone 12', 0, 0, 2),
(19, 13, N'Address 13', N'RecipientName 13', N'Phone 13', 1, 1, 2),
(20, 14, N'Address 14', N'RecipientName 14', N'Phone 14', 2, 0, 2),
(21, 15, N'Address 15', N'RecipientName 15', N'Phone 15', 0, 1, 2),
(22, 16, N'Address 16', N'RecipientName 16', N'Phone 16', 1, 0, 2),
(23, 7, N'Address 17', N'RecipientName 17', N'Phone 17', 2, 1, 2),
(24, 8, N'Address 18', N'RecipientName 18', N'Phone 18', 0, 0, 2),
(25, 9, N'Address 19', N'RecipientName 19', N'Phone 19', 1, 1, 2),
(26, 10, N'Address 20', N'RecipientName 20', N'Phone 20', 2, 0, 1),
(27, 1, N'Address 21', N'RecipientName 21', N'Phone 21', 0, 1, 1),
(28, 2, N'Address 22', N'RecipientName 22', N'Phone 22', 1, 0, 1),
(29, 3, N'Address 23', N'RecipientName 23', N'Phone 23', 2, 1, 1),
(30, 4, N'Address 24', N'RecipientName 24', N'Phone 24', 0, 0, 1)
SET IDENTITY_INSERT [dbo].[Transit] OFF



/****** Object:  Table [dbo].[Broker]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Broker] ON
INSERT [dbo].[Broker] ([Id], [UserId], [Name], [Email]) VALUES 
(1, 2, N'Broker1', N'Broker1@timez.org'),
(2, 14, N'Broker2', N'Broker2@timez.org')
SET IDENTITY_INSERT [dbo].[Broker] OFF


/****** Object:  Table [dbo].[Admin]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Admin] ON
INSERT [dbo].[Admin] ([Id], [UserId], [Name], [Email]) VALUES (1, 1, N'Admin', N'Admin@timez.org')
SET IDENTITY_INSERT [dbo].[Admin] OFF


/****** Object:  Table [dbo].[Forwarder]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Forwarder] ON
INSERT [dbo].[Forwarder] ([Id], [UserId], [Name], [Email]) VALUES (1, 3, N'Forwarder1', N'Forwarder1@timez.org')
INSERT [dbo].[Forwarder] ([Id], [UserId], [Name], [Email]) VALUES (2, 9, N'Forwarder2', N'Forwarder2@timez.org')
SET IDENTITY_INSERT [dbo].[Forwarder] OFF

INSERT [dbo].[ForwarderCity] ([CityId], [ForwarderId])
SELECT [Id] AS [CityId], 1 AS [ForwarderId]
FROM [dbo].[City]
WHERE [Id] < 8

INSERT [dbo].[ForwarderCity] ([CityId], [ForwarderId])
SELECT [Id] AS [CityId], 2 AS [ForwarderId]
FROM [dbo].[City]
WHERE [Id] >= 8


/****** Object:  Table [dbo].[Sender]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Sender] ON
INSERT [dbo].[Sender] ([Id], [UserId], [Name], [Email], [Contact], [Address], [Phone]) VALUES 
(1, 4, N'Sender1', N'Sender1@timez.org', N'Contact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50))),
(2, 11, N'Sender2', N'Sender2@timez.org', N'Contact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50)))
SET IDENTITY_INSERT [dbo].[Sender] OFF

INSERT [dbo].[SenderCountry] ([CountryId], [SenderId])
SELECT [Id] AS [CountryId], 1 AS [SenderId]
FROM [dbo].[Country]
WHERE [Id] < 127

INSERT [dbo].[SenderCountry] ([CountryId], [SenderId])
SELECT [Id] AS [CountryId], 2 AS [SenderId]
FROM [dbo].[Country]
WHERE [Id] >= 127


/****** Object:  Table [dbo].[Client]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Client] ON
INSERT [dbo].[Client] ([Id], [UserId], [Emails], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES 
(1, 5, N'u1@timez.org, u12@timez.org; u13@timez.org', N'Nic 0', N'LegalEntity 0', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 1),
(2, 6, N'u2@timez.org', N'Nic 1', N'LegalEntity 1', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 2),
(3, 7, N'u3@timez.org', N'Nic 2', N'LegalEntity 2', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 3),
(4, 8, N'u4@timez.org', N'Nic 3', N'LegalEntity 3', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 4),
(5, 9, N'u5@timez.org', N'Nic 4', N'LegalEntity 4', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 5)
SET IDENTITY_INSERT [dbo].[Client] OFF