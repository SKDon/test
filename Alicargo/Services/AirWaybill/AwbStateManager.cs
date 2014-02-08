using System.Linq;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.AirWaybill
{	
    internal sealed class AwbStateManager : IAwbStateManager
    {
        private readonly IAdminApplicationManager _applicationManager;
        private readonly IApplicationRepository _applications;
        private readonly IAwbRepository _awbs;

        public AwbStateManager(
            IAwbRepository awbs,
            IApplicationRepository applications,
            IAdminApplicationManager applicationManager)
        {
            _awbs = awbs;
            _applications = applications;
            _applicationManager = applicationManager;
        }

        public void SetState(long airWaybillId, long stateId)
        {
            var oldData = _awbs.Get(airWaybillId).First();
            var oldStateId = oldData.StateId;

            _awbs.SetState(airWaybillId, stateId);

            UpdateApplicationsState(airWaybillId, stateId, oldStateId);
        }

        private void UpdateApplicationsState(long airWaybillId, long stateId, long oldStateId)
        {
            if (stateId == oldStateId)
            {
                return;
            }

            var applications = _applications.GetByAirWaybill(airWaybillId);
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