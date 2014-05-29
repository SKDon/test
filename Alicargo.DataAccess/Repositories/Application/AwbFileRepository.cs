using System.Collections.Generic;
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

		public IReadOnlyDictionary<long, string> GetNames(long awbId, AwbFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AwbIds", new[] { awbId });
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			return _executor.Array<dynamic>("[dbo].[AwbFile_GetNames]", parameters)
				.ToDictionary(x => (long)x.Id, x => (string)x.Name);
		}

		public IReadOnlyDictionary<long, FileInfo[]> GetInfo(long[] awbIds, AwbFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AwbIds", awbIds);
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			return _executor.Array<dynamic>("[dbo].[AwbFile_GetNames]", parameters)
				.GroupBy(x => (long)x.AwbId)
				.ToDictionary(a => a.Key,
					a => a.Select(x => new FileInfo
					{
						Id = x.Id,
						Name = x.Name
					}).ToArray());
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
	}
}