using System;

namespace Alicargo.Core.Contracts.Event
{
	public sealed class ApplicationSetStateEventData
	{
		public long StateId { get; set; }
		
		public DateTimeOffset Timestamp { get; set; }
	}
}