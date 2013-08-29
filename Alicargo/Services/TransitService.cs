using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
    internal sealed class TransitService : ITransitService
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

        public TransitEditModel[] Get(params long[] ids)
        {
            var data = _transitRepository.Get(ids);

            return data.Select(TransitEditModel.GetModel).ToArray();
        }

        public void Update(long transitId, TransitEditModel transit, CarrierSelectModel carrierModel)
        {
            var carrierId = _carrierService.AddOrGetCarrier(carrierModel);

            var data = TransitEditModel.GetData(transit, carrierId);

            data.Id = transitId;

            _transitRepository.Update(data);

            _unitOfWork.SaveChanges();
        }

        public void Delete(long transitId)
        {
            _transitRepository.Delete(transitId);

            _unitOfWork.SaveChanges();
        }

        public long AddTransit(TransitEditModel model, CarrierSelectModel carrierModel)
        {
            var carrierId = _carrierService.AddOrGetCarrier(carrierModel);

            var transitId = _transitRepository.Add(TransitEditModel.GetData(model, carrierId));

            _unitOfWork.SaveChanges();

            return transitId();
        }
    }
}