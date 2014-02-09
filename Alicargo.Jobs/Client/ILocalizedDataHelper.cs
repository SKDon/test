using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.Jobs.Client
{
	internal interface ILocalizedDataHelper
	{
		IDictionary<string, string> Get(string language, byte[] eventData, ClientData clientData);
	}
}
