using System;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Client;
using Alicargo.Core.Helpers;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Contracts.Calculation;
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

			var money = CalculationDataHelper.GetMoney(calculation, calculation.InsuranceRate);

			_balance.Decrease(calculation.ClientId,
				money,
				GetComment(EventType.Calculate, calculation),
				DateTimeProvider.Now,
				true);

			return calculation;
		}

		public void CancelCalculatation(long applicationId)
		{
			var calculation = _calculations.GetByApplication(applicationId);

			_service.CancelCalculatation(applicationId);

			var money = CalculationDataHelper.GetMoney(calculation, calculation.InsuranceRate);

			_balance.Increase(calculation.ClientId,
				money,
				GetComment(EventType.CalculationCanceled, calculation),
				DateTimeProvider.Now,
				true);
		}

		private static string GetComment(string type, CalculationData calculation)
		{
			if(calculation.Profit.HasValue)
				return type
				       + Environment.NewLine
				       + Entities.AWB + ": " + calculation.AirWaybillDisplay
				       + Environment.NewLine
				       + Entities.Application + ": " + calculation.ApplicationDisplay
				       + Environment.NewLine
				       + Entities.Sum + ": " + calculation.Profit;

			var profit = calculation.TotalTariffCost ?? 0
			             + calculation.ScotchCost
			             + (decimal)calculation.Weight * calculation.TariffPerKg
			             + (decimal)calculation.InsuranceRate * calculation.Value
			             + calculation.FactureCost
			             + calculation.FactureCostEx
			             + calculation.PickupCost
			             + calculation.TransitCost;

			return string.Format(
				"#{0} | {1:N2} kg * {2:N2}€ = {3:N2}€ | скотч - {4:N2}€ | страховка - {5:N2}€ |" +
				" фактура - {6:N2}€ | доставка - {7:N2}€ | забор с фабрики - {8:N2}€ | Итого - {9:N2}€",
				calculation.ApplicationDisplay,
				calculation.Weight,
				calculation.TariffPerKg,
				calculation.Weight * (double)calculation.TariffPerKg,
				calculation.ScotchCost,
				calculation.InsuranceRate * (double)calculation.Value,
				calculation.FactureCost + calculation.FactureCostEx,
				calculation.TransitCost,
				calculation.PickupCost,
				profit);
		}
	}
}