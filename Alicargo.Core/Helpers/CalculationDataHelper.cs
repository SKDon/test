using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Core.Helpers
{
	public static class CalculationDataHelper
	{
		public static decimal GetMoney(CalculationData calculation)
		{
			return (decimal)calculation.Weight * calculation.TariffPerKg + calculation.ScotchCost
			       + calculation.InsuranceCost + calculation.FactureCost + calculation.TransitCost + calculation.PickupCost;
		}
	}
}