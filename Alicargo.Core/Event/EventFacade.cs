﻿using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Utilities;

namespace Alicargo.Core.Event
{
	public sealed class EventFacade : IEventFacade
	{
		private readonly IPartitionConverter _converter;
		private readonly IEventRepository _events;
		private readonly IIdentityService _identity;
		private readonly ISerializer _serializer;

		public EventFacade(
			IEventRepository events,
			ISerializer serializer,
			IPartitionConverter converter,
			IIdentityService identity)
		{
			_events = events;
			_serializer = serializer;
			_converter = converter;
			_identity = identity;
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
			var userId = _identity.IsAuthenticated ? _identity.Id : (long?)null;

			var partitionId = _converter.GetKey(entityId);

			var entityData = new EventDataForEntity
			{
				EntityId = entityId,
				Data = data
			};

			var bytes = _serializer.Serialize(entityData);

			_events.Add(partitionId, userId, type, state, bytes);
		}
	}
}