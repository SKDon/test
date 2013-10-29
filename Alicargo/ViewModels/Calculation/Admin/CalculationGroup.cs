using System.Linq;

namespace Alicargo.ViewModels.Calculation.Admin
{
	public sealed class CalculationGroup
	{
		public long AirWaybillId { get; set; }

		// ReSharper disable InconsistentNaming
		
		public string field { get { return "AirWaybillId"; } }
		public object value { get; set; }
		public bool hasSubgroups { get { return false; } }
		public CalculationItem[] items { get; set; }
		public Aggregates aggregates { get; set; }

		public sealed class Aggregates
		{
			public Aggregates(CalculationItem[] items)
			{
				Count = new Holder<int>(items.Sum(x => x.Count ?? 0));
				Weight = new Holder<float>(items.Sum(x => x.Weight ?? 0));
				Value = new Holder<decimal>(items.Sum(x => x.Value));
				TotalTariffCost = new Holder<decimal>(items.Sum(x => x.TotalTariffCost));
				TotalSenderRate = new Holder<decimal>(items.Sum(x => x.TotalSenderRate));
				ScotchCost = new Holder<decimal>(items.Sum(x => x.ScotchCost ?? 0));
				TransitCost = new Holder<decimal>(items.Sum(x => x.TransitCost ?? 0));
				InsuranceCost = new Holder<decimal>(items.Sum(x => x.InsuranceCost));
				Profit = new Holder<decimal>(items.Sum(x => x.Profit));
			}

			public Holder<decimal> TotalSenderRate { get; private set; }
			public Holder<int> Count { get; private set; }
			public Holder<float> Weight { get; private set; }
			public Holder<decimal> Value { get; private set; }
			public Holder<decimal> TotalTariffCost { get; private set; }
			public Holder<decimal> ScotchCost { get; private set; }
			public Holder<decimal> TransitCost { get; private set; }
			public Holder<decimal> InsuranceCost { get; private set; }
			public Holder<decimal> Profit { get; private set; }
		}

		public sealed class Holder<T>
		{
			public readonly T sum;

			public Holder(T value)
			{
				sum = value;
			}
		}

		// ReSharper restore InconsistentNaming
	}
}