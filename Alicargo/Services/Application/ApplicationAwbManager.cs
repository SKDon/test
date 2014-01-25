using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Application
{
    public sealed class ApplicationAwbManager : IApplicationAwbManager
    {
        private readonly IAdminApplicationManager _applicationManager;
        private readonly IApplicationEditor _applicationUpdater;
        private readonly IAwbRepository _awbRepository;
	    private readonly IStateConfig _stateConfig;
	    private readonly IUnitOfWork _unitOfWork;

        public ApplicationAwbManager(
            IAwbRepository awbRepository,
			IStateConfig stateConfig,
            IUnitOfWork unitOfWork,
            IAdminApplicationManager applicationManager,
            IApplicationEditor applicationUpdater)
        {
            _awbRepository = awbRepository;
	        _stateConfig = stateConfig;
	        _unitOfWork = unitOfWork;
            _applicationManager = applicationManager;
            _applicationUpdater = applicationUpdater;
        }

        public void SetAwb(long applicationId, long? awbId)
        {
            // todo: test
            if (awbId.HasValue)
            {
                var aggregate = _awbRepository.GetAggregate(awbId.Value).First();

	            _applicationUpdater.SetAirWaybill(applicationId, awbId.Value);

	            _applicationManager.SetState(applicationId, aggregate.StateId);

	            _unitOfWork.SaveChanges();
            }
            else
            {
                _applicationUpdater.SetAirWaybill(applicationId, null);

				_applicationManager.SetState(applicationId, _stateConfig.CargoInStockStateId);

                _unitOfWork.SaveChanges();
            }
        }
    }
}