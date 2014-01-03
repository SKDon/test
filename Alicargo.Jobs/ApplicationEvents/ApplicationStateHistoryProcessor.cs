using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationStateHistoryProcessor : IEventProcessor
	{
		public void ProcessEvent(EventType type, EventData data)
		{
		}
	}
}