using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;

namespace Alicargo.DataAccess.Repositories.User
{
	internal sealed class ClientBalanceRepository : IClientBalanceRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ClientBalanceRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void AddToHistory(long clientId, decimal balance, decimal input, string comment)
		{
			_executor.Execute("[dbo].[ClientBalanceHistory_Add]", new { clientId, balance, input, comment });
		}

		public decimal GetBalance(long clientId)
		{
			return _executor.Query<decimal>("[dbo].[Client_GetBalance]", new { clientId });
		}

		public void SetBalance(long clientId, decimal balance)
		{
			_executor.Execute("[dbo].[Client_SetBalance]", new { clientId, balance });
		}
	}
}