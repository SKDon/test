using Alicargo.Core.Contracts;
using Alicargo.Core.Contracts.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Helpers
{
	internal sealed class DefaultEmailingProcessor : IEventProcessor
	{
		private readonly IMessageBuilder _messageBuilder;
		private readonly IMailSender _sender;

		public DefaultEmailingProcessor(
			IMailSender sender,
			IMessageBuilder messageBuilder)
		{
			_sender = sender;
			_messageBuilder = messageBuilder;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var messages = _messageBuilder.Get(type, data);

			if (messages != null)
			{
				_sender.Send(messages);
			}
		}
	}
}