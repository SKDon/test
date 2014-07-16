using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Awb;

namespace Alicargo.Core.AirWaybill
{
	public sealed class AwbGtdHelper : IAwbGtdHelper
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
			if(!IsReadyForCargoAtCustomsStateId(data.GTD, newGtd)) return;

			// todo: 1. check order of states and return if current state is supper than CargoAtCustomsStateId (257)
			if(data.StateId == _stateConfig.CargoIsCustomsClearedStateId) return;

			_stateManager.SetState(data.Id, _stateConfig.CargoAtCustomsStateId);
		}

		private static bool IsReadyForCargoAtCustomsStateId(string oldGtd, string newGtd)
		{
			return string.IsNullOrWhiteSpace(oldGtd) && !string.IsNullOrWhiteSpace(newGtd);
		}
	}
}