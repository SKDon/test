using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Helpers.Abstract
{
	public interface ICommonFilesFacade
	{
		IReadOnlyDictionary<string, FileHolder[]> GetFiles(EventType type, EventDataForEntity eventData, string[] languages);
	}
}