﻿using Alicargo.Core.Contracts.Event;
using Alicargo.Core.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
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

		public CalculationData Calculate(long applicationId)
		{
			var data = _service.Calculate(applicationId);

			_events.Add(applicationId, EventType.Calculate, EventState.Emailing, data);

			return data;
		}

		public void CancelCalculatation(long applicationId)
		{
			var data = _calculations.GetByApplication(applicationId);

			_service.CancelCalculatation(applicationId);

			_events.Add(applicationId, EventType.CalculationCanceled, EventState.Emailing, data);
		}
	}
}