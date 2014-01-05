using System;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IClientBalanceRepository
	{
		void AddToHistory(long clientId, decimal balance, decimal money, EventType type, DateTimeOffset timestamp, string comment);
		decimal GetBalance(long clientId);
		decimal SumBalance();
		void SetBalance(long clientId, decimal balance);
		ClientBalanceHistoryItem[] GetHistory(long clientId);
	}
}