using Alicargo.Contracts.Helpers;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationListPresenter
	{
		ApplicationListCollection List(int take, int skip, Order[] groups);
	}
}
