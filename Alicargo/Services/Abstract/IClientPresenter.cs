using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IClientPresenter
	{
		ClientData GetClientData(long? id = null);
		ListCollection<ClientData> GetList(int take, int skip);
	}
}
