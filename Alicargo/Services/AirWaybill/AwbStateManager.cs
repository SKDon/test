using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.AirWaybill
{
    // todo: 2. registration
    internal sealed class AwbStateManager : IAwbStateManager
    {
        private readonly IApplicationManager _applicationManager;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IAwbRepository _awbRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AwbStateManager(
            IAwbRepository awbRepository,
            IApplicationRepository applicationRepository,
            IUnitOfWork unitOfWork,
            IApplicationManager applicationManager)
        {
            _awbRepository = awbRepository;
            _applicationRepository = applicationRepository;
            _unitOfWork = unitOfWork;
            _applicationManager = applicationManager;
        }

        public void SetState(long airWaybillId, long stateId)
        {
            var data = _awbRepository.Get(airWaybillId).First();

            SetStateImpl(airWaybillId, stateId, data.StateId);
        }

        private void SetStateImpl(long airWaybillId, long newStateId, long currentStateId)
        {
            var applications = _applicationRepository.GetByAirWaybill(airWaybillId);

            _awbRepository.SetState(airWaybillId, newStateId);

            // todo: 1. test sets
            foreach (var application in applications)
            {
                if (currentStateId == application.StateId)
                {
                    _applicationManager.SetState(application.Id, newStateId);
                }
            }

            _unitOfWork.SaveChanges();
        }
    }
}