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
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF


INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (1, 1, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (3, 2, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (7, 3, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (4, 4, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (5, 5, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (9, 6, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (10, 7, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (6, 8, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (8, 9, 1)
INSERT [dbo].[ApplicationEventEmailTemplate] ([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES (2, 10, 1)


SET IDENTITY_INSERT [dbo].[EmailTemplateLocalization] ON 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (1, 1, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Создана новая заявка.
Номер заявки: {DisplayNumber}
Клиент: {ClientNic}
Производитель: {FactoryName}
Марка: {MarkName}
Дата создания:{CreationTimestamp}', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (2, 1, N'en', N'Alicar : Order#{DisplayNumber}', N'Created new order.
Number order: {DisplayNumber}
Client: {ClientNic}
Factory: {FactoryName}
Brands: {MarkName}
Created:{CreationTimestamp}', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (3, 1, N'it', N'Alicar : Order#{DisplayNumber}', N'Created new order.
Number order: {DisplayNumber}
Client: {ClientNic}
Factory: {FactoryName}
Brands: {MarkName}
Created:{CreationTimestamp}', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (4, 2, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ Счет-фактура.
Просьба подписать и выслать почтой по адресу: п/о Домодедово-4 а/я 649 (142004, Московская область, Домодедовский район, г. Домодедово, ул. Корнеева 36.

С уважением,
Alicar  srl', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (5, 2, N'en', N'Alicar : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.
Please sign and send by mail to: Post Office Domodedovo-4 PO Box 649 (142004, Moscow region, Domodedovo district, Domodedovo, st. Korneev 36.

Sincerely,
Alicar  srl', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (6, 2, N'it', N'Alicar : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.
Please sign and send by mail to: Post Office Domodedovo-4 PO Box 649 (142004, Moscow region, Domodedovo district, Domodedovo, st. Korneev 36.

Sincerely,
Alicar  srl', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (7, 3, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен счет за доставку.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (8, 3, N'en', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (9, 3, N'it', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (10, 4, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен инвойс {InvoiceFileName}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (11, 4, N'en', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {InvoiceFileName}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (12, 4, N'it', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {InvoiceFileName}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (13, 5, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен пакинг.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (14, 5, N'en', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (15, 5, N'it', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (16, 6, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Предположительная дата получения — {DateOfCar Receipt}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (17, 6, N'en', N'Alicar : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCar Receipt}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (18, 6, N'it', N'Alicar : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCar Receipt}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (19, 7, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Задан транзитный референс {TransitReference}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (20, 7, N'en', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (21, 7, N'it', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (22, 8, N'ru', N'Alicar : Order#{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен swift.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (23, 8, N'en', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (24, 8, N'it', N'Alicar : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift.', 0)
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (25, 9, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ ТОРГ-12.
Просьба подписать и выслать почтой по адресу: п/о Домодедово-4 а/я 649 (142004, Московская область, Домодедовский район, г. Домодедово, ул. Корнеева 36.

С уважением,
Alicar  srl', 0)

INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (26, 9, N'en', N'Alicar : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to: Post Office Domodedovo-4 PO Box 649 (142004, Moscow region, Domodedovo district, Domodedovo, st. Korneev 36.

Sincerely,
Alicar  srl', 0)

INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (27, 9, N'it', N'Alicar : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to: Post Office Domodedovo-4 PO Box 649 (142004, Moscow region, Domodedovo district, Domodedovo, st. Korneev 36.

Sincerely,
Alicar  srl', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (28, 10, N'ru', N'Alicar : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {StateName}
Номер заказа: {DisplayNumber}
{DateOfCar Receipt [Предположительная дата получения: {0}]}
{TransitCarrierName [Перевозчик: {0}]}
{FactoryName} [Производитель: {0}]}
{FactoryEmail}[Email фабрики: {0}]}
{FactoryPhone}[Телефон фабрики: {0}]}
{FactoryContact}[Контактное лицо фабрики: {0}]}
{DaysInWork}[Дней в работе: {0}]}
{Invoice}[Инвойс: {0}]}
{CreationTimestamp}[Дата создания: {0}]}
{StateChangeTimestamp}[Дата сметы статуса: {0}]}
{MarkName}[Марка: {0}]}
{Count}[Количество мест (коробки): {0}]}
{Volume}[Объем (m3 ): {0}]}
{Weight}[Вес (кг): {0}]}
{Characteristic}[Характеристика товара: {0}]}
{Value}[Стоимость: {0}]}
{AddressLoad}[Адрес забора груза: {0}]}
{CountryName}[Страна: {0}]}
{WarehouseWorkingTime}[Время работы склада: {0}]}
{TermsOfDelivery}[Условия поставки: {0}]}
{MethodOfDelivery}[Способ доставки: {0}]}
{TransitReference}[Транзитный референс: {0}]}

Транзит по России:
{TransitCity}[Город: {0}]}
{TransitAddress}[Адрес: {0}]}
{TransitRecipientName}[Получатель: {0}]}
{TransitPhone}[Телефон: {0}]}
{TransitWarehouseWorkingTime}[Время работы склада: {0}]}
{MethodOfTransit}[Способ транзита: {0}]}
{DeliveryType}[Тип доставки: {0}]}

{AirWaybill}[AWB: {0}]}
{AirWaybillDateOfDeparture}[Дата вылета: {0}]}
{AirWaybillDateOfArrival}[Дата прилета: {0}]}
{AirWaybillGTD}[Номер ГТД: {0}]}', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (29, 10, N'en', N'Alicar : Order#{DisplayNumber}', N'Изменение статуса заявки: {StateName}
Номер заказа: {DisplayNumber}
{DateOfCar Receipt [Предположительная дата получения: {0}]}
{TransitCarrierName [Перевозчик: {0}]}
{FactoryName} [Производитель: {0}]}
{FactoryEmail}[Email фабрики: {0}]}
{FactoryPhone}[Телефон фабрики: {0}]}
{FactoryContact}[Контактное лицо фабрики: {0}]}
{DaysInWork}[Дней в работе: {0}]}
{Invoice}[Инвойс: {0}]}
{CreationTimestamp}[Дата создания: {0}]}
{StateChangeTimestamp}[Дата сметы статуса: {0}]}
{MarkName}[Марка: {0}]}
{Count}[Количество мест (коробки): {0}]}
{Volume}[Объем (m3 ): {0}]}
{Weight}[Вес (кг): {0}]}
{Characteristic}[Характеристика товара: {0}]}
{Value}[Стоимость: {0}]}
{AddressLoad}[Адрес забора груза: {0}]}
{CountryName}[Страна: {0}]}
{WarehouseWorkingTime}[Время работы склада: {0}]}
{TermsOfDelivery}[Условия поставки: {0}]}
{MethodOfDelivery}[Способ доставки: {0}]}
{TransitReference}[Транзитный референс: {0}]}

Транзит по России:
{TransitCity}[Город: {0}]}
{TransitAddress}[Адрес: {0}]}
{TransitRecipientName}[Получатель: {0}]}
{TransitPhone}[Телефон: {0}]}
{TransitWarehouseWorkingTime}[Время работы склада: {0}]}
{MethodOfTransit}[Способ транзита: {0}]}
{DeliveryType}[Тип доставки: {0}]}

{AirWaybill}[AWB: {0}]}
{AirWaybillDateOfDeparture}[Дата вылета: {0}]}
{AirWaybillDateOfArrival}[Дата прилета: {0}]}
{AirWaybillGTD}[Номер ГТД: {0}]}', 0)
 
INSERT [dbo].[EmailTemplateLocalization] ([Id], [EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES (30, 10, N'it', N'Alicar : Order#{DisplayNumber}', N'Изменение статуса заявки: {StateName}
Номер заказа: {DisplayNumber}
{DateOfCar Receipt [Предположительная дата получения: {0}]}
{TransitCarrierName [Перевозчик: {0}]}
{FactoryName} [Производитель: {0}]}
{FactoryEmail}[Email фабрики: {0}]}
{FactoryPhone}[Телефон фабрики: {0}]}
{FactoryContact}[Контактное лицо фабрики: {0}]}
{DaysInWork}[Дней в работе: {0}]}
{Invoice}[Инвойс: {0}]}
{CreationTimestamp}[Дата создания: {0}]}
{StateChangeTimestamp}[Дата сметы статуса: {0}]}
{MarkName}[Марка: {0}]}
{Count}[Количество мест (коробки): {0}]}
{Volume}[Объем (m3 ): {0}]}
{Weight}[Вес (кг): {0}]}
{Characteristic}[Характеристика товара: {0}]}
{Value}[Стоимость: {0}]}
{AddressLoad}[Адрес забора груза: {0}]}
{CountryName}[Страна: {0}]}
{WarehouseWorkingTime}[Время работы склада: {0}]}
{TermsOfDelivery}[Условия поставки: {0}]}
{MethodOfDelivery}[Способ доставки: {0}]}
{TransitReference}[Транзитный референс: {0}]}

Транзит по России:
{TransitCity}[Город: {0}]}
{TransitAddress}[Адрес: {0}]}
{TransitRecipientName}[Получатель: {0}]}
{TransitPhone}[Телефон: {0}]}
{TransitWarehouseWorkingTime}[Время работы склада: {0}]}
{MethodOfTransit}[Способ транзита: {0}]}
{DeliveryType}[Тип доставки: {0}]}

{AirWaybill}[AWB: {0}]}
{AirWaybillDateOfDeparture}[Дата вылета: {0}]}
{AirWaybillDateOfArrival}[Дата прилета: {0}]}
{AirWaybillGTD}[Номер ГТД: {0}]}', 0)
 
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
SET IDENTITY_INSERT [dbo].[EmailTemplateLocalization] OFF


INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (11, 11, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (3, 12, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (13, 13, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (2, 14, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (10, 15, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (15, 16, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (4, 17, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (6, 18, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (7, 19, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (8, 20, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (9, 21, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (12, 22, 1, 1)
INSERT [dbo].[StateEmailTemplate] ([StateId], [EmailTemplateId], [EnableEmailSend], [UseApplicationEventTemplate]) VALUES (14, 23, 1, 1)


INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 1)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 2)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 3)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (1, 8)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 1)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 4)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 5)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (2, 6)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (4, 2)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 1)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 2)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 3)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 4)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 5)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 6)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 7)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 8)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 9)
INSERT [dbo].[ApplicationEventEmailRecipient] ([RoleId], [EventTypeId]) VALUES (5, 10)