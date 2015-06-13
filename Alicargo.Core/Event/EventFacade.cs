using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Utilities;

namespace Alicargo.Core.Event
{
	public sealed class EventFacade : IEventFacade
	{
		private readonly IApplicationRepository _applications;
		private readonly IPartitionConverter _converter;
		private readonly IEventRepository _events;
		private readonly IIdentityService _identity;
		private readonly ISenderRepository _senders;
		private readonly ISerializer _serializer;

		public EventFacade(
			IEventRepository events,
			ISerializer serializer,
			IPartitionConverter converter,
			IIdentityService identity,
			IApplicationRepository applications,
			ISenderRepository senders)
		{
			_events = events;
			_serializer = serializer;
			_converter = converter;
			_identity = identity;
			_applications = applications;
			_senders = senders;
		}

		public void Add<T>(long entityId, EventType type, EventState state, T obj)
		{
			var data = _serializer.Serialize(obj);

			AddImpl(entityId, type, state, data);
		}

		public void Add(long entityId, EventType type, EventState state)
		{
			AddImpl(entityId, type, state, null);
		}

		private void AddImpl(long entityId, EventType type, EventState state, byte[] data)
		{
			var senderUserId = GetSenderUserId(entityId, type);

			var currentUserId = senderUserId ?? (_identity.IsAuthenticated ? _identity.Id : (long?)null);

			var partitionId = _converter.GetKey(entityId);

			var entityData = new EventDataForEntity
			{
				EntityId = entityId,
				Data = data
			};

			var bytes = _serializer.Serialize(entityData);

			_events.Add(partitionId, currentUserId, type, state, bytes);
		}

		private long? GetSenderUserId(long entityId, EventType type)
		{
			long? senderIdInEntity = null;
			if(EventHelper.ApplicationEventTypes.Contains(type))
			{
				senderIdInEntity = _applications.Get(entityId).SenderId;
			}
			else if(EventHelper.AwbEventTypes.Contains(type))
			{
				senderIdInEntity = _applications.GetByAirWaybill(entityId).Select(x => x.SenderId).FirstOrDefault();
			}
			
			long? senderUserId = null;
			if(senderIdInEntity.HasValue)
			{
				senderUserId = _senders.GetUserId(senderIdInEntity.Value);
			}

			return senderUserId;
		}
	}
}