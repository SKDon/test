using Alicargo.Core.Helpers;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationListPresenter
	{
		ApplicationListCollection List(int take, int skip, Order[] groups);

	}
}
