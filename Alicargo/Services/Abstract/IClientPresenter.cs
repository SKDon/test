using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IClientPresenter
	{
		ClientData GetCurrentClientData(long? clientId = null);
		ListCollection<ClientListItem> GetList(int take, int skip);
	}
}
