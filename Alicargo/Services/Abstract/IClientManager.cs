using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IClientManager
	{		
		void Update(long clientId, ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, AuthenticationModel authenticationModel);
		long Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, AuthenticationModel authenticationModel);
	}
}