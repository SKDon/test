using System;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientBalance : IClientBalance
	{
		private readonly IClientBalanceRepository _balance;

		public ClientBalance(IClientBalanceRepository balance)
		{
			_balance = balance;
		}

		public void Increase(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			if (money < 0)
			{
				throw new ArgumentException(@"Money value must be positive", "money");
			}

			SetBalance(clientId, money, comment, timestamp);
		}

		public void Decrease(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			if (money < 0)
			{
				throw new ArgumentException(@"Money value must be positive", "money");
			}

			SetBalance(clientId, -money, comment, timestamp);
		}

		private void SetBalance(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			var balance = _balance.GetBalance(clientId);

			balance += money;

			_balance.SetBalance(clientId, balance);

			_balance.AddToHistory(clientId, balance, money, comment, timestamp);
		}
	}
}