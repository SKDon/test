using System.Data.SqlClient;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.FilesMigration.Properties;
using Dapper;

namespace Alicargo.FilesMigration
{
	internal class Program
	{
		private static void Main()
		{
			using(var mainDb = new SqlConnection(Settings.Default.MainConnectionString))
			using(var filesDb = new SqlConnection(Settings.Default.FilesConnectionString))
			{
				mainDb.Open();
				var applications = mainDb.Query(
					@"SELECT [Id]
						  ,[InvoiceFileData]
						  ,[InvoiceFileName]
						  ,[SwiftFileData]
						  ,[SwiftFileName]
						  ,[DeliveryBillFileData]
						  ,[DeliveryBillFileName]
						  ,[Torg12FileData]
						  ,[Torg12FileName]
						  ,[CPFileData]
						  ,[CPFileName]
						  ,[PackingFileData]
						  ,[PackingFileName]
					  FROM [dbo].[Application]");

				filesDb.Open();
				foreach(var application in applications)
				{
					var id = application.Id;

					Insert(filesDb, id, ApplicationFileType.Invoice, application.InvoiceFileName, application.InvoiceFileData);
					Insert(filesDb, id, ApplicationFileType.CP, application.CPFileName, application.CPFileData);
					Insert(filesDb, id, ApplicationFileType.DeliveryBill, application.DeliveryBillFileName, application.DeliveryBillFileData);
					Insert(filesDb, id, ApplicationFileType.Packing, application.PackingFileName, application.PackingFileData);
					Insert(filesDb, id, ApplicationFileType.Swift, application.SwiftFileName, application.SwiftFileData);
					Insert(filesDb, id, ApplicationFileType.Torg12, application.Torg12FileName, application.Torg12FileData);
				}
			}
		}

		private static void Insert(SqlConnection filesDb, long id,
			ApplicationFileType fileType, string fileName, byte[] fileData)
		{
			if(string.IsNullOrEmpty(fileName) || fileData == null)
			{
				return;
			}

			filesDb.Execute(
				@"INSERT INTO [dbo].[ApplicationFile]
							([ApplicationId],
							 [TypeId],
							 [Name],
							 [Data])
							VALUES
							(@ApplicationId,
							 @TypeId,
							 @Name,
							 @Data)", new
				{
					ApplicationId = id,
					TypeId = fileType,
					Name = fileName,
					Data = fileData
				});
		}
	}
}