using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IClientService
	{
		ClientData GetClientData(long? id = null);
		void Update(long clientId, ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, AuthenticationModel authenticationModel);
		void Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, AuthenticationModel authenticationModel);
		ListCollection<ClientData> GetList(int take, int skip);
	}
}