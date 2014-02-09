using System.Collections.Generic;
using System.Globalization;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Client.Balance
{
	internal sealed class BalanceLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly IClientBalanceRepository _balance;
		private readonly ISerializer _serializer;

		public BalanceLocalizedDataHelper(IClientBalanceRepository balance, ISerializer serializer)
		{
			_balance = balance;
			_serializer = serializer;
		}

		public IDictionary<string, string> Get(string language, byte[] eventData, ClientData clientData)
		{
			var paymentEventData = _serializer.Deserialize<PaymentEventData>(eventData);

			var culture = CultureInfo.GetCultureInfo(language);
			var balance = _balance.GetBalance(clientData.ClientId);

			return new Dictionary<string, string>
			{
				{ "ClientBalance", balance.ToString("N2") },
				{ "Money", paymentEventData.Money.ToString("N2") },
				{ "Comment", paymentEventData.Comment },
				{ "ClientNic", clientData.Nic },
				{ "LegalEntity", clientData.LegalEntity },
				{ "Timestamp", LocalizationHelper.GetDate(paymentEventData.Timestamp, culture) }
			};
		}
	}
}