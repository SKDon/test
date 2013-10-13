using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IClientPresenter
	{
		ClientData GetCurrentClientData(long? clientId = null);
		ListCollection<ClientData> GetList(int take, int skip);
		FileHolder GetCalculationFile(long clientId);
	}
}
