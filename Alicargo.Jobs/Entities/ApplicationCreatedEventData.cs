using System;

namespace Alicargo.Jobs.Entities
{
	public sealed class ApplicationCreatedEventData
	{
		public long ClientId { get; set; }
		public long? Count { get; set; }
		public string FactoryName { get; set; }
		public string MarkName { get; set; }
		public DateTimeOffset CreationTimestamp { get; set; }
	}
}
