USE [$(MainDbName)]
GO

UPDATE [EmailTemplateLocalization]
SET [Body] = N'Создана новая заявка.
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
{AirWaybillGTD [Номер ГТД: {0}]}'
WHERE [EmailTemplateId] = 1
GO

UPDATE [EmailTemplateLocalization]
SET [Body] = N'Изменение статуса заявки: {StateName}
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
{AirWaybillGTD [Номер ГТД: {0}]}'
WHERE [EmailTemplateId] = 10
GO

UPDATE [EmailTemplateLocalization]
SET [Body] = N'Создана авианакладная. Номер накладной: {AirWaybill}.
Аэропорт вылета: {DepartureAirport}/{DateOfDeparture}.
Аэропорт прилета: {ArrivalAirport}/{DateOfArrival}.
Вес: {TotalWeight}.
Количество мест: {TotalCount}.'
WHERE [EmailTemplateId] = 32
GO

SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
INSERT [dbo].[EmailTemplate] ([Id]) VALUES
(11)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF

INSERT [dbo].[EmailTemplateLocalization]
([EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES
(11, 'ru', N'Создана авианакладная',
N'Номер накладной: {AirWaybill}.
Дата вылета: {DateOfDeparture}.
Дата прилета: {DateOfArrival}.
Аэропорт вылета: {DepartureAirport}.
Аэропорт прилета: {ArrivalAirport}.
Количество мест: {TotalCount}.
Общий вес {TotalWeight}.', 0)

INSERT [dbo].[EventEmailTemplate]
([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES
(27, 11, 1)  -- SetBroker

INSERT [dbo].[EventEmailRecipient]
([RoleId], [EventTypeId]) VALUES
(3, 27),
(4, 27),
(5, 27)
GO

DELETE FROM [dbo].[EventEmailRecipient]
WHERE ([RoleId] = 3 AND [EventTypeId] = 2) 
OR ([RoleId] = 3 AND [EventTypeId] = 19)
GO