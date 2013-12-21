CREATE PROCEDURE [dbo].[StateLocalization_Merge]
	@Name  NVARCHAR (320),
	@TwoLetterISOLanguageName CHAR(2),
	@StateId BIGINT
AS
BEGIN
	
	SET NOCOUNT ON;
	
    MERGE [dbo].[StateLocalization] AS target
    
	USING (SELECT @Name, @TwoLetterISOLanguageName, @StateId) AS source ([Name], [TwoLetterISOLanguageName], [StateId])
		ON (target.[StateId] = source.[StateId] 
		AND target.[TwoLetterISOLanguageName] = source.[TwoLetterISOLanguageName])
    
	WHEN MATCHED THEN 
        UPDATE SET [Name] = source.[Name]

	WHEN NOT MATCHED THEN	
	    INSERT ([Name], [TwoLetterISOLanguageName], [StateId])
	    VALUES (source.[Name], source.[TwoLetterISOLanguageName], source.[StateId]);

END
GO