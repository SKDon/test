using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Core.Helpers
{
	public static class CalculationDataHelper
	{
		public static decimal GetMoney(CalculationData calculation, decimal insuranceRate)
		{
			return (decimal)calculation.Weight * calculation.TariffPerKg
				   + calculation.ScotchCost
				   + calculation.Value * insuranceRate
				   + calculation.FactureCost
				   + calculation.FactureCostEx
				   + calculation.TransitCost
				   + calculation.PickupCost;
		}
	}
}