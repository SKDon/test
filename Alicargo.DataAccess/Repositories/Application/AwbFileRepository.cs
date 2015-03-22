using System.Collections.ObjectModel;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class AwbFileRepository : IAwbFileRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public AwbFileRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public ReadOnlyCollection<FileInfo> GetNames(long awbId, AwbFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AwbIds", new[] { awbId });
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			var infos = _executor.Array<dynamic>("[dbo].[AwbFile_GetNames]", parameters)
				.Select(x => new FileInfo
				{
					Id = x.Id,
					Name = x.Name
				})
				.ToArray();

			return new ReadOnlyCollection<FileInfo>(infos);
		}

		public ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>> GetInfo(long[] awbIds, AwbFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AwbIds", awbIds);
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			var dictionary = _executor.Array<dynamic>("[dbo].[AwbFile_GetNames]", parameters)
				.GroupBy(x => (long)x.AwbId)
				.ToDictionary(a => a.Key,
					a =>
					{
						var infos = a.Select(x => new FileInfo
						{
							Id = x.Id,
							Name = x.Name
						}).ToArray();

						return new ReadOnlyCollection<FileInfo>(infos);
					});

			return new ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>>(dictionary);
		}

		public FileHolder Get(long id)
		{
			return _executor.Query<FileHolder>("[dbo].[AwbFile_Get]", new { id });
		}

		public long Add(long awbId, AwbFileType type, string name, byte[] data)
		{
			return _executor.Query<long>("[dbo].[AwbFile_Add]", new { awbId, TypeId = type, name, data });
		}

		public void Delete(long id)
		{
			_executor.Execute("[dbo].[AwbFile_Delete]", new { id });
		}

		public FileHolder GTDAdditionalFile(long awbId)
		{
			return GetFileHolder(awbId, AwbFileType.GTDAdditional);
		}

		public FileHolder GetAWBFile(long awbId)
		{
			return GetFileHolder(awbId, AwbFileType.AWB);
		}

		public FileHolder GetGTDFile(long awbId)
		{
			return GetFileHolder(awbId, AwbFileType.GTD);
		}

		public FileHolder GetInvoiceFile(long awbId)
		{
			return GetFileHolder(awbId, AwbFileType.Invoice);
		}

		public FileHolder GetPackingFile(long awbId)
		{
			return GetFileHolder(awbId, AwbFileType.Packing);
		}

		public FileHolder GetDrawFile(long awbId)
		{
			return GetFileHolder(awbId, AwbFileType.Draw);
		}

		private FileHolder GetFileHolder(long awbId, AwbFileType awbFileType)
		{
			var name = GetNames(awbId, awbFileType).FirstOrDefault();

			return name != null ? Get(name.Id) : null;
		}
	}
}