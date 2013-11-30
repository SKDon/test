using System;

namespace Alicargo.Jobs.ApplicationEvents.Entities
{
	public sealed class ApplicationSetStateEventData
	{
		public long StateId { get; set; }
		
		public DateTimeOffset Timestamp { get; set; }
	}
}
