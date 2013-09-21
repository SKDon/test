using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationSenderManager
	{
		void Add(ApplicationSenderModel model, long clientId, long senderId);
		ApplicationSenderModel Get(long id);
		void Update(long id, ApplicationSenderModel model);
	}
}
