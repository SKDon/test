using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Client.ClientAdd
{
	internal sealed class CommonLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly ISerializer _serializer;
		private readonly IClientRepository _clients;

		public CommonLocalizedDataHelper(ISerializer serializer, IClientRepository clients)
		{
			_serializer = serializer;
			_clients = clients;
		}

		public IDictionary<string, string> Get(string language, EventDataForEntity eventData)
		{
			var clientData = _clients.Get(eventData.EntityId);

			var password = _serializer.Deserialize<string>(eventData.Data);

			return new Dictionary<string, string>
			{
				{ "ClientNic", clientData.Nic },
				{ "LegalEntity", clientData.LegalEntity },
				{ "Password", password },
				{ "Login", clientData.Login }
			};
		}
	}
}