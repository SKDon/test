using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class EventData
	{
		public long Id { get; set; }		
		public EventState State { get; set; }
		public byte[] Data { get; set; }
	}
}