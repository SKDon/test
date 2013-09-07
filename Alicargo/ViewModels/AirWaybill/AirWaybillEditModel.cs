using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels.AirWaybill
{
	public sealed class AirWaybillEditModel
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

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTD { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Broker")]
		public long BrokerId { get; set; }

		// todo: 3. localization of cost fields
		[DisplayNameLocalized(typeof(Entities), "FlightCost")]
		public decimal? FlightCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CustomCost")]
		public decimal? CustomCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "BrokerCost")]
		public decimal? BrokerCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "ForwarderCost")]
		public decimal? ForwarderCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AdditionalCost")]
		public decimal? AdditionalCost { get; set; }

		#endregion

		#region Files

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public byte[] GTDFile { get; set; }
		public string GTDFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTDAdditional")]
		public byte[] GTDAdditionalFile { get; set; }
		public string GTDAdditionalFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public byte[] PackingFile { get; set; }
		public string PackingFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public byte[] InvoiceFile { get; set; }
		public string InvoiceFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AWB")]
		public byte[] AWBFile { get; set; }
		public string AWBFileName { get; set; }

		#endregion
	}
}