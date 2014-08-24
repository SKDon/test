using System;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class ClientBalanceRepository : IClientBalanceRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ClientBalanceRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void AddToHistory(long clientId, decimal balance, decimal money, EventType type, DateTimeOffset timestamp,
			string comment, bool isCalculation)
		{
			_executor.Execute("[dbo].[ClientBalanceHistory_Add]", new
			{
				clientId,
				balance,
				money,
				EventTypeId = type,
				timestamp,
				comment,
				isCalculation
			});
		}

		public ClientBalanceHistoryItem[] GetHistory(long clientId)
		{
			return _executor.Array<ClientBalanceHistoryItem>("[dbo].[ClientBalanceHistory_Get]", new { clientId });
		}

		public decimal GetBalance(long clientId)
		{
			return _executor.Query<decimal>("[dbo].[Client_GetBalance]", new { clientId });
		}

		public decimal SumBalance()
		{
			return _executor.Query<decimal>("[dbo].[Client_SumBalance]");
		}

		public RegistryOfPaymentsData[] GetRegistryOfPayments()
		{
			return Enumerable.Empty<RegistryOfPaymentsData>().ToArray();
		}

		public void SetBalance(long clientId, decimal balance)
		{
			_executor.Execute("[dbo].[Client_SetBalance]", new { clientId, balance });
		}
	}
}