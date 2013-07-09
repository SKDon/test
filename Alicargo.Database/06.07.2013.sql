/****** Object:  Table [dbo].[State]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_dbo.State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[State] ON
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (1, N'New', 10)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (2, N'Factory awaits payment', 40)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (3, N'Cargo is not ready', 20)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (4, N'Cargo withdraw', 60)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (6, N'Cargo in stock', 70)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (7, N'Cargo flew', 90)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (8, N'Cargo at customs', 100)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (9, N'Customs cleared cargo', 110)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (10, N'Factory cargo sent', 50)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (11, N'Cargo received', 140)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (12, N'On transit', 120)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (13, N'Factory does not respond', 30)
INSERT [dbo].[State] ([Id], [Name], [Position]) VALUES (14, N'Stop', 130)
SET IDENTITY_INSERT [dbo].[State] OFF
/****** Object:  Table [dbo].[Factory]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](320) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](320) NULL,
 CONSTRAINT [PK_dbo.Factory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Factory_Name] ON [dbo].[Factory] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Factory] ON
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (1, N'Factory 0', N'85241f3e-5433-4c81-a08a-d56001705e77', N'factory0@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (2, N'Factory 1', N'81167170-34f3-4077-9a61-d0b9e7c726af', N'factory1@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (3, N'Factory 2', N'c5cb43af-dca3-45f7-bc6a-78e5ff919221', N'factory2@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (4, N'Factory 3', N'78c79dbe-e032-41d3-9f12-c6a662d25c2c', N'factory3@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (5, N'Factory 4', N'bf420c4e-d797-4562-8247-6255549fe639', N'factory4@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (6, N'Factory 5', N'c86dc08a-2e5a-4a98-a4a7-fa5dd3a3fe10', N'factory5@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (7, N'Factory 6', N'f121af85-d859-4e22-80c2-b24dd5de44a0', N'factory6@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (8, N'Factory 7', N'da46619b-a492-4e05-9d02-4264544020f7', N'factory7@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (9, N'Factory 8', N'880356f1-5c94-44d0-8dc0-0780ba43416c', N'factory8@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (10, N'Factory 9', N'976bcd0f-4196-4c2d-b058-47eeb6aeca58', N'factory9@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (11, N'Factory 10', N'd11a64bb-efb4-44f1-bb10-6f2f417b1145', N'factory10@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (12, N'Factory 11', N'be3bfe82-8b50-4a6b-94cb-ee5ce1adb09c', N'factory11@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (13, N'Factory 12', N'f9359b29-5f71-4622-88b7-a86672b92afa', N'factory12@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (14, N'Factory 13', N'56c5da6e-9794-4790-9f39-76007b1f2475', N'factory13@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (15, N'Factory 14', N'f9290c0d-5357-4f42-944c-49768b00fcbf', N'factory14@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (16, N'Factory 15', N'5007b5e6-2e15-441e-999f-e876b8a24c4b', N'factory15@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (17, N'Factory 16', N'47ce0d87-08bc-4a9e-9ed6-c3484a7333c1', N'factory16@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (18, N'Factory 17', N'152504b9-7d60-4e05-a5e5-31b786800a19', N'factory17@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (19, N'Factory 18', N'3f13f1af-6fe5-4796-bc52-38ec100ee72e', N'factory18@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (20, N'Factory 19', N'04b4596c-5a00-466e-9c3b-995fead87eae', N'factory19@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (21, N'Factory 20', N'd6ddd809-fbc7-4bd6-81cd-e4212bcce90e', N'factory20@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (22, N'Factory 21', N'9eff55ad-0715-4b06-9df8-8953a1057e0d', N'factory21@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (23, N'Factory 22', N'c1bc13ca-6c18-422a-916a-9e16e13d8322', N'factory22@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (24, N'Factory 23', N'd4fb077c-461a-4041-a8eb-687c277ba8f7', N'factory23@mail.ru')
INSERT [dbo].[Factory] ([Id], [Name], [Phone], [Email]) VALUES (25, N'Factory 24', N'd0535456-2dc1-4fe0-9f58-4b9717b4bf65', N'factory24@mail.ru')
SET IDENTITY_INSERT [dbo].[Factory] OFF
/****** Object:  Table [dbo].[Country]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_En] [nvarchar](128) NOT NULL,
	[Name_Ru] [nvarchar](128) NOT NULL,
	[Code] [char](2) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
SET IDENTITY_INSERT [dbo].[Country] ON
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (1, N'Abkhazia', N'Абхазия', N'  ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (2, N'Afghanistan', N'Афганистан', N'AF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (3, N'Aland Islands', N'Аландские острова', N'AX')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (4, N'Albania', N'Албания', N'AL')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (5, N'Algeria', N'Алжир', N'DZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (6, N'American Samoa', N'Американское Самоа', N'AS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (7, N'Andorra', N'Андорра', N'AD')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (8, N'Angola', N'Ангола', N'AO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (9, N'Anguilla', N'Ангилья', N'AI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (10, N'Antarctica', N'Антарктика', N'AQ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (11, N'Antigua and Barbuda', N'Антигуа и Барбуда', N'AG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (12, N'Argentina', N'Аргентина', N'AR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (13, N'Armenia', N'Армения', N'AM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (14, N'Aruba', N'Аруба', N'AW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (15, N'Australia', N'Австралия', N'AU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (16, N'Austria', N'Австрия', N'AT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (17, N'Azerbaijan', N'Азербайджан', N'AZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (18, N'Azores', N'Азорские острова', N'PT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (19, N'Bahamas', N'Багамские Острова', N'BS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (20, N'Bahrain', N'Бахрейн', N'BH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (21, N'Bangladesh', N'Бангладеш', N'BD')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (22, N'Barbados', N'Барбадос', N'BB')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (23, N'Belarus', N'Беларусь', N'BY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (24, N'Belgium', N'Бельгия', N'BE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (25, N'Belize', N'Белиз', N'BZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (26, N'Benin', N'Бенин', N'BJ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (27, N'Bermuda', N'Бермудские Острова', N'BM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (28, N'Bolivia', N'Боливия', N'BO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (29, N'Bosnia-Herzegovina', N'Босния и Герцеговина', N'BA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (30, N'Botswana', N'Ботсвана', N'BW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (31, N'Bouvet Island', N'Буве', N'BV')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (32, N'Brazil', N'Бразилия', N'BR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (33, N'British Indian Ocean Territory', N'Британская территория в Индийском океане', N'IO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (34, N'Brunei Darussalam', N'Бруней', N'BN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (35, N'Bulgaria', N'Болгария', N'BG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (36, N'Burkina Faso', N'Буркина-Фасо', N'BF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (37, N'Burundi', N'Бурунди', N'BI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (38, N'Buthan', N'Бутан', N'BT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (39, N'Cambodia', N'Камбоджа', N'KH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (40, N'Cameroon', N'Камерун', N'CM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (41, N'Canada', N'Канада', N'CA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (42, N'Cape Verde', N'Кабо-Верде', N'CV')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (43, N'Cayman Islands', N'Каймановы Острова', N'KY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (44, N'Central African Rep.', N'Центральноафриканская Республика', N'CF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (45, N'Chad', N'Чад', N'TD')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (46, N'Chile', N'Чили', N'CL')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (47, N'China', N'Китай', N'CN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (48, N'Christmas Island', N'Остров Рождества', N'CX')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (49, N'Cocos (Keeling) Isl.', N'Кокосовые Острова', N'CC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (50, N'Colombia', N'Колумбия', N'CO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (51, N'Comoros', N'Коморские Острова', N'KM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (52, N'Cook Islands', N'Кука острова', N'CK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (53, N'Costa Rica', N'Коста-Рика', N'CR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (54, N'Croatia', N'Хорватия', N'HR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (55, N'Cuba', N'Куба', N'CU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (56, N'Cyprus', N'Кипр', N'CY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (57, N'Czech Republic', N'Чехия', N'CZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (58, N'Democratic Republic of the Congo', N'Конго, Демократическая Республика', N'CD')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (59, N'Denmark', N'Дания', N'DK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (60, N'Djibouti', N'Джибути', N'DJ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (61, N'Dominica', N'Доминика', N'DM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (62, N'Dominican Republic', N'Доминиканская Республика', N'DO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (63, N'East Timor', N'Восточный Тимор', N'TP')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (64, N'Ecuador', N'Эквадор', N'EC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (65, N'Egypt', N'Египет', N'EG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (66, N'El Salvador', N'Сальвадор', N'SV')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (67, N'Equatorial Guinea', N'Экваториальная Гвинея', N'GQ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (68, N'Eritrea', N'Эритрея', N'ER')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (69, N'Estonia', N'Эстония', N'EE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (70, N'Ethiopia', N'Эфиопия', N'ET')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (71, N'Falkland Islands', N'Фолклендские (Мальвинские) острова', N'FK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (72, N'Faroe Islands', N'Фарерские Острова', N'FO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (73, N'Fiji', N'Фиджи', N'FJ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (74, N'Finland', N'Финляндия', N'FI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (75, N'France', N'Франция', N'FR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (76, N'French Polynesia', N'Французская Полинезия', N'PF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (77, N'French Southern and Antarctic Lands', N'Французские Южные и Антарктические Территории', N'TF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (78, N'Gabon', N'Габон', N'GA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (79, N'Gambia', N'Гамбия', N'GM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (80, N'Georgia', N'Грузия', N'GE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (81, N'Germany', N'Германия', N'DE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (82, N'Ghana', N'Гана', N'GH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (83, N'Gibraltar', N'Гибралтар', N'GI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (84, N'Greece', N'Греция', N'GR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (85, N'Greenland', N'Гренландия', N'GL')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (86, N'Grenada', N'Гренада', N'GD')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (87, N'Guadeloupe (Fr.)', N'Гваделупа', N'GP')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (88, N'Guam (US)', N'Гуам', N'GU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (89, N'Guatemala', N'Гватемала', N'GT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (90, N'Guernsey', N'Гернси', N'GG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (91, N'Guinea', N'Гвинея', N'GN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (92, N'Guinea Bissau', N'Гвинея-Бисау', N'GW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (93, N'Guyana', N'Гвиана', N'GY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (94, N'Guyana (Fr.)', N'Гайана', N'GF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (95, N'Haiti', N'Гаити', N'HT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (96, N'Heard Island and McDonald Islands', N'Острова Херд и Макдональд', N'HM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (97, N'Honduras', N'Гондурас', N'HN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (98, N'Hong Kong', N'Гонконг', N'HK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (99, N'Hungary', N'Венгрия', N'HU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (100, N'Iceland', N'Исландия', N'IS')
GO
print 'Processed 100 total records'
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (101, N'India', N'Индия', N'IN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (102, N'Indonesia', N'Индонезия', N'ID')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (103, N'Iran', N'Иран', N'IR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (104, N'Iraq', N'Ирак', N'IQ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (105, N'Ireland', N'Ирландия', N'IE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (106, N'Isle of Man', N'Остров Мэн', N'IM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (107, N'Israel', N'Израиль', N'IL')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (108, N'Italy', N'Италия', N'IT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (109, N'Ivory Coast', N'Кот-д''Ивуар', N'CI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (110, N'Jamaica', N'Ямайка', N'JM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (111, N'Japan', N'Япония', N'JP')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (112, N'Jersey', N'Джерси', N'JE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (113, N'Jordan', N'Иордания', N'JO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (114, N'Kazachstan', N'Казахстан', N'KZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (115, N'Kenya', N'Кения', N'KE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (116, N'Kirgistan', N'Кыргызстан', N'KG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (117, N'Kiribati', N'Кирибати', N'KI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (118, N'Korea (North)', N'Корея (Северная)', N'KP')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (119, N'Korea (South)', N'Корея (Южная)', N'KR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (120, N'Kosovo', N'Косово', N'  ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (121, N'Kuwait', N'Кувейт', N'KW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (122, N'Laos', N'Лаос', N'LA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (123, N'Latvia', N'Латвия', N'LV')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (124, N'Lebanon', N'Ливан', N'LB')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (125, N'Lesotho', N'Лесото', N'LS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (126, N'Liberia', N'Либерия', N'LR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (127, N'Libya', N'Ливия', N'LY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (128, N'Liechtenstein', N'Лихтенштейн', N'LI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (129, N'Lithuania', N'Литва', N'LT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (130, N'Luxembourg', N'Люксембург', N'LU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (131, N'Macau', N'Аомынь', N'MO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (132, N'Madagascar', N'Мадагаскар', N'MG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (133, N'Malawi', N'Малави', N'MW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (134, N'Malaysia', N'Малайзия', N'MY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (135, N'Maldives', N'Мальдивы', N'MV')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (136, N'Mali', N'Мали', N'ML')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (137, N'Malta', N'Мальта', N'MT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (138, N'Marshall Islands', N'Маршалловы Острова', N'MH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (139, N'Martinique (Fr.)', N'Мартиника', N'MQ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (140, N'Mauritania', N'Мавритания', N'MR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (141, N'Mauritius', N'Маврикий', N'MU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (142, N'Mayotte', N'Майотта', N'YT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (143, N'Mexico', N'Мексика', N'MX')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (144, N'Micronesia', N'Микронезия', N'FM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (145, N'Moldavia', N'Молдова', N'MD')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (146, N'Monaco', N'Монако', N'MC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (147, N'Mongolia', N'Монголия', N'MN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (148, N'Montenegro', N'Черногория', N'ME')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (149, N'Montserrat', N'Монтсеррат', N'MS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (150, N'Morocco', N'Морокко', N'MA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (151, N'Mozambique', N'Мозамбик', N'MZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (152, N'Myanmar', N'Мьянма', N'MM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (153, N'Nagorno-Karabakh Republic', N'Нагорно-Карабахская Республика', N'  ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (154, N'Namibia', N'Намибия', N'NA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (155, N'Nauru', N'Науру', N'NR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (156, N'Nepal', N'Непал', N'NP')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (157, N'Netherland Antilles', N'Антильские Острова', N'AN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (158, N'Netherlands', N'Нидерланды', N'NL')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (159, N'New Caledonia (Fr.)', N'Новая Каледония', N'NC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (160, N'New Zealand', N'Новая Зеландия', N'NZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (161, N'Nicaragua', N'Никарагуа', N'NI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (162, N'Niger', N'Нигер', N'NE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (163, N'Nigeria', N'Нигерия', N'NG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (164, N'Niue', N'Ниуэ', N'NU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (165, N'Norfolk Island', N'Норфолк', N'NF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (166, N'Northern Cyprus', N'Турецкая Республика Северного Кипра', N'NC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (167, N'Northern Mariana Isl.', N'Северные Марианские острова', N'MP')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (168, N'Norway', N'Норвегия', N'NO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (169, N'Oman', N'Оман', N'OM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (170, N'Pakistan', N'Пакистан', N'PK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (171, N'Palau', N'Палау', N'PW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (172, N'Palestine', N'Палестина', N'PS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (173, N'Panama', N'Панама', N'PA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (174, N'Papua New', N'Папуа — Новая Гвинея', N'PG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (175, N'Paraguay', N'Парагвай', N'PY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (176, N'Peru', N'Перу', N'PE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (177, N'Philippines', N'Филиппины', N'PH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (178, N'Pitcairn', N'Питкэрн', N'PN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (179, N'Poland', N'Польша', N'PL')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (180, N'Portugal', N'Португалия', N'PT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (181, N'Puerto Rico', N'Пуэрто-Рико', N'PR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (182, N'Qatar', N'Катар', N'QA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (183, N'Republic of Macedonia', N'Македония', N'MK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (184, N'Republic of Somaliland', N'Сомалиленд', N'  ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (185, N'Republic of the Congo', N'Республика Конго', N'CG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (186, N'Reunion (Fr.)', N'Реюньон', N'RE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (187, N'Romania', N'Румыния', N'RO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (188, N'Russia', N'Россия', N'RU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (189, N'Rwanda', N'Руанда', N'RW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (190, N'Saint Lucia', N'Сент-Люсия', N'LC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (191, N'Samoa', N'Самоа', N'WS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (192, N'San Marino', N'Сан-Марино', N'SM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (193, N'Saudi Arabia', N'Саудовская Аравия', N'SA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (194, N'Senegal', N'Сенегал', N'SN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (195, N'Serbia', N'Сербия', N'RS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (196, N'Seychelles', N'Сейшельские острова', N'SC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (197, N'Sierra Leone', N'Сьерра-Леоне', N'SL')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (198, N'Singapore', N'Сингапур', N'SG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (199, N'Slovak Republic', N'Словакия', N'SK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (200, N'Slovenia', N'Словения', N'SI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (201, N'Solomon Islands', N'Соломоновы Острова', N'SB')
GO
print 'Processed 200 total records'
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (202, N'Somalia', N'Сомали', N'SO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (203, N'South Africa', N'Южно-Африканская Республика', N'ZA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (204, N'South Georgia and the South Sandwich Islands', N'Южная Георгия и Южные Сандвичевы острова', N'GS')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (205, N'South Ossetia', N'Южная Осетия', N'  ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (206, N'Spain', N'Испания', N'ES')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (207, N'Sri Lanka', N'Шри-Ланка', N'LK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (208, N'St. Helena', N'Остров Святой Елены', N'SH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (209, N'St. Pierre & Miquelon', N'Сен-Пьер и Микелон', N'PM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (210, N'St. Tome and Principe', N'Сан-Томе и Принсипи', N'ST')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (211, N'St.Kitts Nevis Anguilla', N'Сент-Киттс и Невис', N'KN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (212, N'St.Vincent & Grenadines', N'Сент-Винсент и Гренадины', N'VC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (213, N'Sudan', N'Судан', N'SD')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (214, N'Suriname', N'Суринам', N'SR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (215, N'Svalbard & Jan Mayen Is', N'Свальбард', N'SJ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (216, N'Swaziland', N'Свазиленд', N'SZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (217, N'Sweden', N'Швеция', N'SE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (218, N'Switzerland', N'Швейцария', N'CH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (219, N'Syria', N'Сирия', N'SY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (220, N'Tadjikistan', N'Таджикистан', N'TJ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (221, N'Taiwan', N'Тайвань', N'TW')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (222, N'Tamil Eelam', N'Тамил-Илам', N'  ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (223, N'Tanzania', N'Танзания', N'TZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (224, N'Thailand', N'Таиланд', N'TH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (225, N'Togo', N'Того', N'TG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (226, N'Tokelau', N'Токелау', N'TK')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (227, N'Tonga', N'Тонга', N'TO')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (228, N'Transnistria', N'Приднестровье', N'  ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (229, N'Trinidad & Tobago', N'Тринидад и Тобаго', N'TT')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (230, N'Tunisia', N'Тунис', N'TN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (231, N'Turkey', N'Турция', N'TR')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (232, N'Turkmenistan', N'Туркменистан', N'TM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (233, N'Turks and Caicos Islands', N'Тёркс и Кайкос', N'TC')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (234, N'Tuvalu', N'Тувалу', N'TV')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (235, N'Uganda', N'Уганда', N'UG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (236, N'Ukraine', N'Украина', N'UA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (237, N'United Arab Emirates', N'Объединенные Арабские Эмираты', N'AE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (238, N'United Kingdom (Great Britain)', N'Великобритания', N'GB')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (239, N'United States', N'Соединенные Штаты Америки', N'US')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (240, N'United States Minor Outlying Islands', N'Внешние малые острова (США)', N'UM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (241, N'Uruguay', N'Уругвай', N'UY')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (242, N'Uzbekistan', N'Узбекистан', N'UZ')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (243, N'Vanuatu', N'Вануату', N'VU')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (244, N'Vatican City State', N'Ватикан', N'VA')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (245, N'Venezuela', N'Венесуэла', N'VE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (246, N'Vietnam', N'Вьетнам', N'VN')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (247, N'Virgin Islands (British)', N'Виргинские Острова (Британские)', N'VG')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (248, N'Virgin Islands (US)', N'Виргинские Острова (США)', N'VI')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (249, N'Wallis & Futuna Islands', N'Острова Уоллис и Футуна', N'WF')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (250, N'Western Sahara', N'Западная Сахара', N'EH')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (251, N'Yemen', N'Йемен', N'YE')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (252, N'Zambia', N'Замбия', N'ZM')
INSERT [dbo].[Country] ([Id], [Name_En], [Name_Ru], [Code]) VALUES (253, N'Zimbabwe', N'Зимбабве', N'ZW')
SET IDENTITY_INSERT [dbo].[Country] OFF
/****** Object:  Table [dbo].[Carrier]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Carrier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Carrier_Name] ON [dbo].[Carrier] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Carrier] ON
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (1, N'Carrier 0')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (2, N'Carrier 1')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (3, N'Carrier 2')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (4, N'Carrier 3')
INSERT [dbo].[Carrier] ([Id], [Name]) VALUES (5, N'Carrier 4')
SET IDENTITY_INSERT [dbo].[Carrier] OFF
/****** Object:  Table [dbo].[User]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](320) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[TwoLetterISOLanguageName] [char](2) NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Login] ON [dbo].[User] 
(
	[Login] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (1, N'Admin', 0xD620502F233C7DE84C19612F3BFB3C9AF4F18485B8E4D7F3A0C82CBE8E75B0A3, 0x811BE7CE29ABCC4CABF9628D1FEBF5FB, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (2, N'Brocker', 0x98920C44039CADB7F0D7A9A784AF89E715AE3B956B559D758C0B6A4F41D27A0E, 0xCD44B21CC1C2D94FAE61E35D83B38BC1, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (3, N'Forwarder', 0xB9CBD42D8C848D09917995045022ECADF7789301E24DD84F6AD05D574368A056, 0x911C9CB1AE955341A11C4E6EB4A4013B, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (4, N'Sender', 0x72CCEE115CA2264BAF818D81ACC90AFE1343CEC1C21B98B6A31A6DF0EBDFF0F5, 0x929F9FA36D67C242A4113CBE6A49952A, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (5, N'1', 0xF0EA44E7CEF6B282139A8EF5A139ED46B476D93955965B903F03895E8EB446EA, 0x633F51634E9D5341A4AF127F099E6084, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (6, N'c1', 0xA12AA505073C2A2C12FA2554EA633349EFDCCCC14EB58F3BDEE08B99B4F0BF47, 0x27F15ADF0E71134B9DDD8570C7B4558E, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (7, N'c2', 0x3372842A198B88A98E973525FC4AA9AC0F84BC2947124D41AF78B3BFAB95D413, 0xFF8D3301277CEB4DB0F7FE2DA41DD0DE, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (8, N'3', 0x15680C591A987BF6F531C7F646713927F0DECBA810D54C5004C3D1C5BDF880FC, 0x62725047636AC547B80C2E5385BAB143, N'ru')
INSERT [dbo].[User] ([Id], [Login], [PasswordHash], [PasswordSalt], [TwoLetterISOLanguageName]) VALUES (9, N'c4', 0x02DD1CD2AF54FA92AA9A51A584497BF21CECD2B9EC15A1F369C5F3731E85E209, 0xB1AFC47E2F09104D9050A8607CCA2590, N'ru')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Mark]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mark](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Mark] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Mark_Name] ON [dbo].[Mark] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Mark] ON
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (22, N'Mark 08ae41e7-fba0-42f8-b5ad-fa74f7612227')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (3, N'Mark 0e6aac49-8d7c-4650-9f1b-c3338273181b')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (4, N'Mark 262e6b3b-a058-49c0-9d4c-e2550d27ed5c')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (19, N'Mark 30711a03-3616-4c4d-af4d-d18c03f63f3f')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (21, N'Mark 32917d96-a37c-4c80-9cd2-153a7460e2d2')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (16, N'Mark 38a64386-818c-4ff7-8428-946a66fd660d')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (13, N'Mark 3a70eff7-7be1-4669-ac81-d3383ad1cda1')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (1, N'Mark 4debc9fd-a31c-42ac-9054-519380c138e9')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (5, N'Mark 5e45e9dd-c8c8-43aa-b6bd-5225309b61b5')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (17, N'Mark 72d66484-1521-4e96-a381-36dc4e4f359d')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (2, N'Mark 7b0a94e9-1f24-4a3d-bd97-7c7c87e57691')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (7, N'Mark 7cb8199d-b2aa-4a6f-82b7-8b262244f1ed')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (14, N'Mark 85d794c8-c140-4fa8-bfc4-cac11dce58b4')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (12, N'Mark 87c936dc-8652-44da-8feb-33c5cb448e6d')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (24, N'Mark 8daedcf1-4ddb-422a-8179-c5da06c3542b')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (10, N'Mark 8f930c98-55a9-48e2-abec-e68b092a3cca')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (18, N'Mark 9df3dabe-1a37-4575-8126-ec736ae5135f')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (8, N'Mark 9ec8a528-dcf1-41bd-8113-62ce1cdcb908')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (11, N'Mark a67d11a0-7adf-4685-8f76-e5e21738393a')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (15, N'Mark c2ee8d27-5394-4066-951d-5caef8c6e135')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (23, N'Mark d5a8256b-002b-4cf4-9c1d-678f7c0aafe6')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (20, N'Mark e8c7cd4e-aba5-4bc9-88a5-38810b0d0940')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (25, N'Mark ea0d416d-c344-4724-85ae-cd095222d6b9')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (6, N'Mark eafb9da9-d203-468b-8ef1-33e608096e3f')
INSERT [dbo].[Mark] ([Id], [Name]) VALUES (9, N'Mark f38acfad-9935-4c3e-b6a2-4617da6deadb')
SET IDENTITY_INSERT [dbo].[Mark] OFF
/****** Object:  Table [dbo].[VisibleState]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisibleState](
	[RoleId] [int] NOT NULL,
	[StateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.VisibleState] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_VisibleState_RoleId] ON [dbo].[VisibleState] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_VisibleState_StateId] ON [dbo].[VisibleState] 
(
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 11)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (2, 14)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (3, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (3, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (3, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 2)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 3)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 4)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 6)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 7)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 8)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 9)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 10)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 12)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 13)
INSERT [dbo].[VisibleState] ([RoleId], [StateId]) VALUES (5, 14)
/****** Object:  Table [dbo].[Admin]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Admin] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON
INSERT [dbo].[Admin] ([Id], [UserId], [Name], [Email]) VALUES (1, 1, N'Admin', N'Admin@timez.org')
SET IDENTITY_INSERT [dbo].[Admin] OFF
/****** Object:  Table [dbo].[Forwarder]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forwarder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Forwarder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Forwarder] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Forwarder] ON
INSERT [dbo].[Forwarder] ([Id], [UserId], [Name], [Email]) VALUES (1, 3, N'Forwarder', N'Forwarder@timez.org')
SET IDENTITY_INSERT [dbo].[Forwarder] OFF
/****** Object:  Table [dbo].[Transit]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[RecipientName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[WarehouseWorkingTime] [nvarchar](max) NULL,
	[MethodOfTransitId] [int] NOT NULL,
	[DeliveryTypeId] [int] NOT NULL,
	[CarrierId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Transit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_CarrierId] ON [dbo].[Transit] 
(
	[CarrierId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Transit] ON
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (1, N'City 0', N'Address 0', N'Recipient 0', N'Phone 0', NULL, 0, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (2, N'City 1', N'Address 1', N'Recipient 1', N'Phone 1', NULL, 1, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (3, N'City 2', N'Address 2', N'Recipient 2', N'Phone 2', NULL, 2, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (4, N'City 3', N'Address 3', N'Recipient 3', N'Phone 3', NULL, 0, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (5, N'City 4', N'Address 4', N'Recipient 4', N'Phone 4', NULL, 1, 0, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (6, N'City 0', N'Address 0', N'RecipientName 0', N'Phone 0', NULL, 0, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (7, N'City 1', N'Address 1', N'RecipientName 1', N'Phone 1', NULL, 1, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (8, N'City 2', N'Address 2', N'RecipientName 2', N'Phone 2', NULL, 2, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (9, N'City 3', N'Address 3', N'RecipientName 3', N'Phone 3', NULL, 0, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (10, N'City 4', N'Address 4', N'RecipientName 4', N'Phone 4', NULL, 1, 0, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (11, N'City 5', N'Address 5', N'RecipientName 5', N'Phone 5', NULL, 2, 1, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (12, N'City 6', N'Address 6', N'RecipientName 6', N'Phone 6', NULL, 0, 0, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (13, N'City 7', N'Address 7', N'RecipientName 7', N'Phone 7', NULL, 1, 1, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (14, N'City 8', N'Address 8', N'RecipientName 8', N'Phone 8', NULL, 2, 0, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (15, N'City 9', N'Address 9', N'RecipientName 9', N'Phone 9', NULL, 0, 1, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (16, N'City 10', N'Address 10', N'RecipientName 10', N'Phone 10', NULL, 1, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (17, N'City 11', N'Address 11', N'RecipientName 11', N'Phone 11', NULL, 2, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (18, N'City 12', N'Address 12', N'RecipientName 12', N'Phone 12', NULL, 0, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (19, N'City 13', N'Address 13', N'RecipientName 13', N'Phone 13', NULL, 1, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (20, N'City 14', N'Address 14', N'RecipientName 14', N'Phone 14', NULL, 2, 0, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (21, N'City 15', N'Address 15', N'RecipientName 15', N'Phone 15', NULL, 0, 1, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (22, N'City 16', N'Address 16', N'RecipientName 16', N'Phone 16', NULL, 1, 0, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (23, N'City 17', N'Address 17', N'RecipientName 17', N'Phone 17', NULL, 2, 1, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (24, N'City 18', N'Address 18', N'RecipientName 18', N'Phone 18', NULL, 0, 0, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (25, N'City 19', N'Address 19', N'RecipientName 19', N'Phone 19', NULL, 1, 1, 5)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (26, N'City 20', N'Address 20', N'RecipientName 20', N'Phone 20', NULL, 2, 0, 1)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (27, N'City 21', N'Address 21', N'RecipientName 21', N'Phone 21', NULL, 0, 1, 2)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (28, N'City 22', N'Address 22', N'RecipientName 22', N'Phone 22', NULL, 1, 0, 3)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (29, N'City 23', N'Address 23', N'RecipientName 23', N'Phone 23', NULL, 2, 1, 4)
INSERT [dbo].[Transit] ([Id], [City], [Address], [RecipientName], [Phone], [WarehouseWorkingTime], [MethodOfTransitId], [DeliveryTypeId], [CarrierId]) VALUES (30, N'City 24', N'Address 24', N'RecipientName 24', N'Phone 24', NULL, 0, 0, 5)
SET IDENTITY_INSERT [dbo].[Transit] OFF
/****** Object:  Table [dbo].[StateLocalization]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StateLocalization](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[TwoLetterISOLanguageName] [char](2) NOT NULL,
	[StateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.StateLocalization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_StateLocalization_StateId] ON [dbo].[StateLocalization] 
(
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[StateLocalization] ON
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (1, N'Новая заявка', N'ru', 1)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (2, N'Nuovo', N'it', 1)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (3, N'New', N'en', 1)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (4, N'Фабрика ждет оплату', N'ru', 2)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (5, N'Fabbrica attende il pagamento', N'it', 2)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (6, N'Factory awaits payment', N'en', 2)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (7, N'Груз не готов', N'ru', 3)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (8, N'Cargo non è pronto', N'it', 3)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (9, N'Cargo is not ready', N'en', 3)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (10, N'Груз забран на фабрике', N'ru', 4)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (11, N'Cargo è ritirata in fabbrica', N'it', 4)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (12, N'Cargo is withdrawn at the factory', N'en', 4)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (13, N'Груз на складе', N'ru', 6)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (14, N'Cargo in magazzino', N'it', 6)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (15, N'Cargo in stock', N'en', 6)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (16, N'Груз вылетел', N'ru', 7)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (17, N'Cargo volò', N'it', 7)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (18, N'Cargo flew', N'en', 7)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (19, N'Груз на таможне', N'ru', 8)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (20, N'Cargo alla dogana', N'it', 8)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (21, N'Cargo at customs', N'en', 8)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (22, N'Груз выпущен', N'ru', 9)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (23, N'Sdoganate cargo', N'it', 9)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (24, N'Customs cleared cargo', N'en', 9)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (25, N'Фабрика отправила груз', N'ru', 10)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (26, N'Carico di fabbrica inviato', N'it', 10)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (27, N'Factory cargo sent', N'en', 10)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (28, N'Груз получен', N'ru', 11)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (29, N'Cargo ha ricevuto', N'it', 11)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (30, N'Cargo received', N'en', 11)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (31, N'На транзите', N'ru', 12)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (32, N'sul transito', N'it', 12)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (33, N'On transit', N'en', 12)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (34, N'Фабрика не отвечает', N'ru', 13)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (35, N'Fabbrica non risponde', N'it', 13)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (36, N'Factory does not respond', N'en', 13)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (37, N'Груз на стопе', N'ru', 14)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (38, N'Stop', N'it', 14)
INSERT [dbo].[StateLocalization] ([Id], [Name], [TwoLetterISOLanguageName], [StateId]) VALUES (39, N'Stop', N'en', 14)
SET IDENTITY_INSERT [dbo].[StateLocalization] OFF
/****** Object:  Table [dbo].[Brocker]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brocker](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Brocker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Brocker] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Brocker] ON
INSERT [dbo].[Brocker] ([Id], [UserId], [Name], [Email]) VALUES (1, 2, N'Brocker', N'Brocker@timez.org')
SET IDENTITY_INSERT [dbo].[Brocker] OFF
/****** Object:  Table [dbo].[AvailableState]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvailableState](
	[RoleId] [int] NOT NULL,
	[StateId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.AvailableState] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AvailableState_RoleId] ON [dbo].[AvailableState] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AvailableState_StateId] ON [dbo].[AvailableState] 
(
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 1)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 2)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 3)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 4)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 6)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 10)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 11)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 12)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 13)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (1, 14)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 1)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 2)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 3)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 4)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 6)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 10)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (2, 13)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (4, 9)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (4, 12)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (4, 14)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (5, 1)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (5, 11)
INSERT [dbo].[AvailableState] ([RoleId], [StateId]) VALUES (5, 12)
/****** Object:  Table [dbo].[Sender]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sender](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Sender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Sender] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Sender] ON
INSERT [dbo].[Sender] ([Id], [UserId], [Name], [Email]) VALUES (1, 4, N'Sender', N'Sender@timez.org')
SET IDENTITY_INSERT [dbo].[Sender] OFF
/****** Object:  Table [dbo].[Reference]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reference](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationTimestamp] [datetimeoffset](7) NOT NULL,
	[Bill] [nvarchar](320) NOT NULL,
	[ArrivalAirport] [nvarchar](max) NOT NULL,
	[DepartureAirport] [nvarchar](max) NOT NULL,
	[DateOfDeparture] [datetimeoffset](7) NOT NULL,
	[DateOfArrival] [datetimeoffset](7) NOT NULL,
	[BrockerId] [bigint] NOT NULL,
	[GTD] [nvarchar](320) NULL,
	[GTDFileData] [varbinary](max) NULL,
	[GTDFileName] [nvarchar](320) NULL,
	[GTDAdditionalFileData] [varbinary](max) NULL,
	[GTDAdditionalFileName] [nvarchar](320) NULL,
	[PackingFileData] [varbinary](max) NULL,
	[PackingFileName] [nvarchar](320) NULL,
	[InvoiceFileData] [varbinary](max) NULL,
	[InvoiceFileName] [nvarchar](320) NULL,
	[AWBFileData] [varbinary](max) NULL,
	[AWBFileName] [nvarchar](320) NULL,
	[StateId] [bigint] NOT NULL,
	[StateChangeTimestamp] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.Reference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_BrockerId] ON [dbo].[Reference] 
(
	[BrockerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Reference_Bill] ON [dbo].[Reference] 
(
	[Bill] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Reference] ON
INSERT [dbo].[Reference] ([Id], [CreationTimestamp], [Bill], [ArrivalAirport], [DepartureAirport], [DateOfDeparture], [DateOfArrival], [BrockerId], [GTD], [GTDFileData], [GTDFileName], [GTDAdditionalFileData], [GTDAdditionalFileName], [PackingFileData], [PackingFileName], [InvoiceFileData], [InvoiceFileName], [AWBFileData], [AWBFileName], [StateId], [StateChangeTimestamp]) VALUES (1, CAST(0x0710E85B8C421D370B0000 AS DateTimeOffset), N'53b1850a-d7e4-4f94-8412-04b288836f39', N'ArrivalAirport', N'DepartureAirport', CAST(0x0710E85B8C4222370B0000 AS DateTimeOffset), CAST(0x0710E85B8C4227370B0000 AS DateTimeOffset), 1, N'89f5be95-dfa9-48da-a291-50f17a741de5', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 7, CAST(0x0700BDFFBC8F4E370B0000 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[Reference] OFF
/****** Object:  Table [dbo].[Client]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[Nic] [nvarchar](max) NOT NULL,
	[LegalEntity] [nvarchar](max) NOT NULL,
	[Contacts] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[INN] [nvarchar](max) NULL,
	[KPP] [nvarchar](max) NULL,
	[OGRN] [nvarchar](max) NULL,
	[Bank] [nvarchar](max) NULL,
	[BIC] [nvarchar](max) NULL,
	[LegalAddress] [nvarchar](max) NULL,
	[MailingAddress] [nvarchar](max) NULL,
	[RS] [nvarchar](max) NULL,
	[KS] [nvarchar](max) NULL,
	[TransitId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TransitId] ON [dbo].[Client] 
(
	[TransitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Client] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Client] ON
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (1, 5, N'u1@timez.org', N'Nic c50efc4d-4b52-4833-aeb5-9a59ff796029', N'LegalEntity 0', N'Contact 88fb1770-f405-4ca2-ae2f-251a9bc7593b', NULL, N'INN e18cbb8f-13f4-4f58-b5ed-81a45b9e8234', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (2, 6, N'u2@timez.org', N'Nic f9ae5e8b-e1e4-4aec-a86f-62dae76edc9e', N'LegalEntity 1', N'Contact 85eb1690-45fe-4b87-a0d1-3dfb2c5c1b43', NULL, N'INN afc62fc8-6088-49d1-9807-3d960e5f6aff', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (3, 7, N'u3@timez.org', N'Nic 8b1102be-f302-425c-8333-89f1e0f877ab', N'LegalEntity 2', N'Contact 970686ea-499b-4499-90b6-7225df9bf446', NULL, N'INN ce00c59c-abe6-4ffe-b2c7-83585e2460e9', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (4, 8, N'u4@timez.org', N'Nic a8efe6e4-333a-4b7c-b8d5-f382f6b42432', N'LegalEntity 3', N'Contact ee75723c-76ae-4e26-b94e-26a6312b41fb', NULL, N'INN e8d7859f-cfdf-407d-90a0-35c4190e0e8e', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Client] ([Id], [UserId], [Email], [Nic], [LegalEntity], [Contacts], [Phone], [INN], [KPP], [OGRN], [Bank], [BIC], [LegalAddress], [MailingAddress], [RS], [KS], [TransitId]) VALUES (5, 9, N'u5@timez.org', N'Nic e53b6605-66f9-464c-bf6f-7d0a8f8b5a93', N'LegalEntity 4', N'Contact be0004f2-df24-415e-a626-96d439f70ee1', NULL, N'INN c0ae51d5-5438-40ac-866e-13abc2a7bc23', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5)
SET IDENTITY_INSERT [dbo].[Client] OFF
/****** Object:  Table [dbo].[Application]    Script Date: 07/06/2013 17:11:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Application](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationTimestamp] [datetimeoffset](7) NOT NULL,
	[Invoice] [nvarchar](max) NOT NULL,
	[InvoiceFileData] [varbinary](max) NULL,
	[InvoiceFileName] [nvarchar](max) NULL,
	[SwiftFileData] [varbinary](max) NULL,
	[SwiftFileName] [nvarchar](max) NULL,
	[DeliveryBillFileData] [varbinary](max) NULL,
	[DeliveryBillFileName] [nvarchar](max) NULL,
	[Torg12FileData] [varbinary](max) NULL,
	[Torg12FileName] [nvarchar](max) NULL,
	[CPFileData] [varbinary](max) NULL,
	[CPFileName] [nvarchar](max) NULL,
	[PackingFileData] [varbinary](max) NULL,
	[PackingFileName] [nvarchar](320) NULL,
	[Characteristic] [nvarchar](max) NULL,
	[AddressLoad] [nvarchar](max) NULL,
	[WarehouseWorkingTime] [nvarchar](max) NULL,
	[TransitReference] [nvarchar](max) NULL,
	[Gross] [real] NULL,
	[Count] [int] NULL,
	[Volume] [real] NOT NULL,
	[TermsOfDelivery] [nvarchar](max) NOT NULL,
	[MethodOfDeliveryId] [int] NOT NULL,
	[DateOfCargoReceipt] [datetimeoffset](7) NULL,
	[DateInStock] [datetimeoffset](7) NULL,
	[StateChangeTimestamp] [datetimeoffset](7) NOT NULL,
	[StateId] [bigint] NOT NULL,
	[Value] [money] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[TransitId] [bigint] NOT NULL,
	[CountryId] [bigint] NOT NULL,
	[ReferenceId] [bigint] NULL,
	[FactoryName] [nvarchar](320) NOT NULL,
	[FactoryPhone] [nvarchar](max) NOT NULL,
	[FactoryContact] [nvarchar](max) NULL,
	[FactoryEmail] [nvarchar](320) NOT NULL,
	[MarkName] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_dbo.Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Application_TransitId] ON [dbo].[Application] 
(
	[TransitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ClientId] ON [dbo].[Application] 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ReferenceId] ON [dbo].[Application] 
(
	[ReferenceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_StateId] ON [dbo].[Application] 
(
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Application] ON
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (1, CAST(0x073F7B288C421D370B0000 AS DateTimeOffset), N'Invoice 4e85d577-2a7e-4d8d-a3a1-906429e7e844', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, N'75800114-930b-44ab-b541-06099c8b0f8f', 0, NULL, NULL, CAST(0x07D4DA298C421D370B0000 AS DateTimeOffset), 1, 1.0000, 2, 1, 6, 1, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (2, CAST(0x07E877338C421C370B0000 AS DateTimeOffset), N'Invoice 940b08db-668a-4a9f-883a-fa709d822f40', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, N'484bc0d9-ded0-423d-af02-8e24264a3b72', 1, NULL, NULL, CAST(0x0755D3368C421C370B0000 AS DateTimeOffset), 2, 2.0000, 2, 2, 7, 2, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (3, CAST(0x07C9E4378C421B370B0000 AS DateTimeOffset), N'Invoice f6a0701a-5f6d-408b-b69f-b2261c6ec663', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, 2, 2, N'6a66428c-f17f-4a5a-905a-8fcbbd204578', 0, NULL, NULL, CAST(0x070B81388C421B370B0000 AS DateTimeOffset), 3, 3.0000, 2, 3, 8, 3, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (4, CAST(0x076F6B398C421A370B0000 AS DateTimeOffset), N'Invoice 453af235-9f56-41e6-ab90-e6aa60804d41', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, 3, 3, N'35e5816a-f226-4e87-999f-d651234ffa9a', 1, NULL, NULL, CAST(0x07A0E0398C421A370B0000 AS DateTimeOffset), 4, 4.0000, 2, 4, 9, 4, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (5, CAST(0x0704CB3A8C4219370B0000 AS DateTimeOffset), N'Invoice 29e7dd0f-b7f0-4809-8ebe-07a2b2e8029d', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, 4, 4, N'729e73af-c94c-485f-a7c2-e94f9f3bb307', 0, NULL, NULL, CAST(0x0746673B8C4219370B0000 AS DateTimeOffset), 13, 5.0000, 2, 5, 10, 5, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (6, CAST(0x07992A3C8C4218370B0000 AS DateTimeOffset), N'Invoice c8c1df12-088e-42be-969b-f9bdb8e7da89', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 0, 5, N'10d70585-19bf-42e6-874a-34b6c5c34262', 1, NULL, NULL, CAST(0x07AA513C8C421D370B0000 AS DateTimeOffset), 6, 6.0000, 2, 1, 11, 6, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (7, CAST(0x0770263E8C4217370B0000 AS DateTimeOffset), N'Invoice 0f91fe10-348c-46ac-b804-3a4a403e1056', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, 1, 6, N'92a6e721-e5fa-49e9-85f9-02bdc6e45107', 0, NULL, NULL, CAST(0x0770263E8C421C370B0000 AS DateTimeOffset), 7, 7.0000, 2, 2, 12, 7, 1, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (8, CAST(0x07C3E93E8C4216370B0000 AS DateTimeOffset), N'Invoice 66853302-4b64-42fc-811d-efa3fb73f004', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 7, 2, 7, N'50d44102-f61c-4441-861f-24ce51881259', 1, NULL, NULL, CAST(0x07D4103F8C421B370B0000 AS DateTimeOffset), 8, 8.0000, 2, 3, 13, 8, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (9, CAST(0x0716AD3F8C4215370B0000 AS DateTimeOffset), N'Invoice 819db2c1-d37b-4632-9083-8f1f5b1795eb', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8, 3, 8, N'29c7c794-1e9f-496e-83ae-165986570ea9', 0, NULL, NULL, CAST(0x0727D43F8C421A370B0000 AS DateTimeOffset), 9, 9.0000, 2, 4, 14, 9, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (10, CAST(0x076970408C4214370B0000 AS DateTimeOffset), N'Invoice 2310ce59-20eb-45ce-b24c-d3660340716a', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 9, 4, 9, N'd4dbb14a-72e3-4533-8def-6f7b0bf8926f', 1, NULL, NULL, CAST(0x077A97408C4219370B0000 AS DateTimeOffset), 10, 10.0000, 2, 5, 15, 10, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (11, CAST(0x07BC33418C421D370B0000 AS DateTimeOffset), N'Invoice a2a9cd90-bf8d-41c7-8726-3013df5f80ab', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 10, N'941d1442-8c43-4e68-ae20-1c1f0b7b4170', 0, NULL, NULL, CAST(0x07CC5A418C421D370B0000 AS DateTimeOffset), 11, 11.0000, 2, 1, 16, 11, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
INSERT [dbo].[Application] ([Id], [CreationTimestamp], [Invoice], [InvoiceFileData], [InvoiceFileName], [SwiftFileData], [SwiftFileName], [DeliveryBillFileData], [DeliveryBillFileName], [Torg12FileData], [Torg12FileName], [CPFileData], [CPFileName], [PackingFileData], [PackingFileName], [Characteristic], [AddressLoad], [WarehouseWorkingTime], [TransitReference], [Gross], [Count], [Volume], [TermsOfDelivery], [MethodOfDeliveryId], [DateOfCargoReceipt], [DateInStock], [StateChangeTimestamp], [StateId], [Value], [CurrencyId], [ClientId], [TransitId], [CountryId], [ReferenceId], [FactoryName], [FactoryPhone], [FactoryContact], [FactoryEmail], [MarkName]) VALUES (12, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), N'Invoice b8c79b06-bb81-4a53-a1ef-cf8dc732dfe0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 11, N'0ad4f97b-e9d2-484a-a3bf-037176dd0b0e', 1, NULL, NULL, CAST(0x070FF7418C421C370B0000 AS DateTimeOffset), 12, 12.0000, 2, 2, 17, 12, NULL, N'f1', N'f1', NULL, N'f1@mail.ru', N'm1')
SET IDENTITY_INSERT [dbo].[Application] OFF
/****** Object:  Default [DF_Application_CreationTimestamp]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Application] ADD  CONSTRAINT [DF_Application_CreationTimestamp]  DEFAULT (getdate()) FOR [CreationTimestamp]
GO
/****** Object:  Default [DF_ReferenceCreationTimestamp]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Reference] ADD  CONSTRAINT [DF_ReferenceCreationTimestamp]  DEFAULT (getdate()) FOR [CreationTimestamp]
GO
/****** Object:  Default [DF__Reference__State__1FCDBCEB]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Reference] ADD  DEFAULT (getdate()) FOR [StateChangeTimestamp]
GO
/****** Object:  Default [DF__StateLoca__TwoLe__20C1E124]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[StateLocalization] ADD  DEFAULT ('ru') FOR [TwoLetterISOLanguageName]
GO
/****** Object:  Default [DF__User__TwoLetterI__21B6055D]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[User] ADD  DEFAULT ('ru') FOR [TwoLetterISOLanguageName]
GO
/****** Object:  ForeignKey [FK_dbo.Admin_dbo.User_UserId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Admin_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_dbo.Admin_dbo.User_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.Application_dbo.Client_ClientId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.Client_ClientId]
GO
/****** Object:  ForeignKey [FK_dbo.Application_dbo.Country_CountryId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.Country_CountryId]
GO
/****** Object:  ForeignKey [FK_dbo.Application_dbo.Reference_ReferenceId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.Reference_ReferenceId] FOREIGN KEY([ReferenceId])
REFERENCES [dbo].[Reference] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.Reference_ReferenceId]
GO
/****** Object:  ForeignKey [FK_dbo.Application_dbo.State_StateId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.State_StateId]
GO
/****** Object:  ForeignKey [FK_dbo.Application_dbo.Transit_TransitId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId] FOREIGN KEY([TransitId])
REFERENCES [dbo].[Transit] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_dbo.Application_dbo.Transit_TransitId]
GO
/****** Object:  ForeignKey [FK_dbo.AvailableState_dbo.State_StateId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[AvailableState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AvailableState_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AvailableState] CHECK CONSTRAINT [FK_dbo.AvailableState_dbo.State_StateId]
GO
/****** Object:  ForeignKey [FK_dbo.Brocker_dbo.User_UserId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Brocker]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Brocker_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Brocker] CHECK CONSTRAINT [FK_dbo.Brocker_dbo.User_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.Client_dbo.Transit_Transit_Id]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Client_dbo.Transit_Transit_Id] FOREIGN KEY([TransitId])
REFERENCES [dbo].[Transit] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_dbo.Client_dbo.Transit_Transit_Id]
GO
/****** Object:  ForeignKey [FK_dbo.Client_dbo.User_UserId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Client_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_dbo.Client_dbo.User_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.Forwarder_dbo.User_UserId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Forwarder]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forwarder] CHECK CONSTRAINT [FK_dbo.Forwarder_dbo.User_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.Reference_dbo.Brocker_BrockerId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reference_dbo.Brocker_BrockerId] FOREIGN KEY([BrockerId])
REFERENCES [dbo].[Brocker] ([Id])
GO
ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_dbo.Reference_dbo.Brocker_BrockerId]
GO
/****** Object:  ForeignKey [FK_dbo.Reference_dbo.State_StateId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reference_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_dbo.Reference_dbo.State_StateId]
GO
/****** Object:  ForeignKey [FK_dbo.Sender_dbo.User_UserId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Sender]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sender_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sender] CHECK CONSTRAINT [FK_dbo.Sender_dbo.User_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.StateLocalization_dbo.State_State_Id]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[StateLocalization]  WITH CHECK ADD  CONSTRAINT [FK_dbo.StateLocalization_dbo.State_State_Id] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StateLocalization] CHECK CONSTRAINT [FK_dbo.StateLocalization_dbo.State_State_Id]
GO
/****** Object:  ForeignKey [FK_dbo.Transit_dbo.Carrier_Carrier_Id]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[Transit]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transit_dbo.Carrier_Carrier_Id] FOREIGN KEY([CarrierId])
REFERENCES [dbo].[Carrier] ([Id])
GO
ALTER TABLE [dbo].[Transit] CHECK CONSTRAINT [FK_dbo.Transit_dbo.Carrier_Carrier_Id]
GO
/****** Object:  ForeignKey [FK_dbo.VisibleState_dbo.State_StateId]    Script Date: 07/06/2013 17:11:15 ******/
ALTER TABLE [dbo].[VisibleState]  WITH CHECK ADD  CONSTRAINT [FK_dbo.VisibleState_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VisibleState] CHECK CONSTRAINT [FK_dbo.VisibleState_dbo.State_StateId]
GO
