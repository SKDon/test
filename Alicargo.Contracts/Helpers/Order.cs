namespace Alicargo.Contracts.Helpers
{
	public sealed class Order
	{
		public OrderType OrderType { get; set; }

		public bool Desc { get; set; }

		public readonly static Order[] Default =
		{
			new Order
			{
				Desc = true,
				OrderType = OrderType.AirWaybill
			},
			new Order
			{
				Desc = false,
				OrderType = OrderType.ClientNic
			},
			new Order
			{
				Desc = true,
				OrderType = OrderType.Id
			}
		};
	}
}