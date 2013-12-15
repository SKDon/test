namespace Alicargo.ViewModels.Calculation.Client
{
	public sealed class ClientCalculationGroup
	{
		public long AirWaybillId { get; set; }

		// ReSharper disable InconsistentNaming
		public string field { get { return "AirWaybillId"; } }
		public string value { get; set; }
		public bool hasSubgroups { get { return false; } }
		public ClientCalculationItem[] items { get; set; }
		public Aggregates aggregates { get; set; }
		// ReSharper restore InconsistentNaming		

		public sealed class Aggregates
		{
			public Aggregates(decimal sumProfit)
			{
				Profit = new SumHolder<decimal>(sumProfit);
			}

			public SumHolder<decimal> Profit { get; private set; }
		}
	}
}