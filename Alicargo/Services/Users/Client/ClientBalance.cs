using System;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation.Admin;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientBalance : IClientBalance
	{
		private readonly IClientBalanceRepository _balance;

		public ClientBalance(IClientBalanceRepository balance)
		{
			_balance = balance;
		}

		public void Add(long clientId, PaymentModel model, DateTimeOffset timestamp)
		{
			var balance = _balance.GetBalance(clientId);

			balance += model.Money;

			_balance.SetBalance(clientId, balance);

			_balance.AddToHistory(clientId, balance, model.Money, model.Comment, timestamp);
		}

		public decimal GetBalance(long clientId)
		{
			return _balance.GetBalance(clientId);
		}

		public ClientBalanceHistoryItem[] GetHistory(long clientId)
		{
			return _balance.GetHistory(clientId);
		}
	}
}