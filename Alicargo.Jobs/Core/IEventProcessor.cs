using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.Core
{
	public interface IEventProcessor
	{
		void ProcessEvent(EventType type, EventData data);
	}
}