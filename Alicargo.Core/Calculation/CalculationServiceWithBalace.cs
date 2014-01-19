using System;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Client;
using Alicargo.Core.Helpers;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Resources;
using Alicargo.Utilities;

namespace Alicargo.Core.Calculation
{
	public sealed class CalculationServiceWithBalace : ICalculationService
	{
		private readonly IClientBalance _balance;
		private readonly ICalculationRepository _calculations;
		private readonly ICalculationService _service;

		public CalculationServiceWithBalace(
			ICalculationService service,
			ICalculationRepository calculations,
			IClientBalance balance)
		{
			_service = service;
			_calculations = calculations;
			_balance = balance;
		}

		public CalculationData Calculate(long applicationId)
		{
			var calculation = _service.Calculate(applicationId);

			var money = CalculationDataHelper.GetMoney(calculation);

			_balance.Decrease(calculation.ClientId, money,
				GetComment(EventType.Calculate, calculation), DateTimeProvider.Now, true);

			return calculation;
		}

		public void CancelCalculatation(long applicationId)
		{
			var calculation = _calculations.GetByApplication(applicationId);

			_service.CancelCalculatation(applicationId);

			var money = CalculationDataHelper.GetMoney(calculation);

			_balance.Increase(calculation.ClientId, money,
				GetComment(EventType.CalculationCanceled, calculation), DateTimeProvider.Now, true);
		}

		private static string GetComment(string type, CalculationData calculation)
		{
			return type
			       + Environment.NewLine
			       + Entities.AWB + ": " + calculation.AirWaybillDisplay
			       + Environment.NewLine
			       + Entities.Application + ": " + calculation.ApplicationDisplay;
		}
	}
}