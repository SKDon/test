using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents
{
	public interface IMessageFactory
	{
		Message Get(ApplicationEventType eventType, byte[] data);
	}
}
