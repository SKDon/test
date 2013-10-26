using System.Collections.Generic;
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

		public static decimal? GetSenderScotchCost(IReadOnlyDictionary<long, decimal> tariffs, long? senderId, int? count)
		{
			return senderId.HasValue ? tariffs[senderId.Value] * (count ?? 0) : (decimal?)null;
		}

		public static decimal GetProfit(ApplicationData application, IReadOnlyDictionary<long, decimal> tariffs)
		{
			return GetTotalTariffCost(application.TariffPerKg, application.Weigth)
				   + (application.ScotchCostEdited ?? GetSenderScotchCost(tariffs, application.SenderId, application.Count) ?? 0)
				   + GetInsuranceCost(application.Value)
				   + (application.FactureCostEdited ?? application.FactureCost ?? 0)
				   + (application.WithdrawCostEdited ?? application.WithdrawCost ?? 0)
				   + (application.TransitCost ?? 0);
		}

		public static decimal GetProfit(ApplicationListItemData application)
		{
			return GetTotalTariffCost(application.TariffPerKg, application.Weigth)
				   + (application.ScotchCost ?? 0)
				   + GetInsuranceCost(application.Value)
				   + (application.FactureCost ?? 0)
				   + (application.WithdrawCost ?? 0)
				   + (application.TransitCost ?? 0);
		}
	}
}