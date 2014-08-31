using System;
using Alicargo.DataAccess.Contracts.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IClientBalanceRepository
	{
		void AddToHistory(
			long clientId, decimal balance, decimal money, EventType type, DateTimeOffset timestamp,
			string comment, bool isCalculation);
		decimal GetBalance(long clientId);
		ClientBalanceHistoryItem[] GetHistory(long clientId);
		void SetBalance(long clientId, decimal balance);
		decimal SumBalance();
		RegistryOfPaymentsData[] GetRegistryOfPayments();
	}
}