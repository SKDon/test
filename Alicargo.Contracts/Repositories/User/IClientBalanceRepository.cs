using System;
using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IClientBalanceRepository
	{
		void AddToHistory(long clientId, decimal balance, decimal input, string comment, DateTimeOffset timestamp);
		decimal GetBalance(long clientId);
		void SetBalance(long clientId, decimal balance);
		ClientBalanceHistoryItem[] GetHistory(long clientId);
	}
}