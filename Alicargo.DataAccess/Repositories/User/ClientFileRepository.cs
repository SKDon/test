using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class ClientFileRepository : IClientFileRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ClientFileRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public FileHolder GetClientContract(long clientId)
		{
			return _executor.Query<FileHolder>("[dbo].[ClientContract_Get]", new { clientId });
		}

		public string GetClientContractFileName(long clientId)
		{
			return _executor.Query<string>("[dbo].[ClientContract_GetFileName]", new { clientId });
		}

		public void SetClientContract(long clientId, string name, byte[] data)
		{
			_executor.Execute("[dbo].[ClientContract_Merge]", new { clientId, name, data });
		}
	}
}