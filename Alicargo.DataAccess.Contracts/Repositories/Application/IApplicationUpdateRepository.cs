using System;
using Alicargo.Contracts.Contracts.Application;

namespace Alicargo.Contracts.Repositories.Application
{
	public interface IApplicationUpdateRepository
	{
		void Update(ApplicationData application);
		Func<long> Add(ApplicationData application);
		void Delete(long id);

		void SetAirWaybill(long applicationId, long? airWaybillId);
		void SetState(long id, long stateId);
		void SetDateInStock(long applicationId, DateTimeOffset dateTimeOffset);
		void SetTransitReference(long id, string transitReference);
		void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt);
		void SetTransitCost(long id, decimal? transitCost);
		void SetTransitCostEdited(long id, decimal? transitCost);
		void SetTariffPerKg(long id, decimal? tariffPerKg);
		void SetPickupCostEdited(long id, decimal? pickupCost);
		void SetFactureCostEdited(long id, decimal? factureCost);
		void SetScotchCostEdited(long id, decimal? scotchCost);
		void SetSenderRate(long id, decimal? senderRate);
		void SetClass(long id, int? classId);
	}
}
