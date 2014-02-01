using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IClientApplicationManager
	{
		long Add(ApplicationClientModel application, TransitEditModel transit, long clientId);
		ApplicationClientModel Get(long applicationId);
		void Update(long applicationId, ApplicationClientModel application, TransitEditModel transit);
	}
}