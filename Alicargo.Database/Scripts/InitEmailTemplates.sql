SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
INSERT [dbo].[EmailTemplate] ([Id]) VALUES
(1), (2), (3), (4), (5), (6), (7), (8), (9), (10),
(11),
(24), (25), (26), (27), (28), (29), (30), 
(31), (32), (33), (34), (35), (36), (37), (38)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF


INSERT [dbo].[EventEmailTemplate]
([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES
-- Application events
(1, 1, 1), -- ApplicationCreated
(2, 10, 1), -- ApplicationSetState
(9, 6, 1),
(10, 7, 1),
(15, 28, 1), -- SetSender
(16, 29, 1), -- SetForwarder
(20, 32, 1),

-- Application's file events
(3, 2, 1),
(4, 4, 1),
(5, 5, 1),
(6, 8, 1),
(7, 3, 1),
(8, 9, 1),

-- Application's calculation events
(11, 24, 1), -- Calculate
(12, 25, 0), -- CalculationCanceled

-- Client events
(13, 26, 1), -- BalanceIncreased
(14, 27, 1), -- BalanceDecreased
(18, 30, 1),

-- Awb
(19, 31, 1), -- AwbCreated
(21, 33, 1), -- GTDFileUploaded
(22, 34, 1), -- GTDAdditionalFileUploaded
(23, 35, 1), -- AwbPackingFileUploaded
(24, 36, 1), -- AwbInvoiceFileUploaded
(25, 37, 1), -- AWBFileUploaded
(26, 38, 1), -- DrawFileUploaded
(27, 11, 1)  -- SetBroker


INSERT [dbo].[EmailTemplateLocalization]
([EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES
(1, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Создана новая заявка.
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
{SenderContact [Отправитель, контактное лицо: {0}]}
{SenderAddress [Отправитель, адрес: {0}]}
{SenderPhone [Отправитель, телефон: {0}]}
{SenderEmail [Отправитель, email: {0}]}

{CarrierName [Курьер: {0}]}
{CarrierContact [Курьер, контактное лицо: {0}]}
{CarrierAddress [Курьер, адрес: {0}]}
{CarrierPhone [Курьер, телефон: {0}]}
{CarrierEmail [Курьер, email: {0}]}

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

(1, 'en', N'Alicargo : Order#{DisplayNumber}', N'Created new order.
Number order: {DisplayNumber}
Client: {ClientNic}
Factory: {FactoryName}
Brands: {MarkName}
Created: {CreationTimestamp}', 0),

(1, 'it', N'Alicargo : Order#{DisplayNumber}', N'Created new order.
Number order: {DisplayNumber}
Client: {ClientNic}
Factory: {FactoryName}
Brands: {MarkName}
Created: {CreationTimestamp}', 0),

(2, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ Счет-фактура.
Просьба подписать и выслать почтой по адресу:
Россия - ООО "Трэйд Инвест"
107113 Москва, Сокольническая пл. 4а офис 309  

С уважением,
Alicargo  srl', 0),

(2, 'en', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),
 
(2, 'it', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. Commercial invoice is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),

(3, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен счет за доставку.', 0),
(3, 'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0),
(3, 'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added due for delivery.', 0),

(4, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен инвойс {UploadedFile}.', 0),
(4, 'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {UploadedFile}.', 0),
(4, 'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added invoice {UploadedFile}.', 0),

(5, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки:  {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен пакинг {UploadedFile}.', 0),
(5, 'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing {UploadedFile}.', 0),
(5, 'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added packing {UploadedFile}.', 0),

(6, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0),
(6, 'en', N'Alicargo : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0),
(6, 'it', N'Alicargo : Order#{DisplayNumber}', N'Предположительная дата получения — {DateOfCargoReceipt}.', 0),

(7, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Задан транзитный референс {TransitReference}.', 0),
(7, 'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0),
(7, 'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}. Specified transit reference {TransitReference}.', 0),

(8, 'ru', N'Alicargo : Order#{DisplayNumber}', N'Изменение статуса заявки: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, добавлен swift {UploadedFile}.', 0),
(8, 'en', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift {UploadedFile}.', 0),
(8, 'it', N'Alicargo : Order#{DisplayNumber}', N'Changing the status of the application: {DisplayNumber}, {FactoryName}, {MarkName}, {Invoice}, added a swift {UploadedFile}.', 0),

(9, 'ru', N'Alicargo : Заявка #{DisplayNumber}', N'Добрый день, {LegalEntity}
Изменение статуса заявки {DisplayNumber} - прикреплен документ ТОРГ-12.
Просьба подписать и выслать почтой по адресу:
Россия - ООО "Трэйд Инвест"
107113 Москва, Сокольническая пл. 4а офис 309

С уважением,
Alicargo  srl', 0),

(9, 'en', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),

(9, 'it', N'Alicargo : Order#{DisplayNumber}', N'Hello, {LegalEntity}
Changing the status of the application {DisplayNumber}. TORG-12 is attached.
Please sign and send by mail to:
Russia - LLC "Trade Invest"
107113 Moscow, Sokolniki Sq. 4a office 309

Sincerely,
Alicargo  srl', 0),

(10, 'ru', N'Alicargo : Заявка #{DisplayNumber}', 
N'Изменение статуса заявки: {StateName}
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
{SenderContact [Отправитель, контактное лицо: {0}]}
{SenderAddress [Отправитель, адрес: {0}]}
{SenderPhone [Отправитель, телефон: {0}]}
{SenderEmail [Отправитель, email: {0}]}

{CarrierName [Курьер: {0}]}
{CarrierContact [Курьер, контактное лицо: {0}]}
{CarrierAddress [Курьер, адрес: {0}]}
{CarrierPhone [Курьер, телефон: {0}]}
{CarrierEmail [Курьер, email: {0}]}

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
(10, 'en', N'Alicargo : Order#{DisplayNumber}', N'State is changed: {StateName}', 0),
(10, 'it', N'Alicargo : Order#{DisplayNumber}', N'State is changed: {StateName}', 0),

(11, 'ru', N'Создана авианакладная',
N'Номер накладной: {AirWaybill}.
Дата вылета: {DateOfDeparture}.
Дата прилета: {DateOfArrival}.
Аэропорт вылета: {DepartureAirport}.
Аэропорт прилета: {ArrivalAirport}.
Количество мест: {TotalCount}.
Общий вес {TotalWeight}.', 0),

-- calculate
(24, 'ru', 
N'Расчет стоимости заявки {ApplicationDisplay}',
N'Расчет стоимости заявки {ApplicationDisplay}.
Сальдо: {ClientBalance}€ ({CalculationTimestamp}).

{AirWaybillDisplay}

{Weight} kg * {TariffPerKg}€ = {WeightCost}€
скотч - {ScotchCost}€
страховка - {InsuranceCost}€
фактура - {FactureCost}€
доставка - {TransitCost}€
забор с фабрики - {PickupCost}€

Итого - {TotalCost}€', 0),
(25, 'ru', NULL, NULL, 0),
-- balnce
(26, 'ru', N'Поступление денежных средств {ClientNic}',
N'Поступление денежных средств: {Money}€{Comment [ ({0}).]} 
Сальдо: {ClientBalance}€ ({Timestamp}).', 0),
(27, 'ru', N'Списание денежных средств {ClientNic}',
N'Списание денежных средств: {Money}€{Comment [ ({0}).]} 
Сальдо: {ClientBalance}€ ({Timestamp}).', 0),

(28, 'ru', N'Установлен отправитель {SenderName} на заявку {DisplayNumber}',
N'Установлен отправитель {SenderName} на заявку {DisplayNumber}', 0),

(29, 'ru', N'Установлен перевозчик {ForwarderName} на заявку {DisplayNumber}',
N'Установлен перевозчик {ForwarderName} на заявку {DisplayNumber}', 0),

(30, 'ru', N'Alicargo',
N'Добрый день, {ClientNic}! Вы зарегистрированны в системе отслеживания грузов компании Alicargo. Имя доступа: {Login}, Пароль: {Password}.', 0),

(31, 'ru', N'Alicargo',
N'Создана авианакладная. Номер накладной: {AirWaybill}.
Аэропорт вылета: {DepartureAirport}/{DateOfDeparture}.
Аэропорт прилета: {ArrivalAirport}/{DateOfArrival}.
Вес: {TotalWeight}.
Количество мест: {TotalCount}.', 0),

(32, 'ru', N'Alicargo',
N'Заявке {DisplayNumber} задана авианакладная, Аэропорт вылета: {DepartureAirport}/{DateOfDeparture}, Аэропорт прилета: {ArrivalAirport}/{DateOfArrival}, Вес: {TotalWeight}, Количество мест: {TotalCount}. Номер накладной: {AirWaybill}.', 0),

(33, 'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен ГТД»', 0),
(33, 'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added GTD"', 0),

(34, 'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен ГТД-дополнение»', 0),
(34, 'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added the GTD-complement"', 0),

(35, 'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен пакинг-лист»', 0),
(35, 'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added paking-list"', 0),

(36, 'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, «добавлен инвойс»', 0),
(36, 'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, "added invoice"', 0),

(37, 'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, добавлена авиа-накладная', 0),
(37, 'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, added air waybill', 0),

(38, 'ru', N'Alicargo', N'Изменение статуса  AWB: Номер авиа-накладной {AirWaybill}, добавлен Draw', 0),
(38, 'en', N'Alicargo', N'Changing the status of AWB: air waybill number {AirWaybill}, added Draw', 0)


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
(1, 15),
(1, 16),
(1, 18),
(1, 19),
(1, 20),
(1, 21),
(1, 22),
(1, 24),
(1, 25),
(1, 26),
-- Sender 2
(2, 1),
(2, 4),
(2, 5),
(2, 6),
(2, 15),
(2, 24),
(2, 25),
-- Broker 3
(3, 23),
(3, 25),
(3, 26),
(3, 27),
-- Forwarder 4
(4, 2),
(4, 16),
(4, 20),
(4, 27),
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
(5, 14),
(5, 15),
(5, 16),
(5, 18),
(5, 21),
(5, 22),
(5, 27),
-- Carrier
(6, 2)