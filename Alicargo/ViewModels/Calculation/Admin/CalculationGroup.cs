using System.Linq;
using Alicargo.Core.Helpers;

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
		// ReSharper restore InconsistentNaming

		public sealed class Aggregates
		{
			public Aggregates(CalculationItem[] items)
			{
				Count = new SumHolder<int>(items.Sum(x => x.Count ?? 0));
				Weight = new SumHolder<float>(items.Sum(x => x.Weight ?? 0));
				Value = new SumHolder<decimal>(items.Sum(x => x.Value));
				TotalTariffCost = new SumHolder<decimal>(items.Sum(x => x.TotalTariffCost));
				TotalSenderRate = new SumHolder<decimal>(items.Sum(x => x.TotalSenderRate));
				ScotchCost = new SumHolder<decimal>(items.Sum(x => x.ScotchCost ?? 0));
				TransitCost = new SumHolder<decimal>(items.Sum(x => x.TransitCost ?? 0));
				InsuranceCost = new SumHolder<decimal>(items.Sum(x => x.InsuranceCost));
				Profit = new SumHolder<decimal>(items.Sum(x => x.Profit));
			}

			public SumHolder<decimal> TotalSenderRate { get; private set; }
			public SumHolder<int> Count { get; private set; }
			public SumHolder<float> Weight { get; private set; }
			public SumHolder<decimal> Value { get; private set; }
			public SumHolder<decimal> TotalTariffCost { get; private set; }
			public SumHolder<decimal> ScotchCost { get; private set; }
			public SumHolder<decimal> TransitCost { get; private set; }
			public SumHolder<decimal> InsuranceCost { get; private set; }
			public SumHolder<decimal> Profit { get; private set; }
		}
	}
}