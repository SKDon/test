using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	// todo: test
	public sealed class CarrierService : ICarrierService
	{
		private readonly ICarrierRepository _carriers;

		public CarrierService(ICarrierRepository carriers)
		{
			_carriers = carriers;
		}

		public CarrierSelectModel Get(long? selectedId)
		{
			var all = _carriers.GetAll();

			var model = new CarrierSelectModel
			{
				Carriers = all.ToDictionary(x => x.Name, x => x.Name),
				CarrierName = all.First(x => !selectedId.HasValue || x.Id == selectedId).Name
			};
			return model;
		}

		public Dictionary<long, string> ToDictionary()
		{
			return _carriers.GetAll().ToDictionary(x => x.Id, x => x.Name);
		}

		public Func<long> AddOrGetCarrier(CarrierSelectModel carrierSelectModel)
		{
			var carrierName = carrierSelectModel.NewCarrierName ?? carrierSelectModel.CarrierName;
			var carrier = _carriers.Get(carrierName);
			if (carrier != null) return () =>  carrier.Id;

			return _carriers.Add(new Carrier { Name = carrierName });
		}
	}
}