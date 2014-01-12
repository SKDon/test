using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Contracts.Event;
using Alicargo.Utilities;

namespace Alicargo.Core.Event
{
	public sealed class EventFacade : IEventFacade
	{
		private readonly IPartitionConverter _converter;
		private readonly IEventRepository _events;
		private readonly ISerializer _serializer;

		public EventFacade(
			IEventRepository events,
			ISerializer serializer,
			IPartitionConverter converter)
		{
			_events = events;
			_serializer = serializer;
			_converter = converter;
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
			var partitionId = _converter.GetKey(entityId);

			var entityData = new EventDataForEntity
			{
				EntityId = entityId,
				Data = data
			};

			var bytes = _serializer.Serialize(entityData);

			_events.Add(partitionId, type, state, bytes);
		}
	}
}