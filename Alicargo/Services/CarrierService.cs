using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
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
			return selectedId.HasValue
				? new CarrierSelectModel
				{
					CarrierId = selectedId.Value
				}
				: new CarrierSelectModel
				{
					CarrierId = _carriers.GetAll().First().Id
				};
		}

		public Dictionary<long, string> ToDictionary()
		{
			return _carriers.GetAll().ToDictionary(x => x.Id, x => x.Name);
		}

		public Func<long> AddOrGetCarrier(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			var carrier = _carriers.Get(name);
			if (carrier != null) return () => carrier.Id;

			return _carriers.Add(new CarrierData { Name = name });
		}
	}
}