using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel.Rows
{
	public sealed class AdminApplicationExcelRow : BaseApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

	    public AdminApplicationExcelRow(ApplicationListItem application, string airWaybillDisplay)
		{
			_application = application;
			AirWaybillDisplay = airWaybillDisplay;
		}

		[DisplayNameLocalized(typeof(Entities), "CreationTimestamp")]
		public string CreationTimestampLocalString => _application.CreationTimestampLocalString;

	    [DisplayNameLocalized(typeof(Entities), "StateName")]
		public string StateName => _application.State.StateName;

        [DisplayNameLocalized(typeof(Entities), "StateChangeTimestamp")]
        public string StateChangeTimestampLocalString => _application.StateChangeTimestampLocalString;

        [DisplayNameLocalized(typeof(Entities), "DateOfCargoReceipt")]
		public string DateOfCargoReceiptLocalString => _application.DateOfCargoReceiptLocalString;

	    [DisplayNameLocalized(typeof(Entities), "DateInStock")]
		public string DateInStockLocalString => _application.DateInStockLocalString;

	    [DisplayNameLocalized(typeof(Entities), "Nic")]
		public string ClientNic => _application.ClientNic;

	    [DisplayNameLocalized(typeof(Entities), "DisplayNumber")]
		public string DisplayNumber => _application.DisplayNumber;

	    [DisplayNameLocalized(typeof(Entities), "Country")]
		public string CountryName => _application.CountryName;

	    [DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName => _application.FactoryName;

	    [DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName => _application.MarkName;

	    [DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count => _application.Count;

        [DisplayNameLocalized(typeof(Entities), "Weight")]
        public float? Weight => _application.Weight;

        [DisplayNameLocalized(typeof(Entities), "Volume")]
        public float Volume => _application.Volume;

        [DisplayNameLocalized(typeof(Entities), "Invoice")]
        public string Invoice => _application.Invoice;

        [DisplayNameLocalized(typeof(Entities), "MRN")]
        public string MRN => _application.MRN;

        [DisplayNameLocalized(typeof(Entities), "CountInInvoce")]
		public int? CountInInvoce => _application.CountInInvoce;

	    [DisplayNameLocalized(typeof(Entities), "DocumentWeight")]
		public float? DocumentWeight => _application.DocumentWeight;

	    [DisplayNameLocalized(typeof(Entities), "Value")]
		public string ValueString => _application.ValueString;

	    [DisplayNameLocalized(typeof(Entities), "Sender")]
		public string SenderName => _application.SenderName;

	    [DisplayNameLocalized(typeof(Entities), "Forwarder")]
		public string ForwarderName => _application.ForwarderName;

	    [DisplayNameLocalized(typeof(Entities), "City")]
		public string TransitCity => _application.TransitCity;

	    [DisplayNameLocalized(typeof(Entities), "Carrier")]
		public string CarrierName => _application.CarrierName;

	    [DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public string TransitMethodOfTransitString => _application.TransitMethodOfTransitString;

        [DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
        public string MethodOfDeliveryLocalString => _application.MethodOfDeliveryLocalString;

        [DisplayNameLocalized(typeof(Entities), "TransitReference")]
		public string TransitReference => _application.TransitReference;

	    [DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybill => _application.AirWaybill;

	    [DisplayNameLocalized(typeof(Entities), "DaysInWork")]
		public int DaysInWork => _application.DaysInWork;	    

	    [DisplayNameLocalized(typeof(Entities), "Characteristic")]
		public string Characteristic => _application.Characteristic;

	    [DisplayNameLocalized(typeof(Entities), "AddressLoad")]
		public string AddressLoad => _application.AddressLoad;

	    [DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime => _application.WarehouseWorkingTime;

	    [DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
		public string TermsOfDelivery => _application.TermsOfDelivery;

	    [DisplayNameLocalized(typeof(Entities), "LegalEntity")]
		public string ClientLegalEntity => _application.ClientLegalEntity;

	    [DisplayNameLocalized(typeof(Entities), "Address")]
		public string TransitAddress => _application.TransitAddress;

	    [DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string TransitRecipientName => _application.TransitRecipientName;

	    [DisplayNameLocalized(typeof(Entities), "Phone")]
		public string TransitPhone => _application.TransitPhone;

	    [DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string TransitWarehouseWorkingTime => _application.TransitWarehouseWorkingTime;

	    [DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public string TransitDeliveryTypeString => _application.TransitDeliveryTypeString;

	    [DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone => _application.FactoryPhone;

	    [DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail => _application.FactoryEmail;

	    [DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact => _application.FactoryContact;

	    [DisplayNameLocalized(typeof(Entities), "FactureCost")]
		public decimal? FactureCost => _application.FactureCost;

	    [DisplayNameLocalized(typeof(Entities), "FactureCostEx")]
		public decimal? FactureCostEx => _application.FactureCostEx;

	    [DisplayNameLocalized(typeof(Entities), "ScotchCost")]
		public decimal? ScotchCost => _application.ScotchCost;

	    [DisplayNameLocalized(typeof(Entities), "TariffPerKg")]
		public decimal? TariffPerKg => _application.TariffPerKg;

	    [DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost => _application.TransitCost;

	    [DisplayNameLocalized(typeof(Entities), "PickupCost")]
		public decimal? PickupCost => _application.PickupCost;

	    public override string AirWaybillDisplay { get; }
	}
}