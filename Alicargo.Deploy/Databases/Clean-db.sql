USE [Alicargo]
GO

TRUNCATE TABLE [dbo].[Application]
TRUNCATE TABLE [dbo].[Bill]
TRUNCATE TABLE [dbo].[Admin]
TRUNCATE TABLE [dbo].Calculation
TRUNCATE TABLE [dbo].ClientBalanceHistory
TRUNCATE TABLE [dbo].CarrierCity
TRUNCATE TABLE [dbo].EmailMessage
TRUNCATE TABLE [dbo].[Event]
TRUNCATE TABLE [dbo].ForwarderCity
TRUNCATE TABLE [dbo].Manager
TRUNCATE TABLE [dbo].SenderCountry
GO

DELETE [dbo].Client
DELETE [dbo].Transit
DELETE [dbo].[AirWaybill]
DELETE [dbo].[Broker]
DELETE [dbo].Carrier
DELETE [dbo].Forwarder
DELETE [dbo].Sender
DELETE [dbo].[User]
GO

SET IDENTITY_INSERT [dbo].[User] ON;
INSERT INTO [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES
(1, N'Admin', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru')
SET IDENTITY_INSERT [dbo].[User] OFF;

SET IDENTITY_INSERT [dbo].[Admin] ON
INSERT [dbo].[Admin] ([Id], [UserId], [Name], [Email]) VALUES (1, 1, N'Test admin', N'alicargo.clone@gmail.com')
SET IDENTITY_INSERT [dbo].[Admin] OFF

USE Alicargo_Files
GO

TRUNCATE TABLE [dbo].ApplicationFile
TRUNCATE TABLE [dbo].AwbFile
TRUNCATE TABLE [dbo].ClientContract
GO