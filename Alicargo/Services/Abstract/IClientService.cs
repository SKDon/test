using Alicargo.Core.Models;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IClientService
	{
		Client GetClient(long? id);
		void Update(Client model, CarrierSelectModel carrierSelectModel);
		void Add(Client model, CarrierSelectModel carrierSelectModel);
		ListCollection<Client> GetList(int take, int skip);
	}
}