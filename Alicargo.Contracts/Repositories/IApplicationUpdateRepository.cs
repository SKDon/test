using System;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationUpdateRepository
	{
		void Update(ApplicationData application, byte[] swiftFile = null, byte[] invoiceFile = null, byte[] cpFile = null, byte[] deliveryBillFile = null, byte[] torg12File = null, byte[] packingFile = null);
		Func<long> Add(ApplicationData application, byte[] swiftFile, byte[] invoiceFile, byte[] cpFile, byte[] deliveryBillFile, byte[] torg12File, byte[] packingFile);
		void Delete(long id);

		void SetAirWaybill(long applicationId, long? airWaybillId);
		void SetState(long id, long stateId);
		void SetDateInStock(long applicationId, DateTimeOffset dateTimeOffset);
		void SetTransitReference(long id, string transitReference);
		void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt);
		void SetTransitCost(long id, decimal? transitCost);
		void SetTariffPerKg(long id, decimal? tariffPerKg);
		void SetWithdrawCostEdited(long id, decimal? withdrawCost);
		void SetFactureCostEdited(long id, decimal? factureCost);
		void SetScotchCostEdited(long id, decimal? scotchCost);
	}
}
