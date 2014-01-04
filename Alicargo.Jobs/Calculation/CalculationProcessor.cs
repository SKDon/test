using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.Calculation
{
	public sealed class CalculationProcessor : IEventProcessor
	{
		private readonly IClientBalance _balance;

		public CalculationProcessor(IClientBalance balance)
		{
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
		}
	}
}