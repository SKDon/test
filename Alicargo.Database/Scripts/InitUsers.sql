/****** Object:  Table [dbo].[User]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[User] ON;
INSERT INTO [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES
(1, N'Admin', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru')
INSERT INTO [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES
(2, N'Broker1', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru'),
(3, N'Forwarder1', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru'),
(4, N'Sender1', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(5, N'c1', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(6, N'c2', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(7, N'c3', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(8, N'c4', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(9, N'c5', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(10, N'Forwarder2', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(11, N'Sender2', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(12, N'Carrier1', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(13, N'Carrier2', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(14, N'Broker2', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru'),
(15, N'Manager1', CAST(NEWID() AS VARBINARY(MAX)), CAST(NEWID() AS VARBINARY(MAX)), N'ru')
SET IDENTITY_INSERT [dbo].[User] OFF;

SET IDENTITY_INSERT [dbo].[Carrier] ON
INSERT [dbo].[Carrier] ([Id], [UserId], [Name], [Email], [Contact], [Address], [Phone]) VALUES 
(1, 12, N'CarrierName1', N'Carrier1@timez.org', N'Contact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50))),
(2, 13, N'CarrierName2', N'Carrier2@timez.org', N'Contact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50)))
SET IDENTITY_INSERT [dbo].[Carrier] OFF

INSERT [dbo].[CarrierCity] ([CityId], [CarrierId])
SELECT [Id] AS [CityId], 1 AS [CarrierId]
FROM [dbo].[City]
WHERE [Id] < 8

INSERT [dbo].[CarrierCity] ([CityId], [CarrierId])
SELECT [Id] AS [CityId], 2 AS [CarrierId]
FROM [dbo].[City]
WHERE [Id] >= 8


SET IDENTITY_INSERT [dbo].[Transit] ON
INSERT [dbo].[Transit] ([Id], [CityId], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES
(1, 10, N'Address 0', N'Recipient 0', N'Phone 0', 0, 0, 1),
(2, 1, N'Address 1', N'Recipient 1', N'Phone 1', 1, 1, 2),
(3, 2, N'Address 2', N'Recipient 2', N'Phone 2', 2, 0, 1),
(4, 3, N'Address 3', N'Recipient 3', N'Phone 3', 0, 1, 1),
(5, 4, N'Address 4', N'Recipient 4', N'Phone 4', 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Transit] OFF



/****** Object:  Table [dbo].[Broker]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Broker] ON
INSERT [dbo].[Broker] ([Id], [UserId], [Name], [Email]) VALUES 
(1, 2, N'BrokerName1', N'Broker1@timez.org'),
(2, 14, N'BrokerName2', N'Broker2@timez.org')
SET IDENTITY_INSERT [dbo].[Broker] OFF


/****** Object:  Table [dbo].[Admin]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Admin] ON
INSERT [dbo].[Admin] ([Id], [UserId], [Name], [Email]) VALUES (1, 1, N'AdminName', N'Admin@timez.org')
SET IDENTITY_INSERT [dbo].[Admin] OFF


/****** Object:  Table [dbo].[Forwarder]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Forwarder] ON
INSERT [dbo].[Forwarder] ([Id], [UserId], [Name], [Email]) VALUES (1, 3, N'ForwarderName1', N'Forwarder1@timez.org')
INSERT [dbo].[Forwarder] ([Id], [UserId], [Name], [Email]) VALUES (2, 9, N'ForwarderName2', N'Forwarder2@timez.org')
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
(1, 4, N'SenderName1', N'Sender1@timez.org', N'SenderContact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50))),
(2, 11, N'SenderName2', N'Sender2@timez.org', N'SenderContact_' + CAST(NEWID() AS NVARCHAR(50)), N'Address_' + CAST(NEWID() AS NVARCHAR(50)), N'Phone_' + CAST(NEWID() AS NVARCHAR(50)))
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
INSERT [dbo].[Client] 
([Id], [UserId], [Emails], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], 
[Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId], [ContractDate], [ContractNumber],
[DefaultSenderId]) VALUES 
(1, 5, N'u1@timez.org, u12@timez.org; u13@timez.org', N'Nic 0', N'LegalEntity 0', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 1, GETUTCDATE(), 1, NULL),
(2, 6, N'u2@timez.org', N'Nic 1', N'LegalEntity 1', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 2, GETUTCDATE(), 2, 1),
(3, 7, N'u3@timez.org', N'Nic 2', N'LegalEntity 2', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 3, GETUTCDATE(), 3, 2),
(4, 8, N'u4@timez.org', N'Nic 3', N'LegalEntity 3', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 4, GETUTCDATE(), 4, NULL),
(5, 9, N'u5@timez.org', N'Nic 4', N'LegalEntity 4', NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), NEWID(), 5, GETUTCDATE(), 5, 1)
SET IDENTITY_INSERT [dbo].[Client] OFF

SET IDENTITY_INSERT [dbo].[Manager] ON
INSERT [dbo].[Manager]
	([Id], [UserId], [Name], [Email]) VALUES
(1, 15, 'Manager1Name', 'Manager1@mail.ru')
SET IDENTITY_INSERT [dbo].[Manager] OFF