using Alicargo.Core.Contracts;
using Alicargo.Core.Models;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ITransitService
	{
		long AddTransit(TransitData model, CarrierSelectModel carrierSelectModel);
		long AddOrGetCarrier(CarrierSelectModel carrierSelectModel);
		Transit[] Get(params long[] ids);
		void Update(Transit transit, CarrierSelectModel carrierSelectModel);
		void Delete(long transitId);
	}
}