using System.Linq;

namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationAwb
	{
		private CalculationDetailsItem[] _rows;
		private float _totalWeight;

		public CalculationAwb()
		{
			Rows = new CalculationDetailsItem[0];
		}

		public CalculationDetailsItem[] Rows
		{
			get { return _rows; }
			set
			{
				_rows = value;
				CalculateTotalWeigth();
			}
		}

		public long AwbId { get; set; }

		public string AwbDisplay { get; set; }

		public decimal? CostPerKgOfSender
		{
			get
			{
				if (_totalWeight == 0)
				{
					return null;
				}

				return TotalCostOfSenderForWeight / (decimal)_totalWeight;
			}
		}

		public decimal? TotalCostOfSenderForWeight { get; set; }

		public decimal? TotalScotchCost
		{
			get { return Rows.Sum(x => x.ScotchCost); }
		}

		public decimal? TotalFactureCost
		{
			get { return Rows.Sum(x => x.FactureCost); }
		}

		public decimal? TotalWithdrawCost
		{
			get { return Rows.Sum(x => x.WithdrawCost); }
		}

		public decimal? TotalTransitCost
		{
			get { return Rows.Sum(x => x.TransitCost); }
		}

		public decimal? TotalOfSender
		{
			get { return TotalCostOfSenderForWeight + TotalScotchCost + TotalFactureCost + TotalWithdrawCost; }
		}

		public decimal? TotalForwarderCost
		{
			get { return Rows.Sum(x => x.ForwarderCost); }
		}

		public decimal? AdditionalCost { get; set; }

		public decimal TotalExpenses
		{
			get
			{
				var insuranceCost = Rows.Sum(x => x.InsuranceCost ?? 0);
				return (TotalOfSender ?? 0) + (FlightCost ?? 0) + (CustomCost ?? 0) + (BrokerCost ?? 0) + insuranceCost
					   + (TotalForwarderCost ?? 0) + (AdditionalCost ?? 0);
			}
		}

		public decimal Profit
		{
			get { return Rows.Sum(x => x.Profit) - TotalExpenses; }
		}

		public decimal ProfitPerKg
		{
			get
			{
				if (_totalWeight == 0)
				{
					return 0;
				}
				return Profit / (decimal)_totalWeight;
			}
		}

		public decimal? FlightCost { get; set; }

		public decimal? FlightCostPerKg
		{
			get
			{
				if (_totalWeight == 0)
				{
					return null;
				}

				return FlightCost / (decimal)_totalWeight;
			}
		}

		public decimal? CustomCost { get; set; }

		public decimal? CustomCostPerKg
		{
			get
			{
				if (_totalWeight == 0)
				{
					return null;
				}

				return CustomCost / (decimal)_totalWeight;
			}
		}

		public decimal? BrokerCost { get; set; }

		public decimal? BrokerCostPerKg
		{
			get
			{
				if (_totalWeight == 0)
				{
					return null;
				}

				return BrokerCost / (decimal)_totalWeight;
			}
		}

		private void CalculateTotalWeigth()
		{
			_totalWeight = Rows.Sum(x => x.Weigth ?? 0);
		}
	}
}