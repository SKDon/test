﻿using System;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IClientBalanceRepository
	{
		void AddToHistory(long clientId, decimal balance, decimal money, EventType type, DateTimeOffset timestamp,
			string comment, bool isCalculation);

		decimal GetBalance(long clientId);
		decimal SumBalance();
		void SetBalance(long clientId, decimal balance);
		ClientBalanceHistoryItem[] GetHistory(long clientId);
	}
}