using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Contracts;
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

		[Required, DisplayNameLocalized(typeof(Entities), "Brocker")]
		public long BrockerId { get; set; }

		#endregion

		#region Files

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

		public byte[] GTDFile { get; set; }

		public byte[] GTDAdditionalFile { get; set; }

		public byte[] PackingFile { get; set; }

		public byte[] InvoiceFile { get; set; }

		public byte[] AWBFile { get; set; }

		#endregion

		#region Mapping

		public static AirWaybillEditModel GetModel(AirWaybillData data)
		{
			return new AirWaybillEditModel
			{
				PackingFileName = data.PackingFileName,
				InvoiceFileName = data.InvoiceFileName,
				PackingFile = null,
				AWBFileName = data.AWBFileName,
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				DateOfArrivalLocalString = data.DateOfArrival.LocalDateTime.ToShortDateString(),
				DateOfDepartureLocalString = data.DateOfDeparture.LocalDateTime.ToShortDateString(),
				DepartureAirport = data.DepartureAirport,
				GTD = data.GTD,
				GTDFileName = data.GTDFileName,
				InvoiceFile = null,
				AWBFile = null,
				BrockerId = data.BrockerId,
				GTDAdditionalFile = null,
				GTDFile = null
			};
		}

		#endregion
	}
}