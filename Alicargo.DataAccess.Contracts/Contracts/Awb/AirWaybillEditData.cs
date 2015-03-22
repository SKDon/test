using System;

namespace Alicargo.DataAccess.Contracts.Contracts.Awb
{
	public class AirWaybillEditData
	{
		public string Bill { get; set; }
		public string ArrivalAirport { get; set; }
		public string DepartureAirport { get; set; }
		public DateTimeOffset DateOfDeparture { get; set; }
		public DateTimeOffset DateOfArrival { get; set; }
		public long? BrokerId { get; set; }
		public long? SenderUserId { get; set; }
		public string GTD { get; set; }

		public decimal? FlightCost { get; set; }
		public decimal? CustomCost { get; set; }
		public decimal? BrokerCost { get; set; }
		public decimal? AdditionalCost { get; set; }
		public decimal? TotalCostOfSenderForWeight { get; set; }		
	}
}