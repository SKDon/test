USE [$(DatabaseName)]
GO

/****** Object:  Table [dbo].[ClientContract]    Script Date: 10/31/2013 21:54:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientContract](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UpdateTimestamp] [datetimeoffset](7) NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[Data] [varbinary](max) NOT NULL,
	[Name] [nvarchar](320) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ClientContract_ClientId] ON [dbo].[ClientContract] 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ClientContract_Merge]    Script Date: 10/31/2013 21:54:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClientContract_Merge]
	@ClientId BIGINT,
	@Name NVARCHAR(320),
	@Data VARBINARY(MAX)
AS
	SET NOCOUNT ON;

	MERGE	[dbo].[ClientContract] AS target

	USING	(VALUES (@ClientId, @Name, @Data))
			AS source ([ClientId], [Name], [Data])

		ON	target.[ClientId] = source.[ClientId]

	WHEN MATCHED AND source.[Name] IS NULL THEN
		DELETE

	WHEN MATCHED THEN
		UPDATE SET [Name] = Source.[Name], [Data] = source.[Data]

	WHEN NOT MATCHED THEN
		INSERT ([ClientId], [Name], [Data]) 
		VALUES (source.[ClientId], source.[Name], source.[Data]);
GO
/****** Object:  StoredProcedure [dbo].[ClientContract_GetFileName]    Script Date: 10/31/2013 21:54:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClientContract_GetFileName]
	@ClientId BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP 1 c.[Name]
	FROM	[dbo].[ClientContract] c
	WHERE	c.[ClientId] = @ClientId
GO
/****** Object:  StoredProcedure [dbo].[ClientContract_Get]    Script Date: 10/31/2013 21:54:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClientContract_Get]
	@ClientId BIGINT
AS
	SET NOCOUNT ON;

	SELECT	TOP 1 c.[Name], c.[Data]
	FROM	[dbo].[ClientContract] c
	WHERE	c.[ClientId] = @ClientId
GO
/****** Object:  Default [DF__ClientCon__Updat__014935CB]    Script Date: 10/31/2013 21:54:35 ******/
ALTER TABLE [dbo].[ClientContract] ADD  DEFAULT (getutcdate()) FOR [UpdateTimestamp]
GO