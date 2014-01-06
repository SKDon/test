SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
INSERT [dbo].[EmailTemplate] ([Id]) VALUES
(1), (2), (3), (4), (5), (6), (7), (8), (9), (10),
(11), (12), (13), (14), (15), (16), (17), (18), (19), (20),
(21), (22), (23), (24), (25), (26), (27)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF


INSERT [dbo].[EventEmailTemplate]
([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES
(1, 1, 1),
(3, 2, 1),
(7, 3, 1),
(4, 4, 1),
(5, 5, 1),
(9, 6, 1),
(10, 7, 1),
(6, 8, 1),
(8, 9, 1),
(2, 10, 1),
(11, 24, 1), -- Calculate
(12, 25, 0), -- CalculationCanceled
(13, 26, 1), -- BalanceIncreased
(14, 27, 1) -- BalanceDecreased


INSERT [dbo].[EmailTemplateLocalization]
([EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES
(1, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Создана новая заявка.
Номер заявки: {DisplayNumber}
Клиент: {ClientNic}
Производитель: {FactoryName}
Марка: {MarkName}
Дата создания: {CreationTimestamp}', 0),

(1, N'en', N'Alicargo : Order#{DisplayNumber}', N'Created new order.
Number order: {DisplayNumber}
Client: {ClientNic}
Factory: {FactoryName}
Brands: {MarkName}
Created: {CreationTimestamp}', 0),

(1, N'it', N'Alicargo : Order#{DisplayNumber}', N'Created new order.
Number order: {DisplayNumber}
Client: {ClientNic}
Factory: {FactoryName}
Brands: {MarkName}
Created: {CreationTimestamp}', 0),

(2, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ Счет-фактура.
Просьба подписать и выслать почтой по адресу:
Россия - ООО "Трэйд Инвест"
107113 Москва, Сокольническая пл. 4а офис 309  

С уважением,
Alicargo  srl', 0),

(2, N'en', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),
 
(2, N'it', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),

(3, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен счет за доставку.', 0),
(3, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0),
(3, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0),

(4, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен инвойс {UploadedFile}.', 0),
(4, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {UploadedFile}.', 0),
(4, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {UploadedFile}.', 0),

(5, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен пакинг {UploadedFile}.', 0),
(5, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing {UploadedFile}.', 0),
(5, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing {UploadedFile}.', 0),

(6, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0),
(6, N'en', N'Alicargo : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0),
(6, N'it', N'Alicargo : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0),

(7, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Задан транзитный референс {TransitReference}.', 0),
(7, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0),
(7, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0),

(8, N'ru', N'Alicargo : Order#{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен swift {UploadedFile}.', 0),
(8, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift {UploadedFile}.', 0),
(8, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift {UploadedFile}.', 0),

(9, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ ТОРГ-12.
Просьба подписать и выслать почтой по адресу:
Россия - ООО "Трэйд Инвест"
107113 Москва, Сокольническая пл. 4а офис 309

С уважением,
Alicargo  srl', 0),

(9, N'en', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),

(9, N'it', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),

(10, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {StateName}
Номер заказа: {DisplayNumber}
{DateOfCargoReceipt [Предположительная дата получения: {0}]}
{TransitCarrierName [Перевозчик: {0}]}
{FactoryName [Производитель: {0}]}
{FactoryEmail [Email фабрики: {0}]}
{FactoryPhone [Телефон фабрики: {0}]}
{FactoryContact [Контактное лицо фабрики: {0}]}
{DaysInWork [Дней в работе: {0}]}
{Invoice [Инвойс: {0}]}
{CreationTimestamp [Дата создания: {0}]}
{StateChangeTimestamp [Дата сметы статуса: {0}]}
{MarkName [Марка: {0}]}
{Count [Количество мест (коробки): {0}]}
{Volume [Объем (m3 ): {0}]}
{Weight [Вес (кг): {0}]}
{Characteristic [Характеристика товара: {0}]}
{Value [Стоимость: {0}]}
{AddressLoad [Адрес забора груза: {0}]}
{CountryName [Страна: {0}]}
{WarehouseWorkingTime [Время работы склада: {0}]}
{TermsOfDelivery [Условия поставки: {0}]}
{MethodOfDelivery [Способ доставки: {0}]}
{TransitReference [Транзитный референс: {0}]}

Транзит по России:
{TransitCity [Город: {0}]}
{TransitAddress [Адрес: {0}]}
{TransitRecipientName [Получатель: {0}]}
{TransitPhone [Телефон: {0}]}
{TransitWarehouseWorkingTime [Время работы склада: {0}]}
{MethodOfTransit [Способ транзита: {0}]}
{DeliveryType [Тип доставки: {0}]}

{AirWaybill [AWB: {0}]}
{AirWaybillDateOfDeparture [Дата вылета: {0}]}
{AirWaybillDateOfArrival [Дата прилета: {0}]}
{AirWaybillGTD [Номер ГТД: {0}]}', 0),

(10, N'en', N'Alicargo : Order#{DisplayNumber}', N'Изменение статуса заявки: {StateName}
Номер заказа: {DisplayNumber}
{DateOfCargoReceipt [Предположительная дата получения: {0}]}
{TransitCarrierName [Перевозчик: {0}]}
{FactoryName [Производитель: {0}]}
{FactoryEmail [Email фабрики: {0}]}
{FactoryPhone [Телефон фабрики: {0}]}
{FactoryContact [Контактное лицо фабрики: {0}]}
{DaysInWork [Дней в работе: {0}]}
{Invoice [Инвойс: {0}]}
{CreationTimestamp [Дата создания: {0}]}
{StateChangeTimestamp [Дата сметы статуса: {0}]}
{MarkName [Марка: {0}]}
{Count [Количество мест (коробки): {0}]}
{Volume [Объем (m3 ): {0}]}
{Weight [Вес (кг): {0}]}
{Characteristic [Характеристика товара: {0}]}
{Value [Стоимость: {0}]}
{AddressLoad [Адрес забора груза: {0}]}
{CountryName [Страна: {0}]}
{WarehouseWorkingTime [Время работы склада: {0}]}
{TermsOfDelivery [Условия поставки: {0}]}
{MethodOfDelivery [Способ доставки: {0}]}
{TransitReference [Транзитный референс: {0}]}

Транзит по России:
{TransitCity [Город: {0}]}
{TransitAddress [Адрес: {0}]}
{TransitRecipientName [Получатель: {0}]}
{TransitPhone [Телефон: {0}]}
{TransitWarehouseWorkingTime [Время работы склада: {0}]}
{MethodOfTransit [Способ транзита: {0}]}
{DeliveryType [Тип доставки: {0}]}

{AirWaybill [AWB: {0}]}
{AirWaybillDateOfDeparture [Дата вылета: {0}]}
{AirWaybillDateOfArrival [Дата прилета: {0}]}
{AirWaybillGTD [Номер ГТД: {0}]}', 0),

(10, N'it', N'Alicargo : Order#{DisplayNumber}', N'Изменение статуса заявки: {StateName}
Номер заказа: {DisplayNumber}
{DateOfCargoReceipt [Предположительная дата получения: {0}]}
{TransitCarrierName [Перевозчик: {0}]}
{FactoryName [Производитель: {0}]}
{FactoryEmail [Email фабрики: {0}]}
{FactoryPhone [Телефон фабрики: {0}]}
{FactoryContact [Контактное лицо фабрики: {0}]}
{DaysInWork [Дней в работе: {0}]}
{Invoice [Инвойс: {0}]}
{CreationTimestamp [Дата создания: {0}]}
{StateChangeTimestamp [Дата сметы статуса: {0}]}
{MarkName [Марка: {0}]}
{Count [Количество мест (коробки): {0}]}
{Volume [Объем (m3 ): {0}]}
{Weight [Вес (кг): {0}]}
{Characteristic [Характеристика товара: {0}]}
{Value [Стоимость: {0}]}
{AddressLoad [Адрес забора груза: {0}]}
{CountryName [Страна: {0}]}
{WarehouseWorkingTime [Время работы склада: {0}]}
{TermsOfDelivery [Условия поставки: {0}]}
{MethodOfDelivery [Способ доставки: {0}]}
{TransitReference [Транзитный референс: {0}]}

Транзит по России:
{TransitCity [Город: {0}]}
{TransitAddress [Адрес: {0}]}
{TransitRecipientName [Получатель: {0}]}
{TransitPhone [Телефон: {0}]}
{TransitWarehouseWorkingTime [Время работы склада: {0}]}
{MethodOfTransit [Способ транзита: {0}]}
{DeliveryType [Тип доставки: {0}]}

{AirWaybill [AWB: {0}]}
{AirWaybillDateOfDeparture [Дата вылета: {0}]}
{AirWaybillDateOfArrival [Дата прилета: {0}]}
{AirWaybillGTD [Номер ГТД: {0}]}', 0),
 -- for states
(11, N'ru', NULL, NULL, 0),
(12, N'ru', NULL, NULL, 0),
(13, N'ru', NULL, NULL, 0),
(14, N'ru', NULL, NULL, 0),
(15, N'ru', NULL, NULL, 0),
(16, N'ru', NULL, NULL, 0),
(17, N'ru', NULL, NULL, 0),
(18, N'ru', NULL, NULL, 0),
(19, N'ru', NULL, NULL, 0),
(20, N'ru', NULL, NULL, 0),
(21, N'ru', NULL, NULL, 0),
(22, N'ru', NULL, NULL, 0),
(23, N'ru', NULL, NULL, 0),
-- calculate
(24, N'ru', N'Расчет стоимости заявки {ApplicationDisplay}',
N'Расчет стоимости заявки {ApplicationDisplay}.
Сальдо: {ClientBalance}€ ({CalculationTimestamp}).', 0),
(25, N'ru', NULL, NULL, 0),
-- balnce
(26, N'ru', N'Поступление денежных средств {ClientNic}',
N'Поступление денежных средств: {Money}€{Comment [ ({0}).]} 
Сальдо: {ClientBalance}€ ({Timestamp}).', 0),
(27, N'ru', N'Списание денежных средств {ClientNic}',
N'Списание денежных средств: {Money}€{Comment [ ({0}).]} 
Сальдо: {ClientBalance}€ ({Timestamp}).', 0)


INSERT [dbo].[StateEmailTemplate]
([StateId],	[EmailTemplateId],	[EnableEmailSend],	[UseEventTemplate]) VALUES
(11,		11,					1,					1),
(3,			12,					1,					1),
(13,		13,					1,					1),
(2,			14,					1,					1),
(10,		15,					1,					1),
(15,		16,					1,					1),
(4,			17,					1,					1),
(6,			18,					1,					1),
(7,			19,					1,					1),
(8,			20,					1,					1),
(9,			21,					1,					1),
(12,		22,					1,					1),
(14,		23,					1,					1)


INSERT [dbo].[EventEmailRecipient]
([RoleId], [EventTypeId]) VALUES
-- Admin 1
(1, 1),
(1, 2),
(1, 3),
(1, 8),
(1, 11),
(1, 13),
(1, 14),
-- Sender 2
(2, 1),
(2, 4),
(2, 5),
(2, 6),
-- Forwarder 4
(4, 2),
-- Client 5
(5, 1),
(5, 2),
(5, 3),
(5, 4),
(5, 5),
(5, 6),
(5, 7),
(5, 8),
(5, 9),
(5, 10),
(5, 11),
(5, 13),
(5, 14)