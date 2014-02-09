using System.Linq;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Event;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationAwbManager : IApplicationAwbManager
	{
		private readonly IAdminApplicationManager _manager;
		private readonly IEventFacade _events;
		private readonly IApplicationEditor _editor;
		private readonly IAwbRepository _awbs;
		private readonly IStateConfig _config;

		public ApplicationAwbManager(
			IAwbRepository awbs,
			IStateConfig config,
			IAdminApplicationManager manager,
			IEventFacade events,
			IApplicationEditor editor)
		{
			_awbs = awbs;
			_config = config;
			_manager = manager;
			_events = events;
			_editor = editor;
		}

		public void SetAwb(long applicationId, long? awbId)
		{
			if(awbId.HasValue)
			{
				var aggregate = _awbs.GetAggregate(new[] { awbId.Value }).First();

				_editor.SetAirWaybill(applicationId, awbId.Value);

				_manager.SetState(applicationId, aggregate.StateId);

				_events.Add(applicationId, EventType.SetAwb, EventState.Emailing, awbId.Value);
			}
			else
			{
				_editor.SetAirWaybill(applicationId, null);

				_manager.SetState(applicationId, _config.CargoInStockStateId);
			}
		}
	}
}