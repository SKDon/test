using Alicargo.ViewModels.Helpers;

namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationItem
	{
		public const decimal InsuranceRate = 100;

		public string ClientNic { get; set; }

		#region Application data

		public long ApplicationId { get; set; }
		public string Factory { get; set; }
		public string Mark { get; set; }
		public int? Count { get; set; }
		public float? Weigth { get; set; }
		public string Invoice { get; set; }
		public decimal Value { get; set; }
		public int ValueCurrencyId { get; set; }
		public decimal? ScotchCost { get; set; }
		public decimal? FactureCost { get; set; }
		public decimal? WithdrawCost { get; set; }
		public decimal? TransitCost { get; set; }
		public decimal? ForwarderCost { get; set; }
		public decimal? TariffPerKg { get; set; }
		public decimal? SenderRate { get; set; }
		public long AirWaybillId { get; set; }

		#endregion

		public string DisplayNumber
		{
			get
			{
				return ApplicationHelper.GetDisplayNumber(ApplicationId, Count);
			}
		}

		public decimal? TotalTariffCost
		{
			get
			{
				return TariffPerKg * (decimal?)Weigth;
			}
		}

		public decimal? TotalSenderRate
		{
			get
			{
				return SenderRate * (decimal?)Weigth;
			}
		}

		public decimal? InsuranceCost
		{
			get
			{
				if (Value == 0)
				{
					return null;
				}

				return Value / InsuranceRate;
			}
		}

		public decimal Profit
		{
			get
			{
				// todo: 3. supposed that all costs in euro but ValueCurrencyId can be in any other currency
				return (TotalTariffCost ?? 0) + (ScotchCost ?? 0) + (InsuranceCost ?? 0) + (FactureCost ?? 0) + (WithdrawCost ?? 0) + (TransitCost ?? 0);
			}
		}
	}
}