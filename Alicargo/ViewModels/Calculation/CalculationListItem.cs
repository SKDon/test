namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationListItem
	{
		public const decimal InsuranceRate = 100;

		public CalculationListItem()
		{
			Value = new CurrencyModel();
		}

		public string ClientNic { get; set; }

		#region Application data

		public string DisplayNumber { get; set; }
		public string Factory { get; set; }
		public string Mark { get; set; }
		public int? Count { get; set; }
		public float? Weigth { get; set; }
		public string Invoce { get; set; }
		public CurrencyModel Value { get; set; }
		public decimal? ScotchCost { get; set; }
		public decimal? FactureCost { get; set; }
		public decimal? WithdrawCost { get; set; }
		public decimal? TransitCost { get; set; }
		public decimal? TariffPerKg { get; set; }

		#endregion

		public decimal? TotalTariffCost
		{
			get
			{
				return TariffPerKg * Count;
			}
		}

		public CurrencyModel InsuranceCost
		{
			get
			{
				return new CurrencyModel
				{
					CurrencyId = Value.CurrencyId,
					Value = Value.Value / InsuranceRate
				};
			}
		}

		public decimal TotalCost {
			get
			{
				// todo: 3. supposed that all costs in euro but Insurance Cost can be in any other currency
				return TotalTariffCost ?? 0 + ScotchCost ?? 0 + InsuranceCost.Value + FactureCost ?? 0 + WithdrawCost ?? 0 + TransitCost ?? 0;
			}
		}
	}
}