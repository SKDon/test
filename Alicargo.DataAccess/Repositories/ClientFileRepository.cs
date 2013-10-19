using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ClientFileRepository : IClientFileRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ClientFileRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public FileHolder GetClientDocument(long clientId)
		{
			return _executor.Get<FileHolder>("[dbo].[GetClientContract]", new {clientId});
		}
	}
}