using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.Calculation
{
	public sealed class CalculationProcessor : IEventProcessor
	{
		private readonly IEventRepository _events;

		public CalculationProcessor(IEventRepository events)
		{
			_events = events;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			// поправить баланс
			
			_events.SetState(data.Id, EventState.Emailing);
		}
	}
}