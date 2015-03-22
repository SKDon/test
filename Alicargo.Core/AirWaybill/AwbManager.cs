using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Event;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.Core.AirWaybill
{
	public sealed class AwbManager : IAwbManager
	{
		private readonly IApplicationAwbManager _applicationAwbManager;
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IApplicationEditor _editor;
		private readonly IEventFacade _events;
		private readonly IStateConfig _stateConfig;

		public AwbManager(
			IAwbRepository awbs,
			IEventFacade events,
			IStateConfig stateConfig,
			IApplicationAwbManager applicationAwbManager,
			IApplicationRepository applications,
			IApplicationEditor editor)
		{
			_awbs = awbs;
			_events = events;
			_stateConfig = stateConfig;
			_applicationAwbManager = applicationAwbManager;
			_applications = applications;
			_editor = editor;
		}

		public long Create(long? applicationId, AirWaybillEditData data, long creatorUserId)
		{
			if(data.GTD != null)
			{
				throw new InvalidLogicException("GTD data should be defined by update");
			}

			var id = _awbs.Add(data, _stateConfig.CargoIsFlewStateId, creatorUserId);

			if(applicationId.HasValue)
			{
				_applicationAwbManager.SetAwb(applicationId.Value, id);
			}

			if(data.BrokerId.HasValue)
			{
				_events.Add(id, EventType.SetBroker, EventState.Emailing);
			}

			_events.Add(id, EventType.AwbCreated, EventState.Emailing, data);

			return id;
		}

		public void Delete(long awbId)
		{
			var applicationDatas = _applications.GetByAirWaybill(awbId);

			foreach(var app in applicationDatas)
			{
				_editor.SetAirWaybill(app.Id, null);
			}

			_awbs.Delete(awbId);
		}
	}
}