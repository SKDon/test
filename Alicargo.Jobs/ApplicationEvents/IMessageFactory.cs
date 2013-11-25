using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents
{
	public interface IMessageFactory
	{
		EmailMessage[] Get(long applicationId, ApplicationEventType type, byte[] data);
	}
}
