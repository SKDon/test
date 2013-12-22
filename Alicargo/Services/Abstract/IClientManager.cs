using Alicargo.ViewModels;
using Alicargo.ViewModels.Calculation.Admin;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IClientManager
	{		
		void Update(long clientId, ClientModel model, CarrierSelectModel carrier, TransitEditModel transit, AuthenticationModel authentication);
		long Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, AuthenticationModel authenticationModel);
		void AddToBalance(long clientId, PaymentModel model);
	}
}