using System.Linq;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbUpdateManagerWithEvent : IAwbUpdateManager
	{
		private readonly IAwbRepository _awbs;
		private readonly IEventFacade _events;
		private readonly IAwbUpdateManager _manager;

		public AwbUpdateManagerWithEvent(
			IAwbRepository awbs,
			IEventFacade events,
			IAwbUpdateManager manager)
		{
			_awbs = awbs;
			_events = events;
			_manager = manager;
		}

		public void Update(long id, AwbAdminModel model)
		{
			var old = _awbs.Get(id).First();

			AddBrokerEvent(id, old.BrokerId, model.BrokerId);

			_manager.Update(id, model);
		}

		public void Update(long id, AwbBrokerModel model)
		{
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

		private void AddBrokerEvent(long awbId, long? oldBrokerId, long? newBrokerId)
		{
			if(newBrokerId.HasValue && oldBrokerId != newBrokerId)
			{
				_events.Add(awbId, EventType.SetBroker, EventState.Emailing);
			}
		}
	}
}