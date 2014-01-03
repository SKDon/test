using System;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class ClientBalanceRepository : IClientBalanceRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ClientBalanceRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void AddToHistory(long clientId, decimal balance, decimal input, string comment, DateTimeOffset timestamp)
		{
			_executor.Execute("[dbo].[ClientBalanceHistory_Add]", new { clientId, balance, input, comment, timestamp });
		}

		public decimal GetBalance(long clientId)
		{
			return _executor.Query<decimal>("[dbo].[Client_GetBalance]", new { clientId });
		}

		public void SetBalance(long clientId, decimal balance)
		{
			_executor.Execute("[dbo].[Client_SetBalance]", new { clientId, balance });
		}

		public ClientBalanceHistoryItem[] GetHistory(long clientId)
		{
			return _executor.Array<ClientBalanceHistoryItem>("[dbo].[ClientBalanceHistory_Get]", new { clientId });
		}
	}
}