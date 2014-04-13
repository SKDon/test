using System;
using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IApplicationEditor
	{
		long Add(ApplicationEditData application);
		void Delete(long id);
		void SetAirWaybill(long applicationId, long? airWaybillId);
		void SetClass(long id, int? classId);
		void SetDateInStock(long applicationId, DateTimeOffset dateTimeOffset);
		void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt);
		void SetFactureCostEdited(long id, decimal? factureCost);
		void SetFactureCostExEdited(long id, decimal? factureCostEx);
		void SetPickupCostEdited(long id, decimal? pickupCost);
		void SetScotchCostEdited(long id, decimal? scotchCost);
		void SetSenderRate(long id, decimal? senderRate);
		void SetState(long id, long stateId);
		void SetTariffPerKg(long id, decimal? tariffPerKg);
		void SetTransitCost(long id, decimal? transitCost);
		void SetTransitCostEdited(long id, decimal? transitCost);
		void SetTransitReference(long id, string transitReference);
		void SetCount(long id, int? value);
		void SetWeight(long id, float? value);
		void SetInsuranceRate(long id, float insuranceRate);
		void Update(long applicationId, ApplicationEditData application);
		void SetTotalTariffCost(long id, decimal? value);
		void SetProfit(long id, decimal? value);
	}
}