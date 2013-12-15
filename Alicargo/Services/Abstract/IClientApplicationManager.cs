using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IClientApplicationManager
	{
		long Add(ApplicationClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, long clientId);

		void Update(long applicationId, ApplicationClientModel model, CarrierSelectModel carrierModel,
			TransitEditModel transitModel);

		ApplicationClientModel Get(long id);
	}
}