using System.Linq;

namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationAwbInfo
	{
		private readonly CalculationItem[] _rows;
		private float _totalWeight;

		public CalculationAwbInfo(CalculationItem[] rows)
		{
			_rows = rows;
			CalculateTotalWeigth();
		}

		public long AirWaybillId { get; set; }

		public decimal? CostPerKgOfSender
		{
			get
			{
				if (_totalWeight == 0)
				{
					return null;
				}

				return TotalSenderRate / (decimal)_totalWeight;
			}
		}

		public decimal TotalCostOfSenderForWeight { get; set; }

		public decimal? TotalScotchCost
		{
			get { return _rows.Sum(x => x.ScotchCost); }
		}

		public decimal? TotalSenderRate
		{
			get { return _rows.Sum(x => x.TotalSenderRate); }
		}

		public decimal? TotalFactureCost
		{
			get { return _rows.Sum(x => x.FactureCost); }
		}

		public decimal? TotalWithdrawCost
		{
			get { return _rows.Sum(x => x.WithdrawCost); }
		}

		public decimal? TotalTransitCost
		{
			get { return _rows.Sum(x => x.TransitCost); }
		}

		public decimal TotalInsuranceCost
		{
			get { return _rows.Sum(x => x.InsuranceCost ?? 0); }
		}

		public decimal? TotalOfSender
		{
			get { return TotalSenderRate + TotalScotchCost + TotalFactureCost + TotalWithdrawCost; }
		}

		public decimal? TotalForwarderCost
		{
			get { return _rows.Sum(x => x.TransitCost); }
		}

		public decimal? AdditionalCost { get; set; }

		public decimal TotalExpenses
		{
			get
			{
				return (TotalOfSender ?? 0) + FlightCost + CustomCost + BrokerCost + TotalInsuranceCost
					   + (TotalForwarderCost ?? 0) + (AdditionalCost ?? 0);
			}
		}

		public decimal Profit
		{
			get { return _rows.Sum(x => x.Profit) - TotalExpenses; }
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

		public decimal FlightCost { get; set; }

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

		public decimal CustomCost { get; set; }

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

		public decimal BrokerCost { get; set; }

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
			_totalWeight = _rows.Sum(x => x.Weigth ?? 0);
		}
	}
}