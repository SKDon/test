using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Balance.Helpers;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Balance
{
	internal sealed class BalanceEmailCreatorProcessor : IEventProcessor
	{
		private readonly IMailSender _sender;
		private readonly IEventRepository _events;
		private readonly IMessageBuilder _messageBuilder;
		private readonly ISerializer _serializer;

		public BalanceEmailCreatorProcessor(
			IEventRepository events,
			ISerializer serializer, 
			IMailSender sender, 
			IMessageBuilder messageBuilder)
		{
			_events = events;
			_serializer = serializer;
			_sender = sender;
			_messageBuilder = messageBuilder;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var eventData = _serializer.Deserialize<EventDataForEntity>(data.Data);

			var messages = _messageBuilder.Get(type, data);

			if (messages != null)
			{
				_sender.Send(messages);
			}

			_events.Delete(data.Id);
		}
	}
}