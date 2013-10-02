using Alicargo.Contracts.Contracts;

namespace Alicargo.ViewModels.Calculation
{
	internal static class CalculationHelper
	{
		public const decimal InsuranceRate = 100;

		public static decimal GetTotalTariffCost(decimal? tariffPerKg, float? weigth)
		{
			return (tariffPerKg ?? 0) * (decimal)(weigth ?? 0);
		}

		public static decimal GetTotalSenderRate(decimal? senderRate, float? weigth)
		{
			return (senderRate ?? 0) * (decimal)(weigth ?? 0);
		}

		public static decimal GetInsuranceCost(decimal value)
		{
			return value / InsuranceRate;
		}

		public static decimal GetProfit(ApplicationData application)
		{
			// todo: 3. supposed that all costs in euro but ValueCurrencyId can be in any other currency
			return GetTotalTariffCost(application.TariffPerKg, application.Weigth) + (application.ScotchCost ?? 0)
				   + GetInsuranceCost(application.Value) + (application.FactureCost ?? 0) + (application.WithdrawCost ?? 0)
				   + (application.TransitCost ?? 0);
		}
	}
}