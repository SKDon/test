using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class MessageFactory: IMessageFactory
	{
		public EmailMessage Get(ApplicationEventType eventType, byte[] data)
		{
			throw new System.NotImplementedException();
		}

		public EmailMessage Get(EmailMessageData data)
		{
			throw new System.NotImplementedException();
		}
	}
}