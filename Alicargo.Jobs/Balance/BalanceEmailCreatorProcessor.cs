using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Balance
{
	internal sealed class BalanceEmailCreatorProcessor : IEventProcessor
	{
		private readonly IMailSender _sender;
		private readonly IMessageBuilder _messageBuilder;

		public BalanceEmailCreatorProcessor(
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