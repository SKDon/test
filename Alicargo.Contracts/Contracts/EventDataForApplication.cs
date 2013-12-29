namespace Alicargo.Contracts.Contracts
{
	public sealed class EventDataForApplication
	{
		public long ApplicationId { get; set; }
		public byte[] Data { get; set; }
	}
}