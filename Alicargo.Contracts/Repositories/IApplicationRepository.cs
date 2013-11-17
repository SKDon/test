﻿using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;

namespace Alicargo.Contracts.Repositories
{
    public interface IApplicationRepository
    {
        ApplicationData Get(long id);
        ApplicationDetailsData GetDetails(long id);
        ApplicationData[] GetByAirWaybill(params long[] ids);
        ApplicationListItemData[] List(long[] stateIds, Order[] orders, int? take = null, int skip = 0,
            long? clientId = null, long? senderId = null, bool? hasCalculation = null,
			long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null);
		long Count(long[] stateIds, long? clientId = null, long? senderId = null, bool? hasCalculation = null,
			long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null);
        long GetClientId(long id);
        IReadOnlyDictionary<long, long> GetCalculations(long[] appIds);
    }
}