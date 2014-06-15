using System.Collections.Generic;

namespace Alicargo.Core.Calculation
{
	public static class CalculationHelper
	{
		public static decimal GetInsuranceCost(decimal value, float insuranceRate)
		{
			return value * (decimal)insuranceRate;
		}

		public static decimal? GetSenderScotchCost(IReadOnlyDictionary<long, decimal> tariffs, long? senderId, int? count)
		{
			return senderId.HasValue && tariffs.ContainsKey(senderId.Value)
				? tariffs[senderId.Value] * (count ?? 0)
				: (decimal?)null;
		}

		public static decimal GetTotalSenderRate(decimal? senderRate, float? weight)
		{
			return (senderRate ?? 0) * (decimal)(weight ?? 0);
		}

		public static decimal GetTotalTariffCost(decimal? calculationTotalTariffCost, decimal? tariffPerKg, float? weight)
		{
			return calculationTotalTariffCost ?? ((tariffPerKg ?? 0) * (decimal)(weight ?? 0));
		}
	}
}