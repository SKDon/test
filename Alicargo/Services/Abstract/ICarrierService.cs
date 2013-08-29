using System.Collections.Generic;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ICarrierService
	{
		CarrierSelectModel Get(long? transitId);
        long AddOrGetCarrier(CarrierSelectModel model);
		Dictionary<long, string> ToDictionary();
	}
}