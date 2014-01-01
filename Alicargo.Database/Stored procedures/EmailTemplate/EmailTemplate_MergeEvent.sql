CREATE PROCEDURE [dbo].[EmailTemplate_MergeEvent]
	@EventTypeId INT,
	@Subject NVARCHAR (MAX),
	@Body NVARCHAR (MAX),
	@IsBodyHtml BIT,
	@EnableEmailSend BIT,
	@TwoLetterISOLanguageName CHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TemplateId BIGINT;

	SELECT TOP(1) @TemplateId = [EmailTemplateId]
	FROM  [dbo].[EventEmailTemplate]
	WHERE [EventTypeId] = @EventTypeId

	IF @TemplateId IS NOT NULL BEGIN
		BEGIN TRAN
			MERGE [dbo].[EmailTemplateLocalization] AS target
			USING (SELECT @Subject, @Body, @IsBodyHtml, @TemplateId, @TwoLetterISOLanguageName) AS source 
						([Subject], [Body], [IsBodyHtml], [EmailTemplateId], [TwoLetterISOLanguageName])
				ON (target.[EmailTemplateId] = source.[EmailTemplateId] 
					AND target.[TwoLetterISOLanguageName] = source.[TwoLetterISOLanguageName])
			WHEN MATCHED THEN
				UPDATE
				SET [Subject] = source.[Subject],
					[Body] = source.[Body],
					[IsBodyHtml] = source.[IsBodyHtml]
			WHEN NOT MATCHED THEN
				INSERT ([EmailTemplateId], [Subject], [Body], [IsBodyHtml], [TwoLetterISOLanguageName])
				VALUES (source.[EmailTemplateId], source.[Subject], source.[Body], source.[IsBodyHtml], source.[TwoLetterISOLanguageName]);

			UPDATE TOP(1) [dbo].[EventEmailTemplate]
			SET [EnableEmailSend] = @EnableEmailSend
			WHERE [EventTypeId] = @EventTypeId AND [EmailTemplateId] = @TemplateId
		COMMIT
	END
	ELSE BEGIN
		BEGIN TRAN
			INSERT [dbo].[EmailTemplate] DEFAULT VALUES
			SET @TemplateId = SCOPE_IDENTITY()

			INSERT [dbo].[EmailTemplateLocalization] 
					([EmailTemplateId], [Subject], [Body], [IsBodyHtml], [TwoLetterISOLanguageName])
			VALUES (@TemplateId, @Subject, @Body, @IsBodyHtml, @TwoLetterISOLanguageName)

			INSERT [dbo].[EventEmailTemplate] 
					([EmailTemplateId], [EnableEmailSend], [EventTypeId])
			VALUES (@TemplateId, @EnableEmailSend, @EventTypeId);
		COMMIT
	END

END
GO