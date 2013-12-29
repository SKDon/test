using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface IMessageFactory
	{
		EmailMessage[] Get(long applicationId, EventType type, byte[] data);
	}
}
