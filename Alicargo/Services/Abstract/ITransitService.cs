using Alicargo.Core.Models;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ITransitService
	{
		long AddTransit(TransitEditModel model, CarrierSelectModel carrierModel);
		TransitEditModel[] Get(params long[] ids);
		void Update(long transitId, TransitEditModel transit, CarrierSelectModel carrierModel);
		void Delete(long transitId);
	}
}