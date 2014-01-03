using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.Calculation
{
	public sealed class CalculationProcessor : IEventProcessor
	{
		private readonly IEventRepository _events;
		private readonly IClientBalanceRepository _balance;

		public CalculationProcessor(IEventRepository events, IClientBalanceRepository balance)
		{
			_events = events;
			_balance = balance;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			switch (type)
			{
				case EventType.Calculate:
					break;

				case EventType.CalculationCanceled:
					break;

				default:
					throw new JobException("Unexpected type " + type + " in CalculationProcessor");
			}

			_events.SetState(data.Id, EventState.Emailing);
		}
	}
}