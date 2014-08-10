using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.AirWaybill
{
	public sealed class AwbAdminModel
	{
		[Required]
		[DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string Bill { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "ArrivalAirport")]
		public string ArrivalAirport { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DepartureAirport")]
		public string DepartureAirport { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DateOfDeparture")]
		public string DateOfDepartureLocalString { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DateOfArrival")]
		public string DateOfArrivalLocalString { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTD { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Broker")]
		public long? BrokerId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Sender")]
		public long? SenderUserId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FlightCost")]
		public decimal? FlightCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CustomCost")]
		public decimal? CustomCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "BrokerCost")]
		public decimal? BrokerCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AdditionalCost")]
		public decimal? AdditionalCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TotalCostOfSenderForWeight")]
		public decimal? TotalCostOfSenderForWeight { get; set; }
	}
}