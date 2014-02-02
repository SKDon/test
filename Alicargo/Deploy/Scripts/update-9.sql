USE [$(MainDbName)]
GO

DELETE FROM [EmailTemplate]
SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (1)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (2)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (3)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (4)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (5)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (6)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (7)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (8)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (9)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (10)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (11)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (12)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (13)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (14)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (15)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (16)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (17)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (18)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (19)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (20)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (21)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (22)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (23)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (24)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (25)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (26)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (27)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (28)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (29)
INSERT [dbo].[EmailTemplate] ([Id]) VALUES (30)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF

DELETE FROM [EventEmailTemplate]
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (1, 1, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (3, 2, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (7, 3, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (4, 4, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (5, 5, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (9, 6, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (10, 7, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (6, 8, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (8, 9, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (2, 10, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (11, 24, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (12, 25, 0)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (13, 26, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (14, 27, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (15, 28, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (16, 29, 1)
INSERT [dbo].[EventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (17, 30, 0)

DELETE FROM [EmailTemplateLocalization]
SET IDENTITY_INSERT [dbo].[EmailTemplateLocalization] ON 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (1, 1, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Создана новая заявка.
Номер заказа: {DisplayNumber}
{DateOfCargoReceipt [Предположительная дата получения: {0}]}
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
{SenderName [Отправитель: {0}]}
{ForwarderName [Транзитный перевозчик: {0}]}
{CarrierName [Курьер: {0}]}

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
{AirWaybillGTD [Номер ГТД: {0}]}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (2, 1, N'en', N'Alicargo : Order#{DisplayNumber}', N'Создана новая заявка.
Номер заказа: {DisplayNumber}
{DateOfCargoReceipt [Предположительная дата получения: {0}]}
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
{SenderName [Отправитель: {0}]}
{ForwarderName [Транзитный перевозчик: {0}]}
{CarrierName [Курьер: {0}]}

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
{AirWaybillGTD [Номер ГТД: {0}]}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (3, 1, N'it', N'Alicargo : Order#{DisplayNumber}', N'Created new order.
Number order: {DisplayNumber}
Client: {ClientNic}
Factory: {FactoryName}
Brands: {MarkName}
Created: {CreationTimestamp}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (4, 2, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ Счет-фактура.

С уважением,
ООО Трейд инвест', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (5, 2, N'en', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.

Sincerely,
Trade invest', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (6, 2, N'it', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.

Sincerely,
Trade invest', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (7, 3, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен счет за доставку.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (8, 3, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (9, 3, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (10, 4, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен инвойс {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (11, 4, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (12, 4, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (13, 5, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен пакинг {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (14, 5, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (15, 5, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (16, 6, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (17, 6, N'en', N'Alicargo : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (18, 6, N'it', N'Alicargo : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (19, 7, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Задан транзитный референс {TransitReference}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (20, 7, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (21, 7, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (22, 8, N'ru', N'Alicargo : Order#{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен swift {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (23, 8, N'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (24, 8, N'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift {UploadedFile}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (25, 9, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ ТОРГ-12.
Просьба подписать и выслать почтой по адресу:
Россия - ООО "Трэйд Инвест"
107113 Москва, Сокольническая пл. 4а офис 309

С уважением,
ООО "Трэйд Инвест"', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (26, 9, N'en', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
 LLC "Trade Invest"', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (27, 9, N'it', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
 LLC "Trade Invest"', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (28, 10, N'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {StateName}
Номер заказа: {DisplayNumber}
{DateOfCargoReceipt [Предположительная дата получения: {0}]}
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
{SenderName [Отправитель: {0}]}
{ForwarderName [Транзитный перевозчик: {0}]}
{CarrierName [Курьер: {0}]}

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
{AirWaybillGTD [Номер ГТД: {0}]}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (29, 10, N'en', N'Alicargo : Order#{DisplayNumber}', N'Изменение статуса заявки: {StateName}
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
{AirWaybillGTD [Номер ГТД: {0}]}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (30, 10, N'it', N'Alicargo : Order#{DisplayNumber}', N'Изменение статуса заявки: {StateName}
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
{AirWaybillGTD [Номер ГТД: {0}]}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (31, 11, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (32, 12, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (33, 13, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (34, 14, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (35, 15, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (36, 16, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (37, 17, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (38, 18, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (39, 19, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (40, 20, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (41, 21, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (42, 22, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (43, 23, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (44, 24, N'ru', N'Расчет стоимости заявки {ApplicationDisplay}', N'Расчет стоимости заявки {ApplicationDisplay}.
Сальдо: {ClientBalance}€ ({CalculationTimestamp}).

{AirWaybillDisplay}

{Weight} kg * {TariffPerKg}€ = {WeightCost}€
скотч - {ScotchCost}€
страховка - {InsuranceCost}€
фактура - {FactureCost}€
доставка - {TransitCost}€
забор с фабрики - {PickupCost}€

Итого - {TotalCost}€', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (45, 25, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (46, 26, N'ru', N'Поступление денежных средств {ClientNic}', N'Поступление денежных средств: {Money}€{Comment [ ({0}).]} 
Сальдо: {ClientBalance}€ ({Timestamp}).', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (47, 27, N'ru', N'Списание денежных средств {ClientNic}', N'Списание денежных средств: {Money}€{Comment [ ({0}).]} 
Сальдо: {ClientBalance}€ ({Timestamp}).', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (48, 28, N'ru', N'Установлен отправитель {SenderName} на заявку {DisplayNumber}', N'Установлен отправитель {SenderName} на заявку {DisplayNumber}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (49, 29, N'ru', N'Установлен перевозчик {ForwarderName} на заявку {DisplayNumber}', N'Установлен перевозчик {ForwarderName} на заявку {DisplayNumber}', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (50, 30, N'ru', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (51, 17, N'en', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (52, 18, N'en', NULL, NULL, 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (53, 21, N'en', NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[EmailTemplateLocalization] OFF

DELETE FROM [StateEmailTemplate]
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (11, 11, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (3, 12, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (13, 13, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (2, 14, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (10, 15, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (15, 16, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (4, 17, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (6, 18, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (7, 19, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (8, 20, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (9, 21, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (12, 22, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseEventTemplate]) VALUES (14, 23, 1, 1)

DELETE FROM [CarrierCity]
SET IDENTITY_INSERT [dbo].[CarrierCity] ON 
INSERT [dbo].[CarrierCity] ([Id], [CarrierId], [CityId]) VALUES (1, 11, 39)
SET IDENTITY_INSERT [dbo].[CarrierCity] OFF

DELETE FROM [EventEmailRecipient]
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 1)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 2)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 3)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 4)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 5)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 6)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 7)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 8)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 10)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 11)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 13)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 14)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 15)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 16)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 1)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 2)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 4)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 5)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 6)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 15)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (3, 2)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (4, 2)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (4, 16)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 1)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 2)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 3)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 4)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 5)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 6)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 7)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 8)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 9)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 10)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 11)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 13)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 14)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 15)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 16)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (6, 2)
INSERT [dbo].[EventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (6, 17)

DELETE FROM [StateAvailability]
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (1, 15)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 7)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (2, 15)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (3, 7)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (3, 8)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (3, 9)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 3)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 12)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (5, 14)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (6, 9)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (6, 12)
INSERT [dbo].[StateAvailability] ([RoleId], [StateId]) VALUES (6, 14)

DELETE FROM [StateEmailRecipient]
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (1, 15)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (4, 7)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (4, 8)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 2)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 3)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 4)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 6)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 7)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 8)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 9)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 10)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 13)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 14)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (5, 15)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (6, 8)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (6, 9)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (6, 11)
INSERT [dbo].[StateEmailRecipient] ([RoleId], [StateId]) VALUES (6, 14)

DELETE FROM [StateLocalization]
SET IDENTITY_INSERT [dbo].[StateLocalization] ON 
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (1, N'Новая заявка', N'ru', 1)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (2, N'Nuovo', N'it', 1)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (3, N'New order', N'en', 1)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (4, N'Фабрика ждет оплату', N'ru', 2)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (5, N'Fabbrica attende il pagamento', N'it', 2)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (6, N'Factory awaits payment', N'en', 2)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (7, N'Груз не готов', N'ru', 3)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (8, N'Cargo non è pronto', N'it', 3)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (9, N'Cargo is not ready', N'en', 3)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (10, N'Груз забран на фабрике', N'ru', 4)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (11, N'Cargo è ritirata in fabbrica', N'it', 4)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (12, N'Cargo shipped', N'en', 4)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (13, N'Груз на складе', N'ru', 6)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (14, N'Cargo in magazzino', N'it', 6)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (15, N'Cargo at warehouse', N'en', 6)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (16, N'Груз вылетел', N'ru', 7)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (17, N'Cargo volò', N'it', 7)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (18, N'Cargo departed', N'en', 7)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (20, N'Cargo alla dogana', N'it', 8)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (21, N'Cargo at customs', N'en', 8)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (22, N'Груз выпущен', N'ru', 9)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (23, N'Sdoganate cargo', N'it', 9)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (24, N'Cargo cleared', N'en', 9)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (25, N'Фабрика отправила груз', N'ru', 10)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (26, N'Carico di fabbrica inviato', N'it', 10)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (27, N'Factory cargo sent', N'en', 10)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (28, N'Груз получен', N'ru', 11)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (29, N'Cargo ha ricevuto', N'it', 11)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (30, N'Cargo received', N'en', 11)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (31, N'На транзите', N'ru', 12)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (32, N'Sul transito', N'it', 12)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (33, N'On transit', N'en', 12)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (34, N'Фабрика не отвечает', N'ru', 13)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (35, N'Fabbrica non risponde', N'it', 13)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (36, N'Factory does not respond', N'en', 13)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (37, N'Груз на стопе', N'ru', 14)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (38, N'Stop', N'it', 14)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (39, N'Stop', N'en', 14)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (43, N'Груз готов для забора', N'ru', 15)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (44, N'Cargo ready to pick-up', N'it', 15)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (45, N'Cargo ready to pick-up', N'en', 15)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (46, N'Груз на таможне', N'ru', 8)
SET IDENTITY_INSERT [dbo].[StateLocalization] OFF

DELETE FROM [StateVisibility]
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (1, 15)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (2, 15)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (3, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (3, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (3, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 2)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 3)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 4)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 6)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 10)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 13)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 14)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (5, 15)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (6, 7)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (6, 8)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (6, 9)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (6, 11)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (6, 12)
INSERT [dbo].[StateVisibility] ([RoleId], [StateId]) VALUES (6, 14)
