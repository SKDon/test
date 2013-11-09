CREATE PROCEDURE [dbo].[EmailMessage_Add]
	@State INT,
	@PartitionId INT,
	@From NVARCHAR (MAX),
	@To NVARCHAR (MAX),
	@CopyTo NVARCHAR (MAX),
	@Subject NVARCHAR (MAX),
	@Body NVARCHAR (MAX),
	@IsBodyHtml BIT,
	@Files VARBINARY (MAX)
AS
	SET NOCOUNT ON;

	INSERT [dbo].[EmailMessage] ([Body], [CopyTo], [Files], [From], [IsBodyHtml], [PartitionId], [StateId], [To], [Subject])
	VALUES (@Body, @CopyTo, @Files, @From, @IsBodyHtml, @PartitionId, @State, @To, @Subject)

GO