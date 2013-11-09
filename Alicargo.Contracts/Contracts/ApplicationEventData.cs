using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Contracts
{
	public sealed class ApplicationEventData
	{
		public long Id { get; set; }
		public long ApplicationId { get; set; }
		public ApplicationEventType EventType { get; set; }
		public byte[] Data { get; set; }
	}
}