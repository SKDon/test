using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.AirWaybill
{
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
            var oldData = _awbRepository.Get(airWaybillId).First();
            var oldStateId = oldData.StateId;

            _awbRepository.SetState(airWaybillId, stateId);

            UpdateApplicationsState(airWaybillId, stateId, oldStateId);

            _unitOfWork.SaveChanges();
        }

        private void UpdateApplicationsState(long airWaybillId, long stateId, long oldStateId)
        {
            if (stateId == oldStateId)
            {
                return;
            }

            var applications = _applicationRepository.GetByAirWaybill(airWaybillId);
            foreach (var application in applications)
            {
                if (oldStateId == application.StateId)
                {
                    _applicationManager.SetState(application.Id, stateId);
                }
            }
        }
    }
}