using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface ISenderApplicationManager
	{
		void Add(ApplicationSenderModel model, ClientData clientId, long creatorSenderId);
		ApplicationSenderModel Get(long id);
		void Update(long id, ApplicationSenderModel model);
	}
}
