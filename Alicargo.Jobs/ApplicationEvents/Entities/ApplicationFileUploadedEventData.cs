using Alicargo.Contracts.Contracts;

namespace Alicargo.Jobs.ApplicationEvents.Entities
{
	public sealed class ApplicationFileUploadedEventData
	{
		public long? Count { get; set; }
		public FileHolder File { get; set; }
		public string FactoryName { get; set; }
		public string MarkName { get; set; }
		public string Invoice { get; set; }
	}
}
