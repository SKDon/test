using System.Linq;
using Alicargo.Core.Helpers;

namespace Alicargo.ViewModels.Calculation.Sender
{
	public sealed class SenderCalculationGroup
	{
		public long? AirWaybillId { get; set; }
		public decimal TotalCostOfSenderForWeight { get; set; }
		public decimal FlightCost { get; set; }

		// ReSharper disable InconsistentNaming
		public string field { get { return "AirWaybillId"; } }
		public object value { get; set; }
		public bool hasSubgroups { get { return false; } }
		public SenderCalculationItem[] items { get; set; }
		public Aggregates aggregates { get; set; }
		// ReSharper restore InconsistentNaming		

		public sealed class Aggregates
		{
			public Aggregates(SenderCalculationItem[] items)
			{
				Profit = new SumHolder<decimal>(items.Sum(x => x.Profit));
				TotalSenderRate = new SumHolder<decimal>(items.Sum(x => x.TotalSenderRate));

				Count = new SumHolder<int>(items.Sum(x => x.Count ?? 0));
				Weight = new SumHolder<float>(items.Sum(x => x.Weight ?? 0));
				Value = new SumHolder<decimal>(items.Sum(x => x.Value));
				SenderScotchCost = new SumHolder<decimal>(items.Sum(x => x.SenderScotchCost ?? 0));
				PickupCost = new SumHolder<decimal>(items.Sum(x => x.PickupCost ?? 0));
				FactureCost = new SumHolder<decimal>(items.Sum(x => x.FactureCost ?? 0));
				FactureCostEx = new SumHolder<decimal>(items.Sum(x => x.FactureCostEx ?? 0));
			}

			public SumHolder<decimal> Profit { get; private set; }
			public SumHolder<decimal> TotalSenderRate { get; private set; }
			public SumHolder<int> Count { get; private set; }
			public SumHolder<float> Weight { get; private set; }
			public SumHolder<decimal> Value { get; private set; }
			public SumHolder<decimal> SenderScotchCost { get; private set; }
			public SumHolder<decimal> PickupCost { get; private set; }
			public SumHolder<decimal> FactureCost { get; private set; }
			public SumHolder<decimal> FactureCostEx { get; private set; }
		}
	}
}