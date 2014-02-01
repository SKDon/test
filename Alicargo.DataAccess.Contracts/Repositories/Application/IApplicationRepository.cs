using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Helpers;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IApplicationRepository
	{
		long Count(long[] stateIds, long? clientId = null, long? senderId = null, long? carrierId = null,
			long? forwarderId = null, long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null,
			bool? hasCalculation = null);

		ApplicationData Get(long id);

		ApplicationData[] GetByAirWaybill(params long[] ids);

		IReadOnlyDictionary<long, long> GetCalculations(long[] appIds);

		long GetClientId(long id);

		ApplicationExtendedData GetExtendedData(long id);

		// todo: 1. remove hasCalculation parameter from List and Count methods
		ApplicationExtendedData[] List(long[] stateIds, Order[] orders, int? take = null, int skip = 0, long? clientId = null,
			long? senderId = null, long? carrierId = null, long? forwarderId = null, long? cargoReceivedStateId = null,
			int? cargoReceivedDaysToShow = null, bool? hasCalculation = null);
	}
}