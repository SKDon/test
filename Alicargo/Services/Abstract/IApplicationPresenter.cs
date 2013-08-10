using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationPresenter
	{
		ApplicationEditModel Get(long id);
		ApplicationStateModel[] GetAvailableStates(long id);
	}
}