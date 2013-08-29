using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IClientPresenter
	{
		ClientData GetClientData(long? clientId = null);
		ListCollection<ClientData> GetList(int take, int skip);
	}
}
