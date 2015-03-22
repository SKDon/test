using System.Collections.Generic;
using System.Globalization;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Client.Balance
{
	internal sealed class BalanceLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly IClientBalanceRepository _balance;
		private readonly IClientRepository _clients;
		private readonly ISerializer _serializer;

		public BalanceLocalizedDataHelper(IClientBalanceRepository balance, ISerializer serializer, IClientRepository clients)
		{
			_balance = balance;
			_serializer = serializer;
			_clients = clients;
		}

		public IDictionary<string, string> Get(string language, EventDataForEntity eventData)
		{
			var clientData = _clients.Get(eventData.EntityId);

			var paymentEventData = _serializer.Deserialize<PaymentEventData>(eventData.Data);

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