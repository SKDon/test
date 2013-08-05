using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationPresenter
	{
		ApplicationModel Get(long id);
		ApplicationStateModel[] GetAvailableStates(long id);
	}
}