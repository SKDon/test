using System.Linq;

namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationGroup
	{
		// ReSharper disable InconsistentNaming

		public long AirWaybillId { get; set; }
		public string field { get; set; }
		public string value { get; set; }
		public bool hasSubgroups { get; set; }
		public CalculationItem[] items { get; set; }
		public Aggregates aggregates { get; set; }

		public sealed class Aggregates
		{

			public Aggregates(int count, float weigth, decimal value)
			{
				Value = new Holder<decimal>(value);
				Count = new Holder<int>(count);
				Weigth = new Holder<float>(weigth);
			}

			public Aggregates(CalculationItem[] items)
			{
				Count = new Holder<int>(items.Sum(x => x.Count ?? 0));
				Weigth = new Holder<float>(items.Sum(x => x.Weigth ?? 0));
				Value = new Holder<decimal>(items.Sum(x => x.Value));
				TotalTariffCost = new Holder<decimal>(items.Sum(x => x.TotalTariffCost ?? 0));
				TotalSenderRate = new Holder<decimal>(items.Sum(x => x.TotalSenderRate ?? 0));
				ScotchCost = new Holder<decimal>(items.Sum(x => x.ScotchCost ?? 0));
				TransitCost = new Holder<decimal>(items.Sum(x => x.TransitCost ?? 0));
				ForwarderCost = new Holder<decimal>(items.Sum(x => x.ForwarderCost ?? 0));
				InsuranceCost = new Holder<decimal>(items.Sum(x => x.InsuranceCost ?? 0));
				Profit = new Holder<decimal>(items.Sum(x => x.Profit));
			}

			public Holder<decimal> ForwarderCost { get; private set; }
			public Holder<decimal> TotalSenderRate { get; private set; }
			public Holder<int> Count { get; private set; }
			public Holder<float> Weigth { get; private set; }
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