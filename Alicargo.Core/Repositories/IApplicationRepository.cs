using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;

namespace Alicargo.Core.Repositories
{
	public interface IApplicationRepository
	{
		ApplicationData Get(long id);
		ApplicationDetailsData GetDetails(long id);
		ApplicationData[] GetByAirWaybill(long id);
		ApplicationListItemData[] List(int take, int skip, IEnumerable<long> stateIds, Order[] orders = null, long? clientUserId = null);
		long Count(IEnumerable<long> stateIds, long? clientUserId = null);
		long GetClientId(long id);

		FileHolder GetInvoiceFile(long id);
		FileHolder GetSwiftFile(long id);
		FileHolder GetCPFile(long id);
		FileHolder GetDeliveryBillFile(long id);
		FileHolder GetTorg12File(long id);
		FileHolder GetPackingFile(long id);
	}
}
