using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services
{
    internal sealed class CarrierService : ICarrierService
	{
		private readonly ICarrierRepository _carriers;
		private readonly ITransitRepository _transitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CarrierService(
			ICarrierRepository carriers, 
			ITransitRepository transitRepository,
            IUnitOfWork unitOfWork)
		{
			_carriers = carriers;
			_transitRepository = transitRepository;
		    _unitOfWork = unitOfWork;
		}

		public CarrierSelectModel Get(long? transitId)
		{
            return transitId.HasValue // todo: 3. test
				? new CarrierSelectModel
				{
					CarrierId = _transitRepository.Get(transitId.Value).First().CarrierId
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

		private Func<long> AddOrGetCarrier(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			var carrier = _carriers.Get(name);
			if (carrier != null) return () => carrier.Id;

			return _carriers.Add(new CarrierData { Name = name });
		}

        public long AddOrGetCarrier(CarrierSelectModel model)
        {
            if (model.NewCarrierName.IsNullOrWhiteSpace()) return model.CarrierId;

            var id = AddOrGetCarrier(model.NewCarrierName);

            _unitOfWork.SaveChanges();

            return id();
        }
	}
}