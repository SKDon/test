using System;

namespace Alicargo.Contracts.Contracts
{
	public sealed class AirWaybillData
	{
		public long Id { get; set; }
		public DateTimeOffset CreationTimestamp { get; set; }
		public string Bill { get; set; }
		public string ArrivalAirport { get; set; }
		public string DepartureAirport { get; set; }
		public DateTimeOffset DateOfDeparture { get; set; }
		public DateTimeOffset DateOfArrival { get; set; }
		public long BrockerId { get; set; }
		public string GTD { get; set; }
		public string GTDFileName { get; set; }
		public string GTDAdditionalFileName { get; set; }
		public string PackingFileName { get; set; }
		public string InvoiceFileName { get; set; }
		public string AWBFileName { get; set; }
		public long StateId { get; set; }
		public DateTimeOffset StateChangeTimestamp { get; set; }
	}
}