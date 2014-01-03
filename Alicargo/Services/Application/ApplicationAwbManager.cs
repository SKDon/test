using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.Application;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Application
{
    public sealed class ApplicationAwbManager : IApplicationAwbManager
    {
        private readonly IApplicationManager _applicationManager;
        private readonly IApplicationUpdateRepository _applicationUpdater;
        private readonly IAwbRepository _awbRepository;
	    private readonly IStateConfig _stateConfig;
	    private readonly IUnitOfWork _unitOfWork;

        public ApplicationAwbManager(
            IAwbRepository awbRepository,
			IStateConfig stateConfig,
            IUnitOfWork unitOfWork,
            IApplicationManager applicationManager,
            IApplicationUpdateRepository applicationUpdater)
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