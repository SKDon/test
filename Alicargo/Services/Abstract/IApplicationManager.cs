using System;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationManager
	{
		void Update(long applicationId, ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel);
		long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, long clientId);
		void Delete(long id);
		void SetState(long applicationId, long stateId);
		void SetTransitReference(long id, string transitReference);
		void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt);
		void SetTransitCost(long id, decimal? transitCost);
		void SetTariffPerKg(long id, decimal? tariffPerKg);
		void SetPickupCostEdited(long id, decimal? pickupCost);
		void SetFactureCostEdited(long id, decimal? factureCost);
		void SetScotchCostEdited(long id, decimal? scotchCost);
		void SetSenderRate(long id, decimal? senderRate);
		void SetClass(long id, ClassType? classType);
		void SetTransitCostEdited(long id, decimal? transitCost);
	}
}
