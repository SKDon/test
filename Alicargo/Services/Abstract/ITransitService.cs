using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ITransitService
	{
		long AddTransit(TransitEditModel model, CarrierSelectModel carrierModel);
		void Update(long transitId, TransitEditModel transit, CarrierSelectModel carrierModel);
		void Delete(long transitId);
	}
}