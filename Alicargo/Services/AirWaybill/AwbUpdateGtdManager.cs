using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services.AirWaybill
{
    // todo: 2. registration
    internal sealed class AwbUpdateGtdManager : IAwbUpdateManager
    {
        private readonly IAwbUpdateManager _manager;
        private readonly IAwbStateManager _stateManager;
        private readonly IAwbRepository _awbRepository;
        private readonly IStateConfig _stateConfig;

        public AwbUpdateGtdManager(
            IAwbUpdateManager manager,
            IAwbStateManager stateManager,
            IAwbRepository awbRepository,
            IStateConfig stateConfig)
        {
            _manager = manager;
            _stateManager = stateManager;
            _awbRepository = awbRepository;
            _stateConfig = stateConfig;
        }

        public void Update(long id, AirWaybillEditModel model)
        {
            var data = _awbRepository.Get(id).First();

            ProcessGtd(data, model.GTD);

            _manager.Update(id, model);
        }

        public void Update(long id, BrockerAWBModel model)
        {
            var data = _awbRepository.Get(id).First();

            ProcessGtd(data, model.GTD);

            _manager.Update(id, model);
        }

        private void ProcessGtd(AirWaybillData data, string newGtd)
        {
            // todo: 1. test
            if (!IsReadyForCargoAtCustomsStateId(data.GTD, newGtd)) return;

            // todo: 2. check order of states and return if current state is supper than CargoAtCustomsStateId
            if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId) return;

            _stateManager.SetState(data.Id, _stateConfig.CargoAtCustomsStateId);
        }

        private static bool IsReadyForCargoAtCustomsStateId(string oldGtd, string newGtd)
        {
            return oldGtd.IsNullOrWhiteSpace() && !newGtd.IsNullOrWhiteSpace();
        }
    }
}