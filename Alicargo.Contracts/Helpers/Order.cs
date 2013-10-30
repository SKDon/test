namespace Alicargo.Contracts.Helpers
{
	public sealed class Order
	{
		public static readonly Order[] Default =
		{
			new Order
			{
				Desc = true,
				OrderType = OrderType.AirWaybill
			}
		};

		public OrderType OrderType { get; set; }

		public bool Desc { get; set; }
	}
}