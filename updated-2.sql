USE [$(MainDbName)]
GO

DELETE [dbo].[EmailTemplate]
WHERE [Id] >= 11 AND [Id] <= 23

SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
INSERT [dbo].[EmailTemplate] ([Id]) VALUES
(30), (31), (32)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF

INSERT [dbo].[EventEmailTemplate]
([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES
(18, 30, 1),
(19, 31, 1),
(20, 32, 1)

INSERT [dbo].[EventEmailRecipient]
([RoleId], [EventTypeId]) VALUES
(1, 18),
(1, 19),
(1, 20),
(5, 18),
(5, 19),
(3, 19),
(4, 20),
(5, 20)

INSERT [dbo].[EmailTemplateLocalization]
([EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES
(30, N'ru', N'Alicargo',
N'Добрый день, {ClientNic}! Вы зарегистрированны в системе отслеживания грузов компании Alicargo. Имя доступа: {Login}, Пароль: {Password}.', 0),
(31, N'ru', N'Alicargo',
N'Создана авианакладная, Аэропорт вылета: {DepartureAirport}/{DateOfDeparture}, Аэропорт прилета: {ArrivalAirport}/{DateOfArrival}, Вес: {TotalWeight}, Количество мест: {TotalCount}. Номер накладной: {AirWaybill}.', 0),
(32, N'ru', N'Alicargo',
N'Заявке {DisplayNumber} задана авианакладная, Аэропорт вылета: {DepartureAirport}/{DateOfDeparture}, Аэропорт прилета: {ArrivalAirport}/{DateOfArrival}, Вес: {TotalWeight}, Количество мест: {TotalCount}. Номер накладной: {AirWaybill}.', 0)