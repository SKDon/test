using System.Linq;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
    internal sealed class AwbUpdateGtdManager : IAwbUpdateManager
    {
        private readonly IAwbGtdHelper _gtdHelper;
        private readonly IAwbUpdateManager _manager;
        private readonly IAwbRepository _awbRepository;

        public AwbUpdateGtdManager(
            IAwbGtdHelper gtdHelper,
            IAwbUpdateManager manager,
            IAwbRepository awbRepository)
        {
            _gtdHelper = gtdHelper;
            _manager = manager;
            _awbRepository = awbRepository;
        }

        public void Update(long id, AwbAdminModel model)
        {
            var data = _awbRepository.Get(id).First();

            _gtdHelper.ProcessGtd(data, model.GTD);

            _manager.Update(id, model);
        }

        public void Update(long id, AwbBrokerModel model)
        {
            var data = _awbRepository.Get(id).First();

            _gtdHelper.ProcessGtd(data, model.GTD);

            _manager.Update(id, model);
        }

	    public void Update(long id, AwbSenderModel model)
	    {
			_manager.Update(id, model);
	    }

	    public void SetAdditionalCost(long awbId, decimal? additionalCost)
	    {
		    _manager.SetAdditionalCost(awbId, additionalCost);
	    }
    }
}