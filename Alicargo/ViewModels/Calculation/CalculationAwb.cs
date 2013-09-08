using System.Linq;

namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationAwb
	{
		public CalculationAwb()
		{
			Rows = new CalculationListItem[0];
		}

		public CalculationListItem[] Rows { get; set; }

		public string AwbDisplay { get; set; }

		public decimal? CostPerKgOfSender
		{
			get
			{
				var totalWeight = Rows.Sum(x => x.Weigth ?? 0);

				if (totalWeight == 0)
				{
					return null;
				}

				return TotalWeightCostOfSender / (decimal)totalWeight;
			}
		}

		public decimal? TotalWeightCostOfSender { get; set; }

		public decimal? TotalScotchCost
		{
			get
			{
				return Rows.Sum(x => x.ScotchCost);
			}
		}

		public decimal? TotalFactureCost
		{
			get
			{
				return Rows.Sum(x => x.FactureCost);
			}
		}

		public decimal? TotalWithdrawCost
		{
			get
			{
				return Rows.Sum(x => x.WithdrawCost);
			}
		}

		public decimal? TotalTransitCost
		{
			get
			{
				return Rows.Sum(x => x.TransitCost);
			}
		}
	}
}