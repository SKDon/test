namespace Alicargo.DataAccess.Contracts.Helpers
{
	public sealed class Order
	{
		public OrderType OrderType { get; set; }
		public bool Desc { get; set; }
	}
}