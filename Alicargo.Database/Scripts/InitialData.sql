/****** Object:  Table [dbo].[State]    Script Date: 05/18/2013 14:17:27 ******/
SET IDENTITY_INSERT [dbo].[State] ON
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (1, N'New', 10, 1) -- Новая заявка
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (3, N'Cargo is not ready', 20, 0) -- Груз не готов
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (13, N'Factory does not respond', 30, 0) -- Фабрика не отвечает
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (2, N'Factory awaits payment', 40, 0) -- Фабрика ждет оплату
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (10, N'Factory cargo sent', 50, 1) -- Фабрика отправила груз
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (15, N'Cargo ready to pick up', 55, 0) -- Груз готов для забора
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (4, N'Cargo pick up', 60, 0) -- Груз забран на фабрике
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (6, N'Cargo in stock', 70, 1) -- Груз на складе
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (7, N'Cargo flew', 90, 1) -- Груз вылетел
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (8, N'Cargo at customs', 100, 1) -- Груз на таможне
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (9, N'Customs cleared cargo', 110, 1) -- Груз выпущен
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (12, N'On transit', 120, 1) -- На транзите
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (14, N'Stop', 130, 0) -- Груз на стопе
INSERT [dbo].[State] ([Id], [Name], [Position],[IsSystem]) VALUES (11, N'Cargo received', 140, 1) -- Груз получен
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
/****** Object:  Table [dbo].[StateAvailability]    Script Date: 05/18/2013 14:17:27 ******/
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 7)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (3, 7)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (3, 8)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (3, 9)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 12)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 15)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 15)

INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 2)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 3)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 4)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 6)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (3, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (3, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (3, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 10)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 13)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 15)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 15)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 15)

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
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (1, 5, N'u1@timez.org', N'Nic 0', N'LegalEntity 0', N'Contact 88fb1770-f405-4ca2-ae2f-251a9bc7593b', NULL, N'INN e18cbb8f-13f4-4f58-b5ed-81a45b9e8234', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (2, 6, N'u2@timez.org', N'Nic 1', N'LegalEntity 1', N'Contact 85eb1690-45fe-4b87-a0d1-3dfb2c5c1b43', NULL, N'INN afc62fc8-6088-49d1-9807-3d960e5f6aff', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (3, 7, N'u3@timez.org', N'Nic 2', N'LegalEntity 2', N'Contact 970686ea-499b-4499-90b6-7225df9bf446', NULL, N'INN ce00c59c-abe6-4ffe-b2c7-83585e2460e9', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (4, 8, N'u4@timez.org', N'Nic 3', N'LegalEntity 3', N'Contact ee75723c-76ae-4e26-b94e-26a6312b41fb', NULL, N'INN e8d7859f-cfdf-407d-90a0-35c4190e0e8e', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (5, 9, N'u5@timez.org', N'Nic 4', N'LegalEntity 4', N'Contact be0004f2-df24-415e-a626-96d439f70ee1', NULL, N'INN c0ae51d5-5438-40ac-866e-13abc2a7bc23', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5)
SET IDENTITY_INSERT [dbo].[Client] OFF

INSERT [dbo].[Country] ([Name_En], [Name_Ru], [Code], [Position]) VALUES 
(N'Abkhazia', N'Абхазия', N'AB', 108),
(N'Afghanistan', N'Афганистан', N'AF', 111),
(N'Aland Islands', N'Аландские острова', N'AX', 75),
(N'Albania', N'Албания', N'AL', 47),
(N'Algeria', N'Алжир', N'DZ', 238),
(N'American Samoa', N'Американское Самоа', N'AS', 218),
(N'Andorra', N'Андорра', N'AD', 239),
(N'Angola', N'Ангола', N'AO', 81),
(N'Anguilla', N'Ангилья', N'AI', 15),
(N'Antarctica', N'Антарктика', N'AQ', 119),
(N'Antigua and Barbuda', N'Антигуа и Барбуда', N'AG', 206),
(N'Argentina', N'Аргентина', N'AR', 12),
(N'Armenia', N'Армения', N'AM', 13),
(N'Aruba', N'Аруба', N'AW', 14),
(N'Australia', N'Австралия', N'AU', 8),
(N'Austria', N'Австрия', N'AT', 16),
(N'Azerbaijan', N'Азербайджан', N'AZ', 17),
(N'Azores', N'Азорские острова', N'PT', 18),
(N'Bahamas', N'Багамские Острова', N'BS', 19),
(N'Bahrain', N'Бахрейн', N'BH', 20),
(N'Bangladesh', N'Бангладеш', N'BD', 21),
(N'Barbados', N'Барбадос', N'BB', 22),
(N'Belarus', N'Беларусь', N'BY', 23),
(N'Belgium', N'Бельгия', N'BE', 24),
(N'Belize', N'Белиз', N'BZ', 25),
(N'Benin', N'Бенин', N'BJ', 26),
(N'Bermuda', N'Бермудские Острова', N'BM', 27),
(N'Bolivia', N'Боливия', N'BO', 28),
(N'Bosnia-Herzegovina', N'Босния и Герцеговина', N'BA', 29),
(N'Botswana', N'Ботсвана', N'BW', 30),
(N'Bouvet Island', N'Буве', N'BV', 31),
(N'Brazil', N'Бразилия', N'BR', 32),
(N'British Indian Ocean Territory', N'Британская территория в Индийском океане', N'IO', 33),
(N'Brunei Darussalam', N'Бруней', N'BN', 34),
(N'Bulgaria', N'Болгария', N'BG', 35),
(N'Burkina Faso', N'Буркина-Фасо', N'BF', 36),
(N'Burundi', N'Бурунди', N'BI', 37),
(N'Buthan', N'Бутан', N'BT', 38),
(N'Cambodia', N'Камбоджа', N'KH', 39),
(N'Cameroon', N'Камерун', N'CM', 40),
(N'Canada', N'Канада', N'CA', 41),
(N'Cape Verde', N'Кабо-Верде', N'CV', 42),
(N'Cayman Islands', N'Каймановы Острова', N'KY', 43),
(N'Central African Rep.', N'Центральноафриканская Республика', N'CF', 44),
(N'Chad', N'Чад', N'TD', 45),
(N'Chile', N'Чили', N'CL', 46),
(N'China', N'Китай', N'CN', 4),
(N'Christmas Island', N'Остров Рождества', N'CX', 48),
(N'Cocos (Keeling) Isl.', N'Кокосовые Острова', N'CC', 49),
(N'Colombia', N'Колумбия', N'CO', 50),
(N'Comoros', N'Коморские Острова', N'KM', 51),
(N'Cook Islands', N'Кука острова', N'CK', 52),
(N'Costa Rica', N'Коста-Рика', N'CR', 53),
(N'Croatia', N'Хорватия', N'HR', 54),
(N'Cuba', N'Куба', N'CU', 55),
(N'Cyprus', N'Кипр', N'CY', 56),
(N'Czech Republic', N'Чехия', N'CZ', 57),
(N'Democratic Republic of the Congo', N'Конго, Демократическая Республика', N'CD', 58),
(N'Denmark', N'Дания', N'DK', 59),
(N'Djibouti', N'Джибути', N'DJ', 60),
(N'Dominica', N'Доминика', N'DM', 61),
(N'Dominican Republic', N'Доминиканская Республика', N'DO', 62),
(N'East Timor', N'Восточный Тимор', N'TP', 63),
(N'Ecuador', N'Эквадор', N'EC', 64),
(N'Egypt', N'Египет', N'EG', 65),
(N'El Salvador', N'Сальвадор', N'SV', 66),
(N'Equatorial Guinea', N'Экваториальная Гвинея', N'GQ', 67),
(N'Eritrea', N'Эритрея', N'ER', 68),
(N'Estonia', N'Эстония', N'EE', 69),
(N'Ethiopia', N'Эфиопия', N'ET', 70),
(N'Falkland Islands', N'Фолклендские (Мальвинские) острова', N'FK', 71),
(N'Faroe Islands', N'Фарерские Острова', N'FO', 72),
(N'Fiji', N'Фиджи', N'FJ', 73),
(N'Finland', N'Финляндия', N'FI', 74),
(N'France', N'Франция', N'FR', 3),
(N'French Polynesia', N'Французская Полинезия', N'PF', 76),
(N'French Southern and Antarctic Lands', N'Французские Южные и Антарктические Территории', N'TF', 77),
(N'Gabon', N'Габон', N'GA', 78),
(N'Gambia', N'Гамбия', N'GM', 79),
(N'Georgia', N'Грузия', N'GE', 80),
(N'Germany', N'Германия', N'DE', 7),
(N'Ghana', N'Гана', N'GH', 82),
(N'Gibraltar', N'Гибралтар', N'GI', 83),
(N'Greece', N'Греция', N'GR', 84),
(N'Greenland', N'Гренландия', N'GL', 85),
(N'Grenada', N'Гренада', N'GD', 86),
(N'Guadeloupe (Fr.)', N'Гваделупа', N'GP', 87),
(N'Guam (US)', N'Гуам', N'GU', 88),
(N'Guatemala', N'Гватемала', N'GT', 89),
(N'Guernsey', N'Гернси', N'GG', 90),
(N'Guinea', N'Гвинея', N'GN', 91),
(N'Guinea Bissau', N'Гвинея-Бисау', N'GW', 92),
(N'Guyana', N'Гвиана', N'GY', 93),
(N'Guyana (Fr.)', N'Гайана', N'GF', 94),
(N'Haiti', N'Гаити', N'HT', 95),
(N'Heard Island and McDonald Islands', N'Острова Херд и Макдональд', N'HM', 96),
(N'Honduras', N'Гондурас', N'HN', 97),
(N'Hong Kong', N'Гонконг', N'HK', 98),
(N'Hungary', N'Венгрия', N'HU', 99),
(N'Iceland', N'Исландия', N'IS', 100),
(N'India', N'Индия', N'IN', 101),
(N'Indonesia', N'Индонезия', N'ID', 102),
(N'Iran', N'Иран', N'IR', 103),
(N'Iraq', N'Ирак', N'IQ', 104),
(N'Ireland', N'Ирландия', N'IE', 105),
(N'Isle of Man', N'Остров Мэн', N'IM', 106),
(N'Israel', N'Израиль', N'IL', 107),
(N'Italy', N'Италия', N'IT', 1),
(N'Ivory Coast', N'Кот-д''Ивуар', N'CI', 109),
(N'Jamaica', N'Ямайка', N'JM', 110),
(N'Japan', N'Япония', N'JP', 2),
(N'Jersey', N'Джерси', N'JE', 112),
(N'Jordan', N'Иордания', N'JO', 113),
(N'Kazachstan', N'Казахстан', N'KZ', 114),
(N'Kenya', N'Кения', N'KE', 115),
(N'Kirgistan', N'Кыргызстан', N'KG', 116),
(N'Kiribati', N'Кирибати', N'KI', 117),
(N'Korea (North)', N'Корея (Северная)', N'KP', 118),
(N'Korea (South)', N'Корея (Южная)', N'KR', 10),
(N'Kosovo', N'Косово', N'KO', 120),
(N'Kuwait', N'Кувейт', N'KW', 121),
(N'Laos', N'Лаос', N'LA', 122),
(N'Latvia', N'Латвия', N'LV', 123),
(N'Lebanon', N'Ливан', N'LB', 124),
(N'Lesotho', N'Лесото', N'LS', 125),
(N'Liberia', N'Либерия', N'LR', 126),
(N'Libya', N'Ливия', N'LY', 127),
(N'Liechtenstein', N'Лихтенштейн', N'LI', 128),
(N'Lithuania', N'Литва', N'LT', 129),
(N'Luxembourg', N'Люксембург', N'LU', 130),
(N'Macau', N'Аомынь', N'MO', 131),
(N'Madagascar', N'Мадагаскар', N'MG', 132),
(N'Malawi', N'Малави', N'MW', 133),
(N'Malaysia', N'Малайзия', N'MY', 134),
(N'Maldives', N'Мальдивы', N'MV', 135),
(N'Mali', N'Мали', N'ML', 136),
(N'Malta', N'Мальта', N'MT', 137),
(N'Marshall Islands', N'Маршалловы Острова', N'MH', 138),
(N'Martinique (Fr.)', N'Мартиника', N'MQ', 139),
(N'Mauritania', N'Мавритания', N'MR', 140),
(N'Mauritius', N'Маврикий', N'MU', 141),
(N'Mayotte', N'Майотта', N'YT', 142),
(N'Mexico', N'Мексика', N'MX', 143),
(N'Micronesia', N'Микронезия', N'FM', 144),
(N'Moldavia', N'Молдова', N'MD', 145),
(N'Monaco', N'Монако', N'MC', 146),
(N'Mongolia', N'Монголия', N'MN', 147),
(N'Montenegro', N'Черногория', N'ME', 148),
(N'Montserrat', N'Монтсеррат', N'MS', 149),
(N'Morocco', N'Морокко', N'MA', 150),
(N'Mozambique', N'Мозамбик', N'MZ', 151),
(N'Myanmar', N'Мьянма', N'MM', 152),
(N'Nagorno-Karabakh Republic', N'Нагорно-Карабахская Республика', N'NK', 153),
(N'Namibia', N'Намибия', N'NA', 154),
(N'Nauru', N'Науру', N'NR', 155),
(N'Nepal', N'Непал', N'NP', 156),
(N'Netherland Antilles', N'Антильские Острова', N'AN', 157),
(N'Netherlands', N'Нидерланды', N'NL', 158),
(N'New Caledonia (Fr.)', N'Новая Каледония', N'NC', 159),
(N'New Zealand', N'Новая Зеландия', N'NZ', 160),
(N'Nicaragua', N'Никарагуа', N'NI', 161),
(N'Niger', N'Нигер', N'NE', 162),
(N'Nigeria', N'Нигерия', N'NG', 163),
(N'Niue', N'Ниуэ', N'NU', 164),
(N'Norfolk Island', N'Норфолк', N'NF', 165),
(N'Northern Cyprus', N'Турецкая Республика Северного Кипра', N'NC', 166),
(N'Northern Mariana Isl.', N'Северные Марианские острова', N'MP', 167),
(N'Norway', N'Норвегия', N'NO', 168),
(N'Oman', N'Оман', N'OM', 169),
(N'Pakistan', N'Пакистан', N'PK', 170),
(N'Palau', N'Палау', N'PW', 171),
(N'Palestine', N'Палестина', N'PS', 172),
(N'Panama', N'Панама', N'PA', 173),
(N'Papua New', N'Папуа — Новая Гвинея', N'PG', 174),
(N'Paraguay', N'Парагвай', N'PY', 175),
(N'Peru', N'Перу', N'PE', 176),
(N'Philippines', N'Филиппины', N'PH', 177),
(N'Pitcairn', N'Питкэрн', N'PN', 178),
(N'Poland', N'Польша', N'PL', 179),
(N'Portugal', N'Португалия', N'PT', 180),
(N'Puerto Rico', N'Пуэрто-Рико', N'PR', 181),
(N'Qatar', N'Катар', N'QA', 182),
(N'Republic of Macedonia', N'Македония', N'MK', 183),
(N'Republic of Somaliland', N'Сомалиленд', N'S', 184),
(N'Republic of the Congo', N'Республика Конго', N'CG', 185),
(N'Reunion (Fr.)', N'Реюньон', N'RE', 186),
(N'Romania', N'Румыния', N'RO', 187),
(N'Russia', N'Россия', N'RU', 188),
(N'Rwanda', N'Руанда', N'RW', 189),
(N'Saint Lucia', N'Сент-Люсия', N'LC', 190),
(N'Samoa', N'Самоа', N'WS', 191),
(N'San Marino', N'Сан-Марино', N'SM', 192),
(N'Saudi Arabia', N'Саудовская Аравия', N'SA', 193),
(N'Senegal', N'Сенегал', N'SN', 194),
(N'Serbia', N'Сербия', N'RS', 195),
(N'Seychelles', N'Сейшельские острова', N'SC', 196),
(N'Sierra Leone', N'Сьерра-Леоне', N'SL', 197),
(N'Singapore', N'Сингапур', N'SG', 198),
(N'Slovak Republic', N'Словакия', N'SK', 199),
(N'Slovenia', N'Словения', N'SI', 200),
(N'Solomon Islands', N'Соломоновы Острова', N'SB', 201),
(N'Somalia', N'Сомали', N'SO', 202),
(N'South Africa', N'Южно-Африканская Республика', N'ZA', 203),
(N'South Georgia and the South Sandwich Islands', N'Южная Георгия и Южные Сандвичевы острова', N'GS', 204),
(N'South Ossetia', N'Южная Осетия', N'OS', 205),
(N'Spain', N'Испания', N'ES', 11),
(N'Sri Lanka', N'Шри-Ланка', N'LK', 207),
(N'St. Helena', N'Остров Святой Елены', N'SH', 208),
(N'St. Pierre & Miquelon', N'Сен-Пьер и Микелон', N'PM', 209),
(N'St. Tome and Principe', N'Сан-Томе и Принсипи', N'ST', 210),
(N'St.Kitts Nevis Anguilla', N'Сент-Киттс и Невис', N'KN', 211),
(N'St.Vincent & Grenadines', N'Сент-Винсент и Гренадины', N'VC', 212),
(N'Sudan', N'Судан', N'SD', 213),
(N'Suriname', N'Суринам', N'SR', 214),
(N'Svalbard & Jan Mayen Is', N'Свальбард', N'SJ', 215),
(N'Swaziland', N'Свазиленд', N'SZ', 216),
(N'Sweden', N'Швеция', N'SE', 217),
(N'Switzerland', N'Швейцария', N'CH', 6),
(N'Syria', N'Сирия', N'SY', 219),
(N'Tadjikistan', N'Таджикистан', N'TJ', 220),
(N'Taiwan', N'Тайвань', N'TW', 221),
(N'Tamil Eelam', N'Тамил-Илам', N'TE', 222),
(N'Tanzania', N'Танзания', N'TZ', 223),
(N'Thailand', N'Таиланд', N'TH', 224),
(N'Togo', N'Того', N'TG', 225),
(N'Tokelau', N'Токелау', N'TK', 226),
(N'Tonga', N'Тонга', N'TO', 227),
(N'Transnistria', N'Приднестровье', N'TS', 228),
(N'Trinidad & Tobago', N'Тринидад и Тобаго', N'TT', 229),
(N'Tunisia', N'Тунис', N'TN', 230),
(N'Turkey', N'Турция', N'TR', 231),
(N'Turkmenistan', N'Туркменистан', N'TM', 232),
(N'Turks and Caicos Islands', N'Тёркс и Кайкос', N'TC', 233),
(N'Tuvalu', N'Тувалу', N'TV', 234),
(N'Uganda', N'Уганда', N'UG', 235),
(N'Ukraine', N'Украина', N'UA', 236),
(N'United Arab Emirates', N'Объединенные Арабские Эмираты', N'AE', 237),
(N'United Kingdom (Great Britain)', N'Великобритания', N'GB', 5),
(N'United States', N'Соединенные Штаты Америки', N'US', 239),
(N'United States Minor Outlying Islands', N'Внешние малые острова (США)', N'UM', 240),
(N'Uruguay', N'Уругвай', N'UY', 241),
(N'Uzbekistan', N'Узбекистан', N'UZ', 242),
(N'Vanuatu', N'Вануату', N'VU', 243),
(N'Vatican City State', N'Ватикан', N'VA', 244),
(N'Venezuela', N'Венесуэла', N'VE', 245),
(N'Vietnam', N'Вьетнам', N'VN', 246),
(N'Virgin Islands (British)', N'Виргинские Острова (Британские)', N'VG', 247),
(N'Virgin Islands (US)', N'Виргинские Острова (США)', N'VI', 248),
(N'Wallis & Futuna Islands', N'Острова Уоллис и Футуна', N'WF', 249),
(N'Western Sahara', N'Западная Сахара', N'EH', 250),
(N'Yemen', N'Йемен', N'YE', 251),
(N'Zambia', N'Замбия', N'ZM', 252),
(N'Zimbabwe', N'Зимбабве', N'ZW', 253)
GO

INSERT [dbo].[Application] 
([CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES 
(CAST(0x073F7B288C421D370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NEWID(), CAST(0x07D4DA298C421D370B0000 AS DateTimeOffset), 1, 1.0000, 2, 0, 1, 6, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 1),
(CAST(0x07E877338C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 1, 1, 1, NEWID(), CAST(0x0755D3368C421C370B0000 AS DateTimeOffset), 2, 2.0000, 2, 1, 2, 7, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 2),
(CAST(0x07C9E4378C421B370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 2, 2, 2, NEWID(), CAST(0x070B81388C421B370B0000 AS DateTimeOffset), 3, 3.0000, 2, 0, 3, 8, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 3),
(CAST(0x076F6B398C421A370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 3, 3, 3, NEWID(), CAST(0x07A0E0398C421A370B0000 AS DateTimeOffset), 4, 4.0000, 2, 1, 4, 9, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 4),
(CAST(0x0704CB3A8C4219370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 4, 4, 4, NEWID(), CAST(0x0746673B8C4219370B0000 AS DateTimeOffset), 13, 5.0000, 2, 0, 5, 10, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 5),
(CAST(0x07992A3C8C4218370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 5, 0, 5, NEWID(), CAST(0x07AA513C8C421D370B0000 AS DateTimeOffset), 6, 6.0000, 2, 1, 1, 11, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 6),
(CAST(0x0770263E8C4217370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 6, 1, 6, NEWID(), CAST(0x0770263E8C421C370B0000 AS DateTimeOffset), 7, 7.0000, 2, 0, 2, 12, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 7),
(CAST(0x07C3E93E8C4216370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 7, 2, 7, NEWID(), CAST(0x07D4103F8C421B370B0000 AS DateTimeOffset), 8, 8.0000, 2, 1, 3, 13, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 8),
(CAST(0x0716AD3F8C4215370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 8, 3, 8, NEWID(), CAST(0x0727D43F8C421A370B0000 AS DateTimeOffset), 9, 9.0000, 2, 0, 4, 14, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 9),
(CAST(0x076970408C4214370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 9, 4, 9, NEWID(), CAST(0x077A97408C4219370B0000 AS DateTimeOffset), 10, 10.0000, 2, 1, 5, 15, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 10),
(CAST(0x07BC33418C421D370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 0, 0, 10, NEWID(), CAST(0x07CC5A418C421D370B0000 AS DateTimeOffset), 11, 11.0000, 2, 0, 1, 16, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 11),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 1, 1, 11, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 2, 1, 2, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 2, 17, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 2, 17, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 4, 17, 2, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 5, 17, 2, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 5, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 5, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 12)
GO

UPDATE [dbo].[Application]
SET [SenderId] = 1
WHERE [SenderId] IS NULL