using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Contracts
{
	public sealed class EventData
	{
		public long Id { get; set; }		
		public EventState State { get; set; }
		public byte[] Data { get; set; }
	}
}