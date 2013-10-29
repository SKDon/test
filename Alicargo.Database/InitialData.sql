﻿/****** Object:  Table [dbo].[State]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[State] ON
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (1, N'New', 10) -- Новая заявка
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (3, N'Cargo is not ready', 20) -- Груз не готов
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (13, N'Factory does not respond', 30) -- Фабрика не отвечает
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (2, N'Factory awaits payment', 40) -- Фабрика ждет оплату
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (10, N'Factory cargo sent', 50) -- Фабрика отправила груз
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (15, N'Cargo ready to pick up', 55) -- Груз готов для забора
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (4, N'Cargo pick up', 60) -- Груз забран на фабрике
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (6, N'Cargo in stock', 70) -- Груз на складе
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (7, N'Cargo flew', 90) -- Груз вылетел
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (8, N'Cargo at customs', 100) -- Груз на таможне
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (9, N'Customs cleared cargo', 110) -- Груз выпущен
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (12, N'On transit', 120) -- На транзите
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (14, N'Stop', 130) -- Груз на стопе
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (11, N'Cargo received', 140) -- Груз получен
SET IDENTITY_INSERT [dbo].[State] OFF
/****** Object:  Table [dbo].[Carrier]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Carrier] ON
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (1, N'Carrier 0')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (2, N'Carrier 1')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (3, N'Carrier 2')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (4, N'Carrier 3')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (5, N'Carrier 4')
SET IDENTITY_INSERT [dbo].[Carrier] OFF
/****** Object:  Table [dbo].[User]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[User] ON;
BEGIN TRANSACTION;
INSERT INTO [dbo].[User]([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName])
SELECT 1, N'Admin', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru' UNION ALL
SELECT 2, N'Broker', 0xC94A76282D55FDECBFCE1B6CB6AFCB3B52DC54B021D544D64E2BC8CBA91D9263, 0x95C6887A0166FC4C905DE2D112C6D011, N'ru' UNION ALL
SELECT 3, N'Forwarder', 0xB9CBD42D8C848D09917995045022ECADF7789301E24DD84F6AD05D574368A056, 0x911C9CB1AE955341A11C4E6EB4A4013B, N'ru' UNION ALL
SELECT 4, N'Sender', 0x72CCEE115CA2264BAF818D81ACC90AFE1343CEC1C21B98B6A31A6DF0EBDFF0F5, 0x929F9FA36D67C242A4113CBE6A49952A, N'ru' UNION ALL
SELECT 5, N'1', 0xF0EA44E7CEF6B282139A8EF5A139ED46B476D93955965B903F03895E8EB446EA, 0x633F51634E9D5341A4AF127F099E6084, N'ru' UNION ALL
SELECT 6, N'c1', 0xA12AA505073C2A2C12FA2554EA633349EFDCCCC14EB58F3BDEE08B99B4F0BF47, 0x27F15ADF0E71134B9DDD8570C7B4558E, N'ru' UNION ALL
SELECT 7, N'c2', 0x3372842A198B88A98E973525FC4AA9AC0F84BC2947124D41AF78B3BFAB95D413, 0xFF8D3301277CEB4DB0F7FE2DA41DD0DE, N'ru' UNION ALL
SELECT 8, N'3', 0x15680C591A987BF6F531C7F646713927F0DECBA810D54C5004C3D1C5BDF880FC, 0x62725047636AC547B80C2E5385BAB143, N'ru' UNION ALL
SELECT 9, N'c4', 0x02DD1CD2AF54FA92AA9A51A584497BF21CECD2B9EC15A1F369C5F3731E85E209, 0xB1AFC47E2F09104D9050A8607CCA2590, N'ru'
COMMIT;
SET IDENTITY_INSERT [dbo].[User] OFF;
/****** Object:  Table [dbo].[Transit]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Transit] ON
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (1, N'City 0', N'Address 0', N'Recipient 0', N'Phone 0', 0, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (2, N'City 1', N'Address 1', N'Recipient 1', N'Phone 1', 1, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (3, N'City 2', N'Address 2', N'Recipient 2', N'Phone 2', 2, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (4, N'City 3', N'Address 3', N'Recipient 3', N'Phone 3', 0, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (5, N'City 4', N'Address 4', N'Recipient 4', N'Phone 4', 1, 0, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (6, N'City 0', N'Address 0', N'RecipientName 0', N'Phone 0', 0, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (7, N'City 1', N'Address 1', N'RecipientName 1', N'Phone 1', 1, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (8, N'City 2', N'Address 2', N'RecipientName 2', N'Phone 2', 2, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (9, N'City 3', N'Address 3', N'RecipientName 3', N'Phone 3', 0, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (10, N'City 4', N'Address 4', N'RecipientName 4', N'Phone 4', 1, 0, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (11, N'City 5', N'Address 5', N'RecipientName 5', N'Phone 5', 2, 1, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (12, N'City 6', N'Address 6', N'RecipientName 6', N'Phone 6', 0, 0, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (13, N'City 7', N'Address 7', N'RecipientName 7', N'Phone 7', 1, 1, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (14, N'City 8', N'Address 8', N'RecipientName 8', N'Phone 8', 2, 0, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (15, N'City 9', N'Address 9', N'RecipientName 9', N'Phone 9', 0, 1, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (16, N'City 10', N'Address 10', N'RecipientName 10', N'Phone 10', 1, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (17, N'City 11', N'Address 11', N'RecipientName 11', N'Phone 11', 2, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (18, N'City 12', N'Address 12', N'RecipientName 12', N'Phone 12', 0, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (19, N'City 13', N'Address 13', N'RecipientName 13', N'Phone 13', 1, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (20, N'City 14', N'Address 14', N'RecipientName 14', N'Phone 14', 2, 0, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (21, N'City 15', N'Address 15', N'RecipientName 15', N'Phone 15', 0, 1, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (22, N'City 16', N'Address 16', N'RecipientName 16', N'Phone 16', 1, 0, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (23, N'City 17', N'Address 17', N'RecipientName 17', N'Phone 17', 2, 1, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (24, N'City 18', N'Address 18', N'RecipientName 18', N'Phone 18', 0, 0, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (25, N'City 19', N'Address 19', N'RecipientName 19', N'Phone 19', 1, 1, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (26, N'City 20', N'Address 20', N'RecipientName 20', N'Phone 20', 2, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (27, N'City 21', N'Address 21', N'RecipientName 21', N'Phone 21', 0, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (28, N'City 22', N'Address 22', N'RecipientName 22', N'Phone 22', 1, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (29, N'City 23', N'Address 23', N'RecipientName 23', N'Phone 23', 2, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (30, N'City 24', N'Address 24', N'RecipientName 24', N'Phone 24', 0, 0, 5)
SET IDENTITY_INSERT [dbo].[Transit] OFF
/****** Object:  Table [dbo].[StateLocalization]    Script Date: 05/18/2013 14:17:27 ******/
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Новая заявка', N'ru', 1);
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Nuovo', N'it', 1)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'New order', N'en', 1)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Фабрика ждет оплату', N'ru', 2)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Fabbrica attende il pagamento', N'it', 2)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Factory awaits payment', N'en', 2)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз не готов', N'ru', 3)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car non è pronto', N'it', 3)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car is not ready', N'en', 3)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз забран на фабрике', N'ru', 4)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car è ritirata in fabbrica', N'it', 4)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car is picked', N'en', 4)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз на складе', N'ru', 6)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car in magazzino', N'it', 6)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car in warehouse', N'en', 6)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз вылетел', N'ru', 7)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car volò', N'it', 7)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car departed', N'en', 7)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз на таможне', N'ru', 8)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car alla dogana', N'it', 8)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car at customs', N'en', 8)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз выпущен', N'ru', 9)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Sdoganate car', N'it', 9)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Customs cleared car', N'en', 9)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Фабрика отправила груз', N'ru', 10)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Carico di fabbrica inviato', N'it', 10)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Factory car sent', N'en', 10)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз получен', N'ru', 11)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car ha ricevuto', N'it', 11)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car received', N'en', 11)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'На транзите', N'ru', 12)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Sul transito', N'it', 12)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'On transit', N'en', 12)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Фабрика не отвечает', N'ru', 13)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Fabbrica non risponde', N'it', 13)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Factory does not respond', N'en', 13)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз на стопе', N'ru', 14)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Stop', N'it', 14)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Stop', N'en', 14)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Груз готов для забора', N'ru', 15)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car ready to pick-up', N'it', 15)
INSERT [dbo].[StateLocalization] ([Name], [TwoLetterISOLanguageName], [StateId]) VALUES (N'Car ready to pick-up', N'en', 15)
/****** Object:  Table [dbo].[Broker]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Broker] ON
INSERT [dbo].[Broker] ([Id], [UserId], [Name], [Email]) VALUES (1, 2, N'Broker', N'Broker@timez.org')
SET IDENTITY_INSERT [dbo].[Broker] OFF
/****** Object:  Table [dbo].[AvailableState]    Script Date: 05/18/2013 14:17:27 ******/
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 7)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (3, 7)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (3, 8)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (3, 9)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (5, 12)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 15)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 15)

INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 2)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 3)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 4)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 6)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (3, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (3, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (3, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 10)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 11)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 13)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 14)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 14)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 15)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 15)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 15)

/****** Object:  Table [dbo].[Admin]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Admin] ON
INSERT [dbo].[Admin] ([Id], [UserId], [Name], [Email]) VALUES (1, 1, N'Admin', N'Admin@timez.org')
SET IDENTITY_INSERT [dbo].[Admin] OFF
/****** Object:  Table [dbo].[Forwarder]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Forwarder] ON
INSERT [dbo].[Forwarder] ([Id], [UserId], [Name], [Email]) VALUES (1, 3, N'Forwarder', N'Forwarder@timez.org')
SET IDENTITY_INSERT [dbo].[Forwarder] OFF
/****** Object:  Table [dbo].[Sender]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Sender] ON
INSERT [dbo].[Sender] ([Id], [UserId], [Name], [Email]) VALUES (1, 4, N'Sender', N'Sender@timez.org')
SET IDENTITY_INSERT [dbo].[Sender] OFF
/****** Object:  Table [dbo].[AirWaybill]    Script Date: 05/18/2013 14:17:27 ******/
INSERT [dbo].[AirWaybill] ([CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [StateId]) VALUES (CAST(GETDATE() - 3 AS DateTimeOffset), NEWID(), NEWID(), NEWID(), CAST(GETDATE() AS DateTimeOffset), CAST(GETDATE() AS DateTimeOffset), 1, NEWID(), 7)

INSERT [dbo].[AirWaybill] ([CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [StateId]) VALUES (CAST(GETDATE() - 2 AS DateTimeOffset), NEWID(), N'ArrivalAirport', N'DepartureAirport', CAST(GETDATE() AS DateTimeOffset), CAST(GETDATE() AS DateTimeOffset), 1, NEWID(), 7)

INSERT [dbo].[AirWaybill] ([CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [StateId]) VALUES (CAST(GETDATE() - 1 AS DateTimeOffset), NEWID(), N'ArrivalAirport', N'DepartureAirport', CAST(GETDATE() AS DateTimeOffset), CAST(GETDATE() AS DateTimeOffset), 1, NEWID(), 7)

/****** Object:  Table [dbo].[Client]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[Client] ON
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (1, 5, N'u1@timez.org', N'Nic c50efc4d-4b52-4833-aeb5-9a59ff796029', N'LegalEntity 0', N'Contact 88fb1770-f405-4ca2-ae2f-251a9bc7593b', NULL, N'INN e18cbb8f-13f4-4f58-b5ed-81a45b9e8234', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (2, 6, N'u2@timez.org', N'Nic f9ae5e8b-e1e4-4aec-a86f-62dae76edc9e', N'LegalEntity 1', N'Contact 85eb1690-45fe-4b87-a0d1-3dfb2c5c1b43', NULL, N'INN afc62fc8-6088-49d1-9807-3d960e5f6aff', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (3, 7, N'u3@timez.org', N'Nic 8b1102be-f302-425c-8333-89f1e0f877ab', N'LegalEntity 2', N'Contact 970686ea-499b-4499-90b6-7225df9bf446', NULL, N'INN ce00c59c-abe6-4ffe-b2c7-83585e2460e9', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (4, 8, N'u4@timez.org', N'Nic a8efe6e4-333a-4b7c-b8d5-f382f6b42432', N'LegalEntity 3', N'Contact ee75723c-76ae-4e26-b94e-26a6312b41fb', NULL, N'INN e8d7859f-cfdf-407d-90a0-35c4190e0e8e', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (5, 9, N'u5@timez.org', N'Nic e53b6605-66f9-464c-bf6f-7d0a8f8b5a93', N'LegalEntity 4', N'Contact be0004f2-df24-415e-a626-96d439f70ee1', NULL, N'INN c0ae51d5-5438-40ac-866e-13abc2a7bc23', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5)
SET IDENTITY_INSERT [dbo].[Client] OFF

INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Abkhazia', N'Абхазия', N'  ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Afghanistan', N'Афганистан', N'AF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Aland Islands', N'Аландские острова', N'AX')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Albania', N'Албания', N'AL')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Algeria', N'Алжир', N'DZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'American Samoa', N'Американское Самоа', N'AS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Andorra', N'Андорра', N'AD')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Angola', N'Ангола', N'AO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Anguilla', N'Ангилья', N'AI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Antarctica', N'Антарктика', N'AQ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Antigua and Barbuda', N'Антигуа и Барбуда', N'AG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Argentina', N'Аргентина', N'AR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Armenia', N'Армения', N'AM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Aruba', N'Аруба', N'AW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Australia', N'Австралия', N'AU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Austria', N'Австрия', N'AT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Azerbaijan', N'Азербайджан', N'AZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Azores', N'Азорские острова', N'PT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bahamas', N'Багамские Острова', N'BS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bahrain', N'Бахрейн', N'BH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bangladesh', N'Бангладеш', N'BD')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Barbados', N'Барбадос', N'BB')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Belarus', N'Беларусь', N'BY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Belgium', N'Бельгия', N'BE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Belize', N'Белиз', N'BZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Benin', N'Бенин', N'BJ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bermuda', N'Бермудские Острова', N'BM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bolivia', N'Боливия', N'BO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bosnia-Herzegovina', N'Босния и Герцеговина', N'BA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Botswana', N'Ботсвана', N'BW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bouvet Island', N'Буве', N'BV')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Brazil', N'Бразилия', N'BR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'British Indian Ocean Territory', N'Британская территория в Индийском океане', N'IO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Brunei Darussalam', N'Бруней', N'BN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Bulgaria', N'Болгария', N'BG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Burkina Faso', N'Буркина-Фасо', N'BF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Burundi', N'Бурунди', N'BI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Buthan', N'Бутан', N'BT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cambodia', N'Камбоджа', N'KH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cameroon', N'Камерун', N'CM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Canada', N'Канада', N'CA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cape Verde', N'Кабо-Верде', N'CV')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cayman Islands', N'Каймановы Острова', N'KY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Central African Rep.', N'Центральноафриканская Республика', N'CF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Chad', N'Чад', N'TD')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Chile', N'Чили', N'CL')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'China', N'Китай', N'CN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Christmas Island', N'Остров Рождества', N'CX')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cocos (Keeling) Isl.', N'Кокосовые Острова', N'CC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Colombia', N'Колумбия', N'CO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Comoros', N'Коморские Острова', N'KM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cook Islands', N'Кука острова', N'CK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Costa Rica', N'Коста-Рика', N'CR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Croatia', N'Хорватия', N'HR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cuba', N'Куба', N'CU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Cyprus', N'Кипр', N'CY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Czech Republic', N'Чехия', N'CZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Democratic Republic of the Congo', N'Конго, Демократическая Республика', N'CD')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Denmark', N'Дания', N'DK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Djibouti', N'Джибути', N'DJ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Dominica', N'Доминика', N'DM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Dominican Republic', N'Доминиканская Республика', N'DO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'East Timor', N'Восточный Тимор', N'TP')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Ecuador', N'Эквадор', N'EC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Egypt', N'Египет', N'EG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'El Salvador', N'Сальвадор', N'SV')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Equatorial Guinea', N'Экваториальная Гвинея', N'GQ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Eritrea', N'Эритрея', N'ER')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Estonia', N'Эстония', N'EE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Ethiopia', N'Эфиопия', N'ET')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Falkland Islands', N'Фолклендские (Мальвинские) острова', N'FK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Faroe Islands', N'Фарерские Острова', N'FO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Fiji', N'Фиджи', N'FJ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Finland', N'Финляндия', N'FI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'France', N'Франция', N'FR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'French Polynesia', N'Французская Полинезия', N'PF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'French Southern and Antarctic Lands', N'Французские Южные и Антарктические Территории', N'TF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Gabon', N'Габон', N'GA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Gambia', N'Гамбия', N'GM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Georgia', N'Грузия', N'GE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Germany', N'Германия', N'DE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Ghana', N'Гана', N'GH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Gibraltar', N'Гибралтар', N'GI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Greece', N'Греция', N'GR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Greenland', N'Гренландия', N'GL')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Grenada', N'Гренада', N'GD')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guadeloupe (Fr.)', N'Гваделупа', N'GP')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guam (US)', N'Гуам', N'GU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guatemala', N'Гватемала', N'GT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guernsey', N'Гернси', N'GG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guinea', N'Гвинея', N'GN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guinea Bissau', N'Гвинея-Бисау', N'GW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guyana', N'Гвиана', N'GY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Guyana (Fr.)', N'Гайана', N'GF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Haiti', N'Гаити', N'HT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Heard Island and McDonald Islands', N'Острова Херд и Макдональд', N'HM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Honduras', N'Гондурас', N'HN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Hong Kong', N'Гонконг', N'HK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Hungary', N'Венгрия', N'HU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Iceland', N'Исландия', N'IS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'India', N'Индия', N'IN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Indonesia', N'Индонезия', N'ID')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Iran', N'Иран', N'IR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Iraq', N'Ирак', N'IQ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Ireland', N'Ирландия', N'IE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Isle of Man', N'Остров Мэн', N'IM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Israel', N'Израиль', N'IL')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Italy', N'Италия', N'IT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Ivory Coast', N'Кот-д''Ивуар', N'CI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Jamaica', N'Ямайка', N'JM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Japan', N'Япония', N'JP')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Jersey', N'Джерси', N'JE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Jordan', N'Иордания', N'JO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Kazachstan', N'Казахстан', N'KZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Kenya', N'Кения', N'KE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Kirgistan', N'Кыргызстан', N'KG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Kiribati', N'Кирибати', N'KI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Korea (North)', N'Корея (Северная)', N'KP')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Korea (South)', N'Корея (Южная)', N'KR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Kosovo', N'Косово', N'  ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Kuwait', N'Кувейт', N'KW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Laos', N'Лаос', N'LA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Latvia', N'Латвия', N'LV')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Lebanon', N'Ливан', N'LB')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Lesotho', N'Лесото', N'LS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Liberia', N'Либерия', N'LR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Libya', N'Ливия', N'LY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Liechtenstein', N'Лихтенштейн', N'LI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Lithuania', N'Литва', N'LT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Luxembourg', N'Люксембург', N'LU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Macau', N'Аомынь', N'MO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Madagascar', N'Мадагаскар', N'MG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Malawi', N'Малави', N'MW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Malaysia', N'Малайзия', N'MY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Maldives', N'Мальдивы', N'MV')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Mali', N'Мали', N'ML')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Malta', N'Мальта', N'MT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Marshall Islands', N'Маршалловы Острова', N'MH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Martinique (Fr.)', N'Мартиника', N'MQ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Mauritania', N'Мавритания', N'MR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Mauritius', N'Маврикий', N'MU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Mayotte', N'Майотта', N'YT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Mexico', N'Мексика', N'MX')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Micronesia', N'Микронезия', N'FM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Moldavia', N'Молдова', N'MD')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Monaco', N'Монако', N'MC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Mongolia', N'Монголия', N'MN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Montenegro', N'Черногория', N'ME')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Montserrat', N'Монтсеррат', N'MS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Morocco', N'Морокко', N'MA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Mozambique', N'Мозамбик', N'MZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Myanmar', N'Мьянма', N'MM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Nagorno-Karabakh Republic', N'Нагорно-Карабахская Республика', N'  ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Namibia', N'Намибия', N'NA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Nauru', N'Науру', N'NR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Nepal', N'Непал', N'NP')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Netherland Antilles', N'Антильские Острова', N'AN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Netherlands', N'Нидерланды', N'NL')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'New Caledonia (Fr.)', N'Новая Каледония', N'NC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'New Zealand', N'Новая Зеландия', N'NZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Nicaragua', N'Никарагуа', N'NI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Niger', N'Нигер', N'NE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Nigeria', N'Нигерия', N'NG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Niue', N'Ниуэ', N'NU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Norfolk Island', N'Норфолк', N'NF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Northern Cyprus', N'Турецкая Республика Северного Кипра', N'NC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Northern Mariana Isl.', N'Северные Марианские острова', N'MP')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Norway', N'Норвегия', N'NO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Oman', N'Оман', N'OM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Pakistan', N'Пакистан', N'PK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Palau', N'Палау', N'PW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Palestine', N'Палестина', N'PS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Panama', N'Панама', N'PA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Papua New', N'Папуа — Новая Гвинея', N'PG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Paraguay', N'Парагвай', N'PY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Peru', N'Перу', N'PE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Philippines', N'Филиппины', N'PH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Pitcairn', N'Питкэрн', N'PN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Poland', N'Польша', N'PL')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Portugal', N'Португалия', N'PT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Puerto Rico', N'Пуэрто-Рико', N'PR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Qatar', N'Катар', N'QA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Republic of Macedonia', N'Македония', N'MK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Republic of Somaliland', N'Сомалиленд', N'  ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Republic of the Congo', N'Республика Конго', N'CG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Reunion (Fr.)', N'Реюньон', N'RE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Romania', N'Румыния', N'RO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Russia', N'Россия', N'RU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Rwanda', N'Руанда', N'RW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Saint Lucia', N'Сент-Люсия', N'LC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Samoa', N'Самоа', N'WS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'San Marino', N'Сан-Марино', N'SM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Saudi Arabia', N'Саудовская Аравия', N'SA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Senegal', N'Сенегал', N'SN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Serbia', N'Сербия', N'RS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Seychelles', N'Сейшельские острова', N'SC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Sierra Leone', N'Сьерра-Леоне', N'SL')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Singapore', N'Сингапур', N'SG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Slovak Republic', N'Словакия', N'SK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Slovenia', N'Словения', N'SI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Solomon Islands', N'Соломоновы Острова', N'SB')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Somalia', N'Сомали', N'SO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'South Africa', N'Южно-Африканская Республика', N'ZA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'South Georgia and the South Sandwich Islands', N'Южная Георгия и Южные Сандвичевы острова', N'GS')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'South Ossetia', N'Южная Осетия', N'  ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Spain', N'Испания', N'ES')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Sri Lanka', N'Шри-Ланка', N'LK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'St. Helena', N'Остров Святой Елены', N'SH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'St. Pierre & Miquelon', N'Сен-Пьер и Микелон', N'PM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'St. Tome and Principe', N'Сан-Томе и Принсипи', N'ST')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'St.Kitts Nevis Anguilla', N'Сент-Киттс и Невис', N'KN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'St.Vincent & Grenadines', N'Сент-Винсент и Гренадины', N'VC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Sudan', N'Судан', N'SD')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Suriname', N'Суринам', N'SR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Svalbard & Jan Mayen Is', N'Свальбард', N'SJ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Swaziland', N'Свазиленд', N'SZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Sweden', N'Швеция', N'SE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Switzerland', N'Швейцария', N'CH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Syria', N'Сирия', N'SY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Tadjikistan', N'Таджикистан', N'TJ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Taiwan', N'Тайвань', N'TW')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Tamil Eelam', N'Тамил-Илам', N'  ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Tanzania', N'Танзания', N'TZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Thailand', N'Таиланд', N'TH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Togo', N'Того', N'TG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Tokelau', N'Токелау', N'TK')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Tonga', N'Тонга', N'TO')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Transnistria', N'Приднестровье', N'  ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Trinidad & Tobago', N'Тринидад и Тобаго', N'TT')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Tunisia', N'Тунис', N'TN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Turkey', N'Турция', N'TR')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Turkmenistan', N'Туркменистан', N'TM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Turks and Caicos Islands', N'Тёркс и Кайкос', N'TC')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Tuvalu', N'Тувалу', N'TV')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Uganda', N'Уганда', N'UG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Ukraine', N'Украина', N'UA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'United Arab Emirates', N'Объединенные Арабские Эмираты', N'AE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'United Kingdom (Great Britain)', N'Великобритания', N'GB')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'United States', N'Соединенные Штаты Америки', N'US')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'United States Minor Outlying Islands', N'Внешние малые острова (США)', N'UM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Uruguay', N'Уругвай', N'UY')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Uzbekistan', N'Узбекистан', N'UZ')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Vanuatu', N'Вануату', N'VU')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Vatican City State', N'Ватикан', N'VA')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Venezuela', N'Венесуэла', N'VE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Vietnam', N'Вьетнам', N'VN')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Virgin Islands (British)', N'Виргинские Острова (Британские)', N'VG')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Virgin Islands (US)', N'Виргинские Острова (США)', N'VI')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Wallis & Futuna Islands', N'Острова Уоллис и Футуна', N'WF')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Western Sahara', N'Западная Сахара', N'EH')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Yemen', N'Йемен', N'YE')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Zambia', N'Замбия', N'ZM')
INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code]) VALUES (N'Zimbabwe', N'Зимбабве', N'ZW')

SET IDENTITY_INSERT [dbo].[Application] ON
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (1, CAST(0x073F7B288C421D370B0000 AS DateTimeOffset), N'Invoice 4e85d577-2a7e-4d8d-a3a1-906429e7e844', NULL, NULL, NULL, NULL, NULL, 0, 0, N'0', N'75800114-930b-44ab-b541-06099c8b0f8f', CAST(0x07D4DA298C421D370B0000 AS DateTimeOffset), 1, 1.0000, 2, 0, 1, 6, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 1)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (2, CAST(0x07E877338C421C370B0000 AS DateTimeOffset), N'Invoice 940b08db-668a-4a9f-883a-fa709d822f40', NULL, NULL, NULL, NULL, NULL, 1, 1, N'1', N'484bc0d9-ded0-423d-af02-8e24264a3b72', CAST(0x0755D3368C421C370B0000 AS DateTimeOffset), 2, 2.0000, 2, 1, 2, 7, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 2)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (3, CAST(0x07C9E4378C421B370B0000 AS DateTimeOffset), N'Invoice f6a0701a-5f6d-408b-b69f-b2261c6ec663', NULL, NULL, NULL, NULL, NULL, 2, 2, N'2', N'6a66428c-f17f-4a5a-905a-8fcbbd204578', CAST(0x070B81388C421B370B0000 AS DateTimeOffset), 3, 3.0000, 2, 0, 3, 8, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 3)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (4, CAST(0x076F6B398C421A370B0000 AS DateTimeOffset), N'Invoice 453af235-9f56-41e6-ab90-e6aa60804d41', NULL, NULL, NULL, NULL, NULL, 3, 3, N'3', N'35e5816a-f226-4e87-999f-d651234ffa9a', CAST(0x07A0E0398C421A370B0000 AS DateTimeOffset), 4, 4.0000, 2, 1, 4, 9, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 4)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (5, CAST(0x0704CB3A8C4219370B0000 AS DateTimeOffset), N'Invoice 29e7dd0f-b7f0-4809-8ebe-07a2b2e8029d', NULL, NULL, NULL, NULL, NULL, 4, 4, N'4', N'729e73af-c94c-485f-a7c2-e94f9f3bb307', CAST(0x0746673B8C4219370B0000 AS DateTimeOffset), 13, 5.0000, 2, 0, 5, 10, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 5)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (6, CAST(0x07992A3C8C4218370B0000 AS DateTimeOffset), N'Invoice c8c1df12-088e-42be-969b-f9bdb8e7da89', NULL, NULL, NULL, NULL, NULL, 5, 0, N'5', N'10d70585-19bf-42e6-874a-34b6c5c34262', CAST(0x07AA513C8C421D370B0000 AS DateTimeOffset), 6, 6.0000, 2, 1, 1, 11, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 6)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (7, CAST(0x0770263E8C4217370B0000 AS DateTimeOffset), N'Invoice 0f91fe10-348c-46ac-b804-3a4a403e1056', NULL, NULL, NULL, NULL, NULL, 6, 1, N'6', N'92a6e721-e5fa-49e9-85f9-02bdc6e45107', CAST(0x0770263E8C421C370B0000 AS DateTimeOffset), 7, 7.0000, 2, 0, 2, 12, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 7)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (8, CAST(0x07C3E93E8C4216370B0000 AS DateTimeOffset), N'Invoice 66853302-4b64-42fc-811d-efa3fb73f004', NULL, NULL, NULL, NULL, NULL, 7, 2, N'7', N'50d44102-f61c-4441-861f-24ce51881259', CAST(0x07D4103F8C421B370B0000 AS DateTimeOffset), 8, 8.0000, 2, 1, 3, 13, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 8)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (9, CAST(0x0716AD3F8C4215370B0000 AS DateTimeOffset), N'Invoice 819db2c1-d37b-4632-9083-8f1f5b1795eb', NULL, NULL, NULL, NULL, NULL, 8, 3, N'8', N'29c7c794-1e9f-496e-83ae-165986570ea9', CAST(0x0727D43F8C421A370B0000 AS DateTimeOffset), 9, 9.0000, 2, 0, 4, 14, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 9)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (10, CAST(0x076970408C4214370B0000 AS DateTimeOffset), N'Invoice 2310ce59-20eb-45ce-b24c-d3660340716a', NULL, NULL, NULL, NULL, NULL, 9, 4, N'9', N'd4dbb14a-72e3-4533-8def-6f7b0bf8926f', CAST(0x077A97408C4219370B0000 AS DateTimeOffset), 10, 10.0000, 2, 1, 5, 15, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 10)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (11, CAST(0x07BC33418C421D370B0000 AS DateTimeOffset), N'Invoice a2a9cd90-bf8d-41c7-8726-3013df5f80ab', NULL, NULL, NULL, NULL, NULL, 0, 0, N'10', N'941d1442-8c43-4e68-ae20-1c1f0b7b4170', CAST(0x07CC5A418C421D370B0000 AS DateTimeOffset), 11, 11.0000, 2, 0, 1, 16, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 11)
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (12, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), N'Invoice b8c79b06-bb81-4a53-a1ef-cf8dc732dfe0', NULL, NULL, NULL, NULL, NULL, 1, 1, N'11', N'0ad4f97b-e9d2-484a-a3bf-037176dd0b0e', CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 2, 1, 2, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)
SET IDENTITY_INSERT [dbo].[Application] OFF

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 2, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 2, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)

--INSERT [dbo].[Application] ([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES (
--CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 1, 1, 1, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)