using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Contracts
{
	public sealed class EventData
	{
		public long Id { get; set; }
		public long ApplicationId { get; set; }
		public EventType EventType { get; set; }
		public byte[] Data { get; set; }
	}
}