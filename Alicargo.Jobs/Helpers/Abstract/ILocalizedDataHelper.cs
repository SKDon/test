using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Jobs.Helpers.Abstract
{
	internal interface ILocalizedDataHelper
	{
		IDictionary<string, string> Get(string language, EventDataForEntity eventData);
	}
}