using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationListPresenter
	{
		ApplicationListCollection List(string language, int? take = null, int skip = 0, Order[] groups = null,
			long? clientId = null, long? senderId = null, long? forwarderId = null, long? carrierId = null);
	}
}