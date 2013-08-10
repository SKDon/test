using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationPresenter
	{
		ApplicationDetailsModel Get(long id);
		ApplicationStateModel[] GetAvailableStates(long id);
	}
}