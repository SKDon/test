using System.Linq;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.Core.State
{
	public sealed class AwbStateManager : IAwbStateManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IApplicationStateManager _states;

		public AwbStateManager(
			IAwbRepository awbs,
			IApplicationRepository applications,
			IApplicationStateManager states)
		{
			_awbs = awbs;
			_applications = applications;
			_states = states;
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
			if(stateId == oldStateId)
			{
				return;
			}

			var applications = _applications.GetByAirWaybill(airWaybillId);
			foreach(var application in applications)
			{
				if(oldStateId == application.StateId)
				{
					_states.SetState(application.Id, stateId);
				}
			}
		}
	}
}