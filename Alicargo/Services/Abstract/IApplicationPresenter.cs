using Alicargo.Core.Helpers;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationPresenter
	{
		ApplicationIndexModel GetApplicationIndexModel();
		ApplicationListCollection List(int take, int skip, Order[] groups);		
		ApplicationModel Get(long id);
		ApplicationStateModel[] GetAvailableStates(long id);
	}
}