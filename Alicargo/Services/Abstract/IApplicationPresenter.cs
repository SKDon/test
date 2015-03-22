using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationPresenter
	{
		ApplicationAdminModel Get(long id);
		ApplicationStateModel[] GetStateAvailability(long id);
	}
}