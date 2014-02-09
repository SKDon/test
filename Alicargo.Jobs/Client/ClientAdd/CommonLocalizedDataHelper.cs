using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Client.ClientAdd
{
	internal sealed class CommonLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly ISerializer _serializer;

		public CommonLocalizedDataHelper(ISerializer serializer)
		{
			_serializer = serializer;
		}

		public IDictionary<string, string> Get(string language, byte[] eventData, ClientData clientData)
		{
			var password = _serializer.Deserialize<string>(eventData);

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