/****** Object:  Table [dbo].[AirWaybill]    Script Date: 05/18/2013 14:17:27 ******/
INSERT [dbo].[AirWaybill] ([CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [StateId]) VALUES (CAST(GETDATE() - 3 AS DateTimeOffset), NEWID(), NEWID(), NEWID(), CAST(GETDATE() AS DateTimeOffset), CAST(GETDATE() AS DateTimeOffset), 1, NEWID(), 7)
INSERT [dbo].[AirWaybill] ([CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [StateId]) VALUES (CAST(GETDATE() - 2 AS DateTimeOffset), NEWID(), N'ArrivalAirport', N'DepartureAirport', CAST(GETDATE() AS DateTimeOffset), CAST(GETDATE() AS DateTimeOffset), 1, NEWID(), 7)
INSERT [dbo].[AirWaybill] ([CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrokerId], [GTD], [StateId]) VALUES (CAST(GETDATE() - 1 AS DateTimeOffset), NEWID(), N'ArrivalAirport', N'DepartureAirport', CAST(GETDATE() AS DateTimeOffset), CAST(GETDATE() AS DateTimeOffset), 1, NEWID(), 7)

INSERT [dbo].[Application] 
([SenderId], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume], [TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName], [FactoryPhone], [FactoryEmail], [MarkName], [CountryId]) VALUES 
(1, CAST(0x073F7B288C421D370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NEWID(), CAST(0x07D4DA298C421D370B0000 AS DateTimeOffset), 1, 1.0000, 2, 0, 1, 6, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 1),
(1, CAST(0x07E877338C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 1, 1, 1, NEWID(), CAST(0x0755D3368C421C370B0000 AS DateTimeOffset), 2, 2.0000, 2, 1, 2, 7, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 2),
(1, CAST(0x07C9E4378C421B370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 2, 2, 2, NEWID(), CAST(0x070B81388C421B370B0000 AS DateTimeOffset), 3, 3.0000, 2, 0, 3, 8, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 3),
(1, CAST(0x076F6B398C421A370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 3, 3, 3, NEWID(), CAST(0x07A0E0398C421A370B0000 AS DateTimeOffset), 4, 4.0000, 2, 1, 4, 9, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 4),
(1, CAST(0x0704CB3A8C4219370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 4, 4, 4, NEWID(), CAST(0x0746673B8C4219370B0000 AS DateTimeOffset), 13, 5.0000, 2, 0, 5, 10, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 5),
(1, CAST(0x07992A3C8C4218370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 5, 0, 5, NEWID(), CAST(0x07AA513C8C421D370B0000 AS DateTimeOffset), 6, 6.0000, 2, 1, 1, 11, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 6),
(1, CAST(0x0770263E8C4217370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 6, 1, 6, NEWID(), CAST(0x0770263E8C421C370B0000 AS DateTimeOffset), 7, 7.0000, 2, 0, 2, 12, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 7),
(1, CAST(0x07C3E93E8C4216370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 7, 2, 7, NEWID(), CAST(0x07D4103F8C421B370B0000 AS DateTimeOffset), 8, 8.0000, 2, 1, 3, 13, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 8),
(1, CAST(0x0716AD3F8C4215370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 8, 3, 8, NEWID(), CAST(0x0727D43F8C421A370B0000 AS DateTimeOffset), 9, 9.0000, 2, 0, 4, 14, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 9),
(1, CAST(0x076970408C4214370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 9, 4, 9, NEWID(), CAST(0x077A97408C4219370B0000 AS DateTimeOffset), 10, 10.0000, 2, 1, 5, 15, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 10),
(1, CAST(0x07BC33418C421D370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 0, 0, 10, NEWID(), CAST(0x07CC5A418C421D370B0000 AS DateTimeOffset), 11, 11.0000, 2, 0, 1, 16, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 11),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NULL, NULL, NULL, 1, 1, 11, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 2, 1, 2, 17, NULL, 'f1', 'f1', 'f1@mail.ru', 'm1', 12),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 2, 17, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 13),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 2, 17, 1, 'f1', 'f1', 'f1@mail.ru', 'm1', 14),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 4, 17, 2, 'f1', 'f1', 'f1@mail.ru', 'm1', 15),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 5, 17, 2, 'f1', 'f1', 'f1@mail.ru', 'm1', 16),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 5, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 17),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 5, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 18),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 19),
(1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL, NULL, NEWID(), NEWID(), NEWID(), 1, 1, 1, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1, 1, 1, 17, 3, 'f1', 'f1', 'f1@mail.ru', 'm1', 10)
GO

UPDATE [dbo].[Application]
SET [SenderId] = 1
WHERE [SenderId] IS NULL