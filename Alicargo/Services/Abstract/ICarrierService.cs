using System;
using System.Collections.Generic;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ICarrierService
	{
		CarrierSelectModel Get(long? selectedId);
		Func<long> AddOrGetCarrier(string name);
		Dictionary<long, string> ToDictionary();
	}
}