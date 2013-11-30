using Alicargo.Contracts.Contracts;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services.AirWaybill
{
    internal sealed class AwbGtdHelper : IAwbGtdHelper
    {
        private readonly IAwbStateManager _stateManager;
        private readonly IStateConfig _stateConfig;

        public AwbGtdHelper(IAwbStateManager stateManager, IStateConfig stateConfig)
        {
            _stateManager = stateManager;
            _stateConfig = stateConfig;
        }

        public void ProcessGtd(AirWaybillData data, string newGtd)
        {
            if (!IsReadyForCargoAtCustomsStateId(data.GTD, newGtd)) return;

            // todo: check order of states and return if current state is supper than CargoAtCustomsStateId
            if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId) return;

            _stateManager.SetState(data.Id, _stateConfig.CargoAtCustomsStateId);
        }

        private static bool IsReadyForCargoAtCustomsStateId(string oldGtd, string newGtd)
        {
            return oldGtd.IsNullOrWhiteSpace() && !newGtd.IsNullOrWhiteSpace();
        }
    }
}