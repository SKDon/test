namespace Alicargo.DataAccess.Contracts.Contracts.Awb
{
	public sealed class AirWaybillAggregate
	{
		public long AirWaybillId { get; set; }
		public long StateId { get; set; }
		public int TotalCount { get; set; }
		public float TotalWeight { get; set; }
		public decimal TotalValue { get; set; }
		public float TotalVolume { get; set; }
		public float TotalDocumentWeight { get; set; }
	}
}