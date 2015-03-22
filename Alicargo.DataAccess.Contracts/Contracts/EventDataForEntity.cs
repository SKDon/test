namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class EventDataForEntity
	{
		public long EntityId { get; set; }
		public byte[] Data { get; set; }
	}
}