using System;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationManager
	{
		ApplicationEditModel Get(long id);
		void Update(long applicationId, ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel);
		long Add(ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, long clientId);
		void Delete(long id);

        // todo: 2. ISP
		void SetState(long applicationId, long stateId);
		void SetTransitReference(long id, string transitReference);
		void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt);
		void SetTransitCost(long id, decimal? transitCost);
		void SetTariffPerKg(long id, decimal? tariffPerKg);
		void SetWithdrawCost(long id, decimal? withdrawCost);
		void SetFactureCost(long id, decimal? factureCost);
		void SetScotchCost(long id, decimal? scotchCost);
	}
}
