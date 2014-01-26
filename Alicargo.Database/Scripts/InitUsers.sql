/****** Object:  Table [dbo].[Carrier]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Carrier] ON
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (1, N'Carrier 0')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (2, N'Carrier 1')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (3, N'Carrier 2')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (4, N'Carrier 3')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (5, N'Carrier 4')
SET IDENTITY_INSERT [dbo].[Carrier] OFF


/****** Object:  Table [dbo].[Transit]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Transit] ON
INSERT [dbo].[Transit] ([Id], [CityId], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES
(1, 10, N'Address 0', N'Recipient 0', N'Phone 0', 0, 0, 1),
(2, 1, N'Address 1', N'Recipient 1', N'Phone 1', 1, 1, 2),
(3, 2, N'Address 2', N'Recipient 2', N'Phone 2', 2, 0, 3),
(4, 3, N'Address 3', N'Recipient 3', N'Phone 3', 0, 1, 4),
(5, 4, N'Address 4', N'Recipient 4', N'Phone 4', 1, 0, 5),
(6, 10, N'Address 0', N'RecipientName 0', N'Phone 0', 0, 0, 1),
(7, 1, N'Address 1', N'RecipientName 1', N'Phone 1', 1, 1, 2),
(8, 2, N'Address 2', N'RecipientName 2', N'Phone 2', 2, 0, 3),
(9, 3, N'Address 3', N'RecipientName 3', N'Phone 3', 0, 1, 4),
(10, 4, N'Address 4', N'RecipientName 4', N'Phone 4', 1, 0, 5),
(11, 5, N'Address 5', N'RecipientName 5', N'Phone 5', 2, 1, 1),
(12, 6, N'Address 6', N'RecipientName 6', N'Phone 6', 0, 0, 2),
(13, 7, N'Address 7', N'RecipientName 7', N'Phone 7', 1, 1, 3),
(14, 8, N'Address 8', N'RecipientName 8', N'Phone 8', 2, 0, 4),
(15, 9, N'Address 9', N'RecipientName 9', N'Phone 9', 0, 1, 5),
(16, 10, N'Address 10', N'RecipientName 10', N'Phone 10', 1, 0, 1),
(17, 11, N'Address 11', N'RecipientName 11', N'Phone 11', 2, 1, 2),
(18, 12, N'Address 12', N'RecipientName 12', N'Phone 12', 0, 0, 3),
(19, 13, N'Address 13', N'RecipientName 13', N'Phone 13', 1, 1, 4),
(20, 14, N'Address 14', N'RecipientName 14', N'Phone 14', 2, 0, 5),
(21, 15, N'Address 15', N'RecipientName 15', N'Phone 15', 0, 1, 1),
(22, 16, N'Address 16', N'RecipientName 16', N'Phone 16', 1, 0, 2),
(23, 7, N'Address 17', N'RecipientName 17', N'Phone 17', 2, 1, 3),
(24, 8, N'Address 18', N'RecipientName 18', N'Phone 18', 0, 0, 4),
(25, 9, N'Address 19', N'RecipientName 19', N'Phone 19', 1, 1, 5),
(26, 10, N'Address 20', N'RecipientName 20', N'Phone 20', 2, 0, 1),
(27, 1, N'Address 21', N'RecipientName 21', N'Phone 21', 0, 1, 2),
(28, 2, N'Address 22', N'RecipientName 22', N'Phone 22', 1, 0, 3),
(29, 3, N'Address 23', N'RecipientName 23', N'Phone 23', 2, 1, 4),
(30, 4, N'Address 24', N'RecipientName 24', N'Phone 24', 0, 0, 5)
SET IDENTITY_INSERT [dbo].[Transit] OFF


/****** Object:  Table [dbo].[User]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[User] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES
(1, N'Admin', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru'),
(2, N'Broker', 0xC94A76282D55FDECBFCE1B6CB6AFCB3B52DC54B021D544D64E2BC8CBA91D9263, 0x95C6887A0166FC4C905DE2D112C6D011, N'ru'),
(3, N'Forwarder', 0xB9CBD42D8C848D09917995045022ECADF7789301E24DD84F6AD05D574368A056, 0x911C9CB1AE955341A11C4E6EB4A4013B, N'ru'),
(4, N'Sender', 0x72CCEE115CA2264BAF818D81ACC90AFE1343CEC1C21B98B6A31A6DF0EBDFF0F5, 0x929F9FA36D67C242A4113CBE6A49952A, N'ru'),
(11, N'Sender2', 0x72CCEE115CA2264BAF818D81ACC90AFE1343CEC1C21B98B6A31A6DF0EBDFF0F5, 0x929F9FA36D67C242A4113CBE6A49952A, N'ru'),
(5, N'c1', 0xF0EA44E7CEF6B282139A8EF5A139ED46B476D93955965B903F03895E8EB446EA, 0x633F51634E9D5341A4AF127F099E6084, N'ru'),
(6, N'c2', 0xA12AA505073C2A2C12FA2554EA633349EFDCCCC14EB58F3BDEE08B99B4F0BF47, 0x27F15ADF0E71134B9DDD8570C7B4558E, N'ru'),
(7, N'c3', 0x3372842A198B88A98E973525FC4AA9AC0F84BC2947124D41AF78B3BFAB95D413, 0xFF8D3301277CEB4DB0F7FE2DA41DD0DE, N'ru'),
(8, N'c4', 0x15680C591A987BF6F531C7F646713927F0DECBA810D54C5004C3D1C5BDF880FC, 0x62725047636AC547B80C2E5385BAB143, N'ru'),
(9, N'c5', 0x02DD1CD2AF54FA92AA9A51A584497BF21CECD2B9EC15A1F369C5F3731E85E209, 0xB1AFC47E2F09104D9050A8607CCA2590, N'ru'),
(10, N'Forwarder2', 0xB9CBD42D8C848D09917995045022ECADF7789301E24DD84F6AD05D574368A056, 0x911C9CB1AE955341A11C4E6EB4A4013B, N'ru')
COMMIT;
SET IDENTITY_INSERT [dbo].[User] OFF;


/****** Object:  Table [dbo].[Broker]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Broker] ON
INSERT [dbo].[Broker] ([Id], [UserId], [Name], [Email]) VALUES (1, 2, N'Broker', N'Broker@timez.org')
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
INSERT [dbo].[Sender] ([Id], [UserId], [Name], [Email]) VALUES (1, 4, N'Sender1', N'Sender1@timez.org')
INSERT [dbo].[Sender] ([Id], [UserId], [Name], [Email]) VALUES (2, 11, N'Sender2', N'Sender2@timez.org')
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