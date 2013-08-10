using System;

namespace Alicargo.Core.Models
{
	[Obsolete]
	public interface IAirWaybillData
	{
		long Id { get; set; }
		DateTimeOffset CreationTimestamp { get; set; }
		string Bill { get; set; }
		string ArrivalAirport { get; set; }
		string DepartureAirport { get; set; }
		DateTimeOffset DateOfDeparture { get; set; }
		DateTimeOffset DateOfArrival { get; set; }
		long BrockerId { get; set; }
		string GTD { get; set; }
		string GTDFileName { get; set; }
		string GTDAdditionalFileName { get; set; }
		string PackingFileName { get; set; }
		string InvoiceFileName { get; set; }
		string AWBFileName { get; set; }

		long StateId { get; set; }
		DateTimeOffset StateChangeTimestamp { get; set; }
	}
}