using System;
using System.Collections.Generic;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ICarrierService
	{
		CarrierSelectModel Get(long? selectedId);
		Func<long> AddOrGetCarrier(CarrierSelectModel carrierSelectModel);
		Dictionary<long, string> ToDictionary();
	}
}