CREATE PROCEDURE [dbo].[EmailTemplate_MergeState]
	@StateId BIGINT,
	@Subject NVARCHAR (MAX),
	@Body NVARCHAR (MAX),
	@IsBodyHtml BIT,
	@EnableEmailSend BIT,
	@TwoLetterISOLanguageName CHAR(2)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TemplateId BIGINT;

	SELECT TOP(1) @TemplateId = st.[EmailTemplateId]
	FROM  [dbo].[StateEmailTemplate] st
	WHERE st.[StateId] = @StateId

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

			UPDATE TOP(1) [dbo].[StateEmailTemplate]
			SET [EnableEmailSend] = @EnableEmailSend
			WHERE [StateId] = @StateId AND [EmailTemplateId] = @TemplateId
		COMMIT
	END
	ELSE BEGIN
		BEGIN TRAN
			INSERT [dbo].[EmailTemplate] DEFAULT VALUES
			SET @TemplateId = SCOPE_IDENTITY()

			INSERT [dbo].[EmailTemplateLocalization] 
					([EmailTemplateId], [Subject], [Body], [IsBodyHtml], [TwoLetterISOLanguageName])
			VALUES (@TemplateId, @Subject, @Body, @IsBodyHtml, @TwoLetterISOLanguageName)

			INSERT [dbo].[StateEmailTemplate] 
					([EmailTemplateId], [EnableEmailSend], [StateId])
			VALUES (@TemplateId, @EnableEmailSend, @StateId);
		COMMIT
	END

END