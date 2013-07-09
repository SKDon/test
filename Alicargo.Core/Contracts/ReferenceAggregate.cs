namespace Alicargo.Core.Contracts
{
	public sealed class ReferenceAggregate
	{
		public long ReferenceId { get; set; }
		public long StateId { get; set; }
		public int TotalCount { get; set; }
		public float TotalWeight { get; set; }
	}
}