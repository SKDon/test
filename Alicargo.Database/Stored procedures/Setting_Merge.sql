CREATE PROCEDURE [dbo].[Setting_Merge]
	@Type INT,
	@RowVersion ROWVERSION,
	@Data VARBINARY(MAX)
AS BEGIN
	SET NOCOUNT ON

	MERGE [dbo].[Setting] AS target
		USING (SELECT @Type, @RowVersion, @Data) AS source ([Type], [RowVersion], [Data])
			ON (target.[RowVersion] = source.[RowVersion] AND target.[Type] = source.[Type])
		WHEN MATCHED THEN 
			UPDATE SET [Data] = source.[Data]
		WHEN NOT MATCHED THEN
			INSERT ([Type], [Data])
			VALUES (source.[Type], source.[Data])
		OUTPUT INSERTED.[RowVersion];

END
GO