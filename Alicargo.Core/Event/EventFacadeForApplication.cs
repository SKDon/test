using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Core.Event
{
	public sealed class EventFacadeForApplication : IEventFacadeForApplication
	{
		private readonly IPartitionConverter _converter;
		private readonly IEventRepository _events;
		private readonly ISerializer _serializer;

		public EventFacadeForApplication(
			IEventRepository events,
			ISerializer serializer,
			IPartitionConverter converter)
		{
			_events = events;
			_serializer = serializer;
			_converter = converter;
		}

		public void Add<T>(long applicationId, EventType type, EventState state, T obj)
		{
			var data = _serializer.Serialize(obj);

			AddImpl(applicationId, type, state, data);
		}

		public void Add(long applicationId, EventType type, EventState state)
		{
			AddImpl(applicationId, type, state, null);
		}

		private void AddImpl(long applicationId, EventType type, EventState state, byte[] data)
		{
			_events.Add(
				_converter.GetKey(applicationId),
				type, state,
				_serializer.Serialize(
					new EventDataForApplication
					{
						ApplicationId = applicationId,
						Data = data
					}));
		}
	}
}