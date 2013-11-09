using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents
{
	public interface IMessageFactory
	{
		EmailMessage[] Get(ApplicationEventType eventType, byte[] data);
		EmailMessage Get(EmailMessageData data);
	}
}
