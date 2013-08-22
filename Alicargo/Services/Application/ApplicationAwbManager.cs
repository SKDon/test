using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Application
{
    public sealed class ApplicationAwbManager : IApplicationAwbManager
    {
        private readonly IApplicationManager _applicationManager;
        private readonly IApplicationUpdateRepository _applicationUpdater;
        private readonly IAwbRepository _awbRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationAwbManager(
            IAwbRepository awbRepository,
            IUnitOfWork unitOfWork,
            IApplicationManager applicationManager,
            IApplicationUpdateRepository applicationUpdater)
        {
            _awbRepository = awbRepository;
            _unitOfWork = unitOfWork;
            _applicationManager = applicationManager;
            _applicationUpdater = applicationUpdater;
        }

        public void SetAwb(long applicationId, long? awbId)
        {
            // todo: 2. test
            if (awbId.HasValue)
            {
                var aggregate = _awbRepository.GetAggregate(awbId.Value).First();

                using (var ts = _unitOfWork.StartTransaction())
                {
                    // SetAirWaybill must be first
                    _applicationUpdater.SetAirWaybill(applicationId, awbId.Value);

                    _applicationManager.SetState(applicationId, aggregate.StateId);

                    _unitOfWork.SaveChanges();

                    ts.Complete();
                }
            }
            else
            {
                _applicationUpdater.SetAirWaybill(applicationId, null);

                _unitOfWork.SaveChanges();
            }
        }
    }
}