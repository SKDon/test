using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationStateHistoryProcessor : IEventProcessor
	{
		private readonly IEventRepository _events;

		public ApplicationStateHistoryProcessor(IEventRepository events)
		{
			_events = events;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			_events.Delete(data.Id);
		}
	}
}