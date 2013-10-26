using System.Linq;

namespace Alicargo.ViewModels.Calculation.Sender
{
	public sealed class SenderCalculationGroup
	{
		public long AirWaybillId { get; set; }

		// ReSharper disable InconsistentNaming

		public string field { get { return "AirWaybillId"; } }
		public string value { get; set; }
		public bool hasSubgroups { get { return false; } }
		public SenderCalculationItem[] items { get; set; }
		public Aggregates aggregates { get; set; }

		public sealed class Aggregates
		{
			public Aggregates(SenderCalculationItem[] items)
			{
				Profit = new Holder<decimal>(items.Sum(x => x.Profit));
				TotalSenderRate = new Holder<decimal>(items.Sum(x => x.TotalSenderRate));

				Count = new Holder<int>(items.Sum(x => x.Count ?? 0));
				Weigth = new Holder<float>(items.Sum(x => x.Weigth ?? 0));
				Value = new Holder<decimal>(items.Sum(x => x.Value));
				SenderScotchCost = new Holder<decimal>(items.Sum(x => x.SenderScotchCost ?? 0));
				WithdrawCost = new Holder<decimal>(items.Sum(x => x.WithdrawCost ?? 0));
				FactureCost = new Holder<decimal>(items.Sum(x => x.FactureCost ?? 0));
			}

			public Holder<decimal> Profit { get; private set; }
			public Holder<decimal> TotalSenderRate { get; private set; }

			public Holder<int> Count { get; private set; }
			public Holder<float> Weigth { get; private set; }
			public Holder<decimal> Value { get; private set; }
			public Holder<decimal> SenderScotchCost { get; private set; }
			public Holder<decimal> WithdrawCost { get; private set; }
			public Holder<decimal> FactureCost { get; private set; }
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