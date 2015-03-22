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
				var awbs = mainDb.Query(
					@"SELECT [Id]
						,[GTDFileData]
						,[GTDFileName]
						,[GTDAdditionalFileData]
						,[GTDAdditionalFileName]
						,[PackingFileData]
						,[PackingFileName]
						,[InvoiceFileData]
						,[InvoiceFileName]
						,[AWBFileData]
						,[AWBFileName]
						,[DrawFileData]
						,[DrawFileName]
					  FROM [dbo].[AirWaybill]");

				filesDb.Open();
				foreach(var awb in awbs)
				{
					var id = awb.Id;

					Insert(filesDb, id, AwbFileType.AWB, awb.AWBFileName, awb.AWBFileData);
					Insert(filesDb, id, AwbFileType.GTD, awb.GTDFileName, awb.GTDFileData);
					Insert(filesDb, id, AwbFileType.GTDAdditional, awb.GTDAdditionalFileName, awb.GTDAdditionalFileData);
					Insert(filesDb, id, AwbFileType.Invoice, awb.InvoiceFileName, awb.InvoiceFileData);
					Insert(filesDb, id, AwbFileType.Packing, awb.PackingFileName, awb.PackingFileData);
					Insert(filesDb, id, AwbFileType.Draw, awb.DrawFileName, awb.DrawFileData);
				}
			}
		}

		private static void Insert(SqlConnection filesDb, long id,
			AwbFileType fileType, string fileName, byte[] fileData)
		{
			if(string.IsNullOrEmpty(fileName) || fileData == null)
			{
				return;
			}

			filesDb.Execute(@"
					INSERT [dbo].[AwbFile] ([AwbId], [Data], [Name], [TypeId])
					VALUES (@AwbId, @Data, @Name, @TypeId)", new
				{
					AwbId = id,
					TypeId = fileType,
					Name = fileName,
					Data = fileData
				});
		}
	}
}