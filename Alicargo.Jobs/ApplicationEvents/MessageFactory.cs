using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class MessageFactory: IMessageFactory
	{
		public Message Get(ApplicationEventType eventType, byte[] data)
		{
			throw new System.NotImplementedException();
		}
	}
}