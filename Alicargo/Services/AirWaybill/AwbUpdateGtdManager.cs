using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
    // todo: 1.1. registration
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

        public void Update(long id, AirWaybillEditModel model)
        {
            var data = _awbRepository.Get(id).First();

            _gtdHelper.ProcessGtd(data, model.GTD);

            _manager.Update(id, model);
        }

        public void Update(long id, BrockerAWBModel model)
        {
            var data = _awbRepository.Get(id).First();

            _gtdHelper.ProcessGtd(data, model.GTD);

            _manager.Update(id, model);
        }
    }
}