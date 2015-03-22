using System.Collections.ObjectModel;
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

		public ReadOnlyCollection<FileInfo> GetNames(long applicationId, ApplicationFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AppIds", new[] { applicationId });
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			var names = _executor.Array<dynamic>("[dbo].[ApplicationFile_GetNames]", parameters)
				.Select(x => new FileInfo
				{
					Name = x.Name,
					Id = x.Id
				}).ToArray();

			return new ReadOnlyCollection<FileInfo>(names);
		}

		public ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>> GetInfo(long[] applicationIds, ApplicationFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AppIds", applicationIds);
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			var dictionary = _executor.Array<dynamic>("[dbo].[ApplicationFile_GetNames]", parameters)
				.GroupBy(x => (long)x.ApplicationId)
				.ToDictionary(a => a.Key,
					a =>
					{
						var names = a.Select(x => new FileInfo
						{
							Id = x.Id,
							Name = x.Name
						}).ToArray();

						return new ReadOnlyCollection<FileInfo>(names);
					});

			return new ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>>(dictionary);
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