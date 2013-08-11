using Alicargo.Core.Models;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ITransitService
	{
		long AddTransit(TransitData model, CarrierSelectModel carrierModel);
		Transit[] Get(params long[] ids);
		void Update(Transit transit, CarrierSelectModel carrierModel);
		void Delete(long transitId);
	}
}