namespace Alicargo.Core.Contracts
{
	public sealed class AirWaybillAggregate
	{
		public long AirWaybillId { get; set; }
		public long StateId { get; set; }
		public int TotalCount { get; set; }
		public float TotalWeight { get; set; }
	}
}