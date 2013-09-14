using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels.AirWaybill
{
	public sealed class SenderAwbModel
	{
		#region Data

		[Required, DisplayNameLocalized(typeof(Entities), "AirWayBill")]
		public string Bill { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "ArrivalAirport")]
		public string ArrivalAirport { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "DepartureAirport")]
		public string DepartureAirport { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "DateOfDeparture")]
		public string DateOfDepartureLocalString { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "DateOfArrival")]
		public string DateOfArrivalLocalString { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Broker")]
		public long BrokerId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FlightCost")]
		public decimal? FlightCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TotalCostOfSenderForWeight")]
		public decimal? TotalCostOfSenderForWeight { get; set; }

		#endregion

		#region Files

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public byte[] PackingFile { get; set; }
		public string PackingFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AWB")]
		public byte[] AWBFile { get; set; }
		public string AWBFileName { get; set; }

		#endregion
	}
}