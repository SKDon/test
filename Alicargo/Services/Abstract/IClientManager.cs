using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IClientManager
	{
		void Update(long clientId, ClientModel model, TransitEditModel transit);
		long Add(ClientModel client, TransitEditModel transit);
	}
}