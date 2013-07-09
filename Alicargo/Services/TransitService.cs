using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	// todo: test
	public sealed class TransitService : ITransitService
	{
		private readonly ICarrierService _carrierService;
		private readonly ITransitRepository _transitRepository;
		private readonly IUnitOfWork _unitOfWork;

		public TransitService(IUnitOfWork unitOfWork, ICarrierService carrierService, ITransitRepository transitRepository)
		{
			_unitOfWork = unitOfWork;
			_carrierService = carrierService;
			_transitRepository = transitRepository;
		}

		public Transit[] Get(params long[] ids)
		{
			var data = _transitRepository.Get(ids);
			var carriers = _carrierService.ToDictionary();

			return data.Select(x => new Transit(x, carriers[x.CarrierId])).ToArray();
		}

		public void Update(Transit transit, CarrierSelectModel carrierSelectModel = null)
		{
			using (var ts = _unitOfWork.GetTransactionScope())
			{
				transit.CarrierId = AddOrGetCarrier(carrierSelectModel);

				_transitRepository.Update(transit);

				_unitOfWork.SaveChanges();
				
				ts.Complete();
			}
		}

		public void Delete(long transitId)
		{
			_transitRepository.Delete(transitId);
			_unitOfWork.SaveChanges();
		}

		public long AddTransit(TransitData model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.GetTransactionScope())
			{
				model.CarrierId = AddOrGetCarrier(carrierSelectModel);

				var transitId = _transitRepository.Add(model);

				_unitOfWork.SaveChanges();

				ts.Complete();

				return transitId();
			}
		}

		public long AddOrGetCarrier(CarrierSelectModel carrierSelectModel)
		{
			var id = _carrierService.AddOrGetCarrier(carrierSelectModel);
			_unitOfWork.SaveChanges();
			return id();
		}
	}
}