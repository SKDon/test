using System.Collections.Generic;
using System.Globalization;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Client.Balance
{
	internal sealed class BalanceLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly IClientBalanceRepository _balance;
		private readonly IClientRepository _clients;
		private readonly ISenderRepository _senders;
		private readonly ISerializer _serializer;

		public BalanceLocalizedDataHelper(
			IClientBalanceRepository balance,
			ISerializer serializer,
			IClientRepository clients,
			ISenderRepository senders)
		{
			_balance = balance;
			_serializer = serializer;
			_clients = clients;
			_senders = senders;
		}

		public IDictionary<string, string> Get(string language, EventDataForEntity eventData)
		{
			var client = _clients.Get(eventData.EntityId);
			var paymentEvent = _serializer.Deserialize<PaymentEventData>(eventData.Data);
			var culture = CultureInfo.GetCultureInfo(language);
			var balance = _balance.GetBalance(client.ClientId);

			return new Dictionary<string, string>
			{
				{ "ClientBalance", balance.ToString("N2") },
				{ "Money", paymentEvent.Money.ToString("N2") },
				{ "Comment", paymentEvent.Comment },
				{ "ClientNic", client.Nic },
				{ "LegalEntity", client.LegalEntity },
				{ "Timestamp", LocalizationHelper.GetDate(paymentEvent.Timestamp, culture) },
				{ "SenderName", GetSenderName(client) }
			};
		}

		private string GetSenderName(ClientEditData client)
		{
			return client.DefaultSenderId.HasValue
				? _senders.Get(client.DefaultSenderId.Value).Name
				: null;
		}
	}
}