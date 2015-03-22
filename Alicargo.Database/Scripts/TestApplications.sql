INSERT [dbo].[AirWaybill] ([CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport],
[DateOfDeparture], [DateOfArrival],[BrokerId], [GTD], [StateId], [CreatorUserId], [SenderUserId]) VALUES 
(SYSDATETIMEOFFSET(), CAST(NEWID() AS NVARCHAR(50)),CAST(NEWID() AS NVARCHAR(50)), CAST(NEWID() AS NVARCHAR(50)),
SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 1, CAST(NEWID() AS NVARCHAR(50)), 7, 1, NULL),
(SYSDATETIMEOFFSET(), CAST(NEWID() AS NVARCHAR(50)), N'ArrivalAirport', N'DepartureAirport',
SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 1, CAST(NEWID() AS NVARCHAR(50)), 7, 1, 4),
(SYSDATETIMEOFFSET(), CAST(NEWID() AS NVARCHAR(50)), N'ArrivalAirport', N'DepartureAirport',
SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 1, CAST(NEWID() AS NVARCHAR(50)), 7, 1, 4)

SET IDENTITY_INSERT [dbo].[Transit] ON
INSERT [dbo].[Transit] ([Id], [CityId], [Address], [RecipientName], [Phone], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES
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
(25, 9, N'Address 19', N'RecipientName 19', N'Phone 19', 1, 1, 2)
SET IDENTITY_INSERT [dbo].[Transit] OFF

INSERT [dbo].[Application] 
([SenderId], [ForwarderId], [CreationTimestamp], [Invoice], [Characteristic]
,[AddressLoad], [WarehouseWorkingTime], [Weight], [Count], [Volume]
,[TermsOfDelivery], [StateChangeTimestamp], [StateId], [Value], [CurrencyId]
,[MethodOfDeliveryId], [ClientId], [TransitId], [AirWaybillId], [FactoryName]
,[FactoryPhone], [FactoryEmail], [MarkName], [CountryId], [DisplayNumber]
,[IsPickup]) VALUES

(1, 1, CAST(0x073F7B288C421D370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 0, 0, 0
, NEWID(), CAST(0x07D4DA298C421D370B0000 AS DateTimeOffset), 1, 1.0000, 2
, 0, 1, 6, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 1, 1
, 1),

(1, 1, CAST(0x07E877338C421C370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 1, 1, 1
, NEWID(), CAST(0x0755D3368C421C370B0000 AS DateTimeOffset), 2, 2.0000, 2
, 1, 2, 7, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 2, 2
, 1),

(1, 1, CAST(0x07C9E4378C421B370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 2, 2, 2
, NEWID(), CAST(0x070B81388C421B370B0000 AS DateTimeOffset), 3, 3.0000, 2
, 0, 3, 8, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 3, 3
, 0),

(1, 1, CAST(0x076F6B398C421A370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 3, 3, 3
, NEWID(), CAST(0x07A0E0398C421A370B0000 AS DateTimeOffset), 4, 4.0000, 2
, 1, 4, 9, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 4, 4
, 0),

(1, 1, CAST(0x0704CB3A8C4219370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 4, 4, 4
, NEWID(), CAST(0x0746673B8C4219370B0000 AS DateTimeOffset), 13, 5.0000, 2
, 0, 5, 10, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 5, 5
, 0),

(1, 2, CAST(0x07992A3C8C4218370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 5, 0, 5
, NEWID(), CAST(0x07AA513C8C421D370B0000 AS DateTimeOffset), 6, 6.0000, 2
, 1, 1, 11, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 6, 6
, 0),

(1, 2, CAST(0x0770263E8C4217370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 6, 1, 6
, NEWID(), CAST(0x0770263E8C421C370B0000 AS DateTimeOffset), 7, 7.0000, 2
, 0, 2, 12, 1, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 7, 7
, 0),

(1, 2, CAST(0x07C3E93E8C4216370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 7, 2, 7
, NEWID(), CAST(0x07D4103F8C421B370B0000 AS DateTimeOffset), 8, 8.0000, 2
, 1, 3, 13, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 8, 8
, 0),

(1, 2, CAST(0x0716AD3F8C4215370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 8, 3, 8
, NEWID(), CAST(0x0727D43F8C421A370B0000 AS DateTimeOffset), 9, 9.0000, 2
, 0, 4, 14, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 9, 9
, 0),

(2, 2, CAST(0x076970408C4214370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 9, 4, 9
, NEWID(), CAST(0x077A97408C4219370B0000 AS DateTimeOffset), 10, 10.0000, 2
, 1, 5, 15, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 10, 10
, 0),

(2, 2, CAST(0x07BC33418C421D370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 0, 0, 10
, NEWID(), CAST(0x07CC5A418C421D370B0000 AS DateTimeOffset), 11, 11.0000, 2
, 0, 1, 16, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 11, 11
, 0),

(2, 2, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NULL
, NULL, NULL, 1, 1, 11
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 2
, 1, 2, 17, NULL, 'f1'
, 'f1', 'f1@mail.ru', 'm1', 12, 12
, 0),

(2, 2, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 2, 18, 1, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 13, 13
, 0),

(2, 2, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 2, 19, 1, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 14, 14
, 0),

(2, 1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 4, 20, 2, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 15, 15
, 0),

(2, 1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 5, 21, 2, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 16, 16
, 0),

(2, 1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 5, 22, 1, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 17, 17
, 0),

(2, 1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 5, 23, 2, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 18, 18
, 0),

(2, 1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 1, 24, 2, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 19, 19
, 0),

(2, 1, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), NEWID(), NEWID()
, NEWID(), NEWID(), 1, 1, 1
, NEWID(), CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 7, 12.0000, 1
, 1, 1, 25, 3, 'f2'
, 'f1', 'f1@mail.ru', 'm2', 10, 20
, 0)
GO

UPDATE [dbo].[Application]
SET [SenderId] = 1
WHERE [SenderId] IS NULL