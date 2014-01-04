using System;
using Alicargo.Contracts.Contracts.User;
using Alicargo.ViewModels.Calculation.Admin;

namespace Alicargo.Services.Abstract
{
	public interface IClientBalance
	{
		void Add(long clientId, PaymentModel model, DateTimeOffset timestamp);
		decimal GetBalance(long clientId);
		ClientBalanceHistoryItem[] GetHistory(long clientId);
	}
}