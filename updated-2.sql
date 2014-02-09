USE [$(MainDbName)]
GO

DELETE [dbo].[EmailTemplate]
WHERE [Id] >= 11 AND [Id] <= 23

SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
INSERT [dbo].[EmailTemplate] ([Id]) VALUES
(30)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF

INSERT [dbo].[EventEmailTemplate]
([EventTypeId], [EmailTemplateId], [EnableEmailSend]) VALUES
(18, 30, 1)

INSERT [dbo].[EventEmailRecipient]
([RoleId], [EventTypeId]) VALUES
(1, 18),
(5, 18)

INSERT [dbo].[EmailTemplateLocalization]
([EmailTemplateId], [TwoLetterISOLanguageName], [Subject], [Body], [IsBodyHtml]) VALUES
(30, N'ru', N'Alicargo',
N'Добрый день, {ClientNic}! Вы зарегистрированны в системе отслеживания грузов компании Alicargo. Имя доступа: {Login}, Пароль: {Password}.', 0)