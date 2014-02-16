using System.Collections.Generic;

namespace Alicargo.Core.Calculation
{
	public static class CalculationHelper
	{
		public static decimal GetInsuranceCost(decimal value, decimal insuranceRate)
		{
			return value * insuranceRate;
		}

		public static decimal? GetSenderScotchCost(IReadOnlyDictionary<long, decimal> tariffs, long? senderId, int? count)
		{
			return senderId.HasValue ? tariffs[senderId.Value] * (count ?? 0) : (decimal?)null;
		}

		public static decimal GetTotalSenderRate(decimal? senderRate, float? weight)
		{
			return (senderRate ?? 0) * (decimal)(weight ?? 0);
		}

		public static decimal GetTotalTariffCost(decimal? tariffPerKg, float? weight)
		{
			return (tariffPerKg ?? 0) * (decimal)(weight ?? 0);
		}
	}
}