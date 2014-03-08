using Alicargo.Core.Calculation;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Core.Helpers
{
	public static class CalculationDataHelper
	{
		public static decimal GetMoney(CalculationData calculation, float insuranceRate)
		{
			return calculation.Profit
			       ?? CalculationHelper.GetTotalTariffCost(
				       calculation.TotalTariffCost,
				       calculation.TariffPerKg,
				       calculation.Weight)
			       + calculation.ScotchCost
			       + calculation.Value * (decimal)insuranceRate
			       + calculation.FactureCost
			       + calculation.FactureCostEx
			       + calculation.TransitCost
			       + calculation.PickupCost;
		}
	}
}