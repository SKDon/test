using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.Core.Models
{
	public class AirWaybillData : IAirWaybillData
	{
		public AirWaybillData() { }

		// todo: test
		public AirWaybillData(IAirWaybillData data)
		{
			ArrivalAirport = data.ArrivalAirport;
			Bill = data.Bill;
			BrockerId = data.BrockerId;
			CreationTimestamp = data.CreationTimestamp;
			DateOfArrival = data.DateOfArrival;
			DateOfDeparture = data.DateOfDeparture;
			DepartureAirport = data.DepartureAirport;
			GTD = data.GTD;
			GTDFileName = data.GTDFileName;
			Id = data.Id;
			PackingFileName = data.PackingFileName;
			GTDAdditionalFileName = data.GTDAdditionalFileName;
			InvoiceFileName = data.InvoiceFileName;
			AWBFileName = data.AWBFileName;
			StateId = data.StateId;
			StateChangeTimestamp = data.StateChangeTimestamp;
		}

		public long Id { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CreationTimestamp")]
		public DateTimeOffset CreationTimestamp { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "AirWayBill")]
		public string Bill { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "ArrivalAirport")]
		public string ArrivalAirport { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DepartureAirport")]
		public string DepartureAirport { get; set; }

		public DateTimeOffset DateOfDeparture { get; set; }

		public DateTimeOffset DateOfArrival { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Brocker")]
		public long BrockerId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTD { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTDFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTDAdditional")]
		public string GTDAdditionalFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public string PackingFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string InvoiceFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AWB")]
		public string AWBFileName { get; set; }

		public long StateId { get; set; }

		public DateTimeOffset StateChangeTimestamp { get; set; }
	}
}