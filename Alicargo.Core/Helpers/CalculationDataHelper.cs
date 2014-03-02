using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Core.Helpers
{
	public static class CalculationDataHelper
	{
		public static decimal GetMoney(CalculationData calculation, float insuranceRate)
		{
			return (decimal)calculation.Weight * calculation.TariffPerKg
				   + calculation.ScotchCost
				   + calculation.Value * (decimal)insuranceRate
				   + calculation.FactureCost
				   + calculation.FactureCostEx
				   + calculation.TransitCost
				   + calculation.PickupCost;
		}
	}
}