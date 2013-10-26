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

		public sealed class Aggregates
		{
			public Aggregates(decimal sumProfit)
			{
				Profit = new Holder<decimal>(sumProfit);
			}

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