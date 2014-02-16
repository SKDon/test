using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.Core.Calculation
{
	public static class CalculationHelper
	{
		public static decimal GetTotalTariffCost(decimal? tariffPerKg, float? weight)
		{
			return (tariffPerKg ?? 0) * (decimal)(weight ?? 0);
		}

		public static decimal GetTotalSenderRate(decimal? senderRate, float? weight)
		{
			return (senderRate ?? 0) * (decimal)(weight ?? 0);
		}

		public static decimal GetInsuranceCost(decimal value, float insuranceRate)
		{
			return value * (decimal)insuranceRate;
		}

		public static decimal? GetSenderScotchCost(IReadOnlyDictionary<long, decimal> tariffs, long? senderId, int? count)
		{
			return senderId.HasValue ? tariffs[senderId.Value] * (count ?? 0) : (decimal?)null;
		}

		public static decimal GetProfit(ApplicationData application, IReadOnlyDictionary<long, decimal> tariffs)
		{
			return GetTotalTariffCost(application.TariffPerKg, application.Weight)
				   + (application.ScotchCostEdited ?? GetSenderScotchCost(tariffs, application.SenderId, application.Count) ?? 0)
				   + GetInsuranceCost(application.Value, application.InsuranceRate)
				   + (application.FactureCostEdited ?? application.FactureCost ?? 0)
				   + (application.FactureCostExEdited ?? application.FactureCostEx ?? 0)
				   + (application.PickupCostEdited ?? application.PickupCost ?? 0)
				   + (application.TransitCostEdited ?? application.TransitCost ?? 0);
		}		
	}
}