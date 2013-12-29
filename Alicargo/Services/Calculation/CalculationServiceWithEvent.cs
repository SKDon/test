using Alicargo.Contracts.Enums;
using Alicargo.Core.Event;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Calculation
{
	internal sealed class CalculationServiceWithEvent : ICalculationService
	{
		private readonly IEventFacadeForApplication _events;
		private readonly ICalculationService _service;

		public CalculationServiceWithEvent(ICalculationService service, IEventFacadeForApplication events)
		{
			_service = service;
			_events = events;
		}

		public void Calculate(long applicationId)
		{
			_service.Calculate(applicationId);

			_events.Add(applicationId, EventType.Calculate, EventState.Default);
		}

		public void CancelCalculatation(long applicationId)
		{
			_service.CancelCalculatation(applicationId);

			_events.Add(applicationId, EventType.CalculationCanceled, EventState.Default);
		}
	}
}