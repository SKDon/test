using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Core
{
	public interface IEventProcessor
	{
		void ProcessEvent(EventType type, EventData data);
	}
}