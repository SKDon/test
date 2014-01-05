using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Resources;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Calculation
{
	internal sealed class CalculationServiceWithBalace : ICalculationService
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

			var money = GetMoney(calculation);

			_balance.Decrease(calculation.ClientId, money,
				GetComment(Contracts.Resources.EventType.CalculationCanceled, calculation), DateTimeOffset.UtcNow);

			return calculation;
		}

		public void CancelCalculatation(long applicationId)
		{
			var calculation = _calculations.GetByApplication(applicationId);

			_service.CancelCalculatation(applicationId);

			var money = GetMoney(calculation);

			_balance.Increase(calculation.ClientId, money,
				GetComment(Contracts.Resources.EventType.CalculationCanceled, calculation), DateTimeOffset.UtcNow);
		}

		private static string GetComment(string type, CalculationData calculation)
		{
			return type
			       + Environment.NewLine
			       + Entities.AWB + ": " + calculation.AirWaybillDisplay
			       + Environment.NewLine
			       + Entities.Application + ": " + calculation.ApplicationDisplay;
		}

		private static decimal GetMoney(CalculationData calculation)
		{
			return (decimal)calculation.Weight * calculation.TariffPerKg + calculation.ScotchCost
			       + calculation.InsuranceCost + calculation.FactureCost + calculation.TransitCost + calculation.PickupCost;
		}
	}
}