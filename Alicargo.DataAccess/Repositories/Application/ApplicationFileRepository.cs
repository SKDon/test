using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class ApplicationFileRepository : IApplicationFileRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ApplicationFileRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public IReadOnlyDictionary<long, string> GetNames(long applicationId, ApplicationFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AppIds", new[] { applicationId });
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			return _executor.Array<dynamic>("[dbo].[ApplicationFile_GetNames]", parameters)
				.ToDictionary(x => (long)x.Id, x => (string)x.Name);
		}

		public IReadOnlyDictionary<long, FileInfo[]> GetInfo(long[] applicationIds, ApplicationFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AppIds", applicationIds);
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			return _executor.Array<dynamic>("[dbo].[ApplicationFile_GetNames]", parameters)
				.GroupBy(x => (long)x.ApplicationId)
				.ToDictionary(a => a.Key, a => a.Select(x => new FileInfo
				{
					Id = x.Id,
					Name = x.Name
				}).ToArray());
		}

		public FileHolder Get(long id)
		{
			return _executor.Query<FileHolder>("[dbo].[ApplicationFile_Get]", new { id });
		}

		public long Add(long applicationId, ApplicationFileType type, string name, byte[] data)
		{
			return _executor.Query<long>("[dbo].[ApplicationFile_Add]", new { applicationId, TypeId = type, name, data });
		}

		public void Delete(long id)
		{
			_executor.Execute("[dbo].[ApplicationFile_Delete]", new { id });
		}
	}
}