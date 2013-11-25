using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class MessageFactoryEx : IMessageFactory
	{
		public EmailMessage[] Get(long applicationId, ApplicationEventType type, byte[] data)
		{
			throw new System.NotImplementedException();
		}
	}
}