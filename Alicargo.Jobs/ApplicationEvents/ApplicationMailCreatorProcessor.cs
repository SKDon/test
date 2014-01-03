using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationMailCreatorProcessor : IEventProcessor
	{
		private readonly IMailSender _sender;
		private readonly IEventRepository _events;
		private readonly IMessageFactory _messageFactory;
		private readonly ISerializer _serializer;

		public ApplicationMailCreatorProcessor(
			IMessageFactory messageFactory,
			IMailSender sender,
			IEventRepository events,
			ISerializer serializer)
		{
			_sender = sender;
			_messageFactory = messageFactory;
			_events = events;
			_serializer = serializer;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var applicationEventData = _serializer.Deserialize<EventDataForEntity>(data.Data);

			var messages = _messageFactory.Get(applicationEventData.EntityId, type, applicationEventData.Data);

			if (messages != null)
			{
				_sender.Send(messages);
			}

			_events.SetState(data.Id, EventState.StateHistorySaving);
		}
	}
}