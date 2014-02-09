USE [$(MainDbName)]
GO

DELETE [dbo].[EmailTemplate]
WHERE [Id] >= 11 AND [Id] <= 23

SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
INSERT [dbo].[EmailTemplate] ([Id]) VALUES
(30), (31), (32), (33), (34), (35), (36), (37), (38)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF

INSERT [dbo].[EventEmailTemplate]
([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES
(18, 30, 1),
(19, 31, 1),
(20, 32, 1),
(21, 33, 1), -- GTDFileUploaded
(22, 34, 1), -- GTDAdditionalFileUploaded
(23, 35, 1), -- AwbPackingFileUploaded
(24, 36, 1), -- AwbInvoiceFileUploaded
(25, 37, 1), -- AWBFileUploaded
(26, 38, 1)  -- DrawFileUploaded

INSERT [dbo].[EventEmailRecipient]
([RoleId], [EventTypeId]) VALUES
(1, 18),
(1, 19),
(1, 20),
(1, 21),
(1, 22),
(1, 24),
(1, 25),
(1, 26),
(2, 24),
(2, 25),
(3, 19),
(3, 23),
(3, 25),
(3, 26),
(4, 20),
(5, 18),
(5, 19),
(5, 20),
(5, 21),
(5, 22)


INSERT [dbo].[EmailTemplateLocalization]
([EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES
(30, N'ru', N'Alicargo',
N'Добрый день, {ClientNic}! Вы зарегистрированны в системе отслеживания грузов компании Alicargo. Имя доступа: {Login}, Пароль: {Password}.', 0),
(31, N'ru', N'Alicargo',
N'Создана авианакладная, Аэропорт вылета: {DepartureAirport}/{DateOfDeparture}, Аэропорт прилета: {ArrivalAirport}/{DateOfArrival}, Вес: {TotalWeight}, Количество мест: {TotalCount}. Номер накладной: {AirWaybill}.', 0),
(32, N'ru', N'Alicargo',
N'Заявке {DisplayNumber} задана авианакладная, Аэропорт вылета: {DepartureAirport}/{DateOfDeparture}, Аэропорт прилета: {ArrivalAirport}/{DateOfArrival}, Вес: {TotalWeight}, Количество мест: {TotalCount}. Номер накладной: {AirWaybill}.', 0),

(33, N'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен ГТД»', 0),
(33, N'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added GTD"', 0),

(34, N'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен ГТД-дополнение»', 0),
(34, N'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added the GTD-complement"', 0),

(35, N'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен пакинг-лист»', 0),
(35, N'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added paking-list"', 0),

(36, N'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен инвойс»', 0),
(36, N'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added invoice"', 0),

(37, N'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, добавлена авиа-накладная', 0),
(37, N'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, added air waybill', 0),

(38, N'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, добавлен Draw', 0),
(38, N'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, added Draw', 0)