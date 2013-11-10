using System;

namespace Alicargo.Jobs.Entities
{
	public sealed class ApplicationSetStateEventData
	{
		public long StateId { get; set; }
		
		public DateTimeOffset Timestamp { get; set; }
	}
}
