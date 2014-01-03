using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationMailCreatorProcessor : IEventProcessor
	{
		private readonly IMailSender _sender;
		private readonly IEventRepository _events;
		private readonly IMessageBuilder _messageBuilder;

		public ApplicationMailCreatorProcessor(
			IMessageBuilder messageBuilder,
			IMailSender sender,
			IEventRepository events)
		{
			_sender = sender;
			_messageBuilder = messageBuilder;
			_events = events;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var messages = _messageBuilder.Get(type, data);

			if (messages != null)
			{
				_sender.Send(messages);
			}

			_events.SetState(data.Id, EventState.StateHistorySaving);
		}
	}
}