using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Event;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Calculation
{
	internal sealed class CalculationServiceWithEvent : ICalculationService
	{
		private readonly ICalculationRepository _calculations;
		private readonly IEventFacade _events;
		private readonly ICalculationService _service;

		public CalculationServiceWithEvent(
			ICalculationService service,
			IEventFacade events,
			ICalculationRepository calculations)
		{
			_service = service;
			_events = events;
			_calculations = calculations;
		}

		public void Calculate(long applicationId)
		{
			_service.Calculate(applicationId);

			var data = _calculations.GetByApplication(applicationId);

			_events.Add(applicationId, EventType.Calculate, EventState.Calculating, data);
		}

		public void CancelCalculatation(long applicationId)
		{
			var data = _calculations.GetByApplication(applicationId);

			_service.CancelCalculatation(applicationId);

			_events.Add(applicationId, EventType.CalculationCanceled, EventState.Calculating, data);
		}
	}
}