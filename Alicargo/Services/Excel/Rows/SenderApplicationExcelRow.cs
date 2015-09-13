using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel.Rows
{
	public sealed class SenderApplicationExcelRow : BaseApplicationExcelRow
	{
		private readonly ApplicationListItem _application;
		private readonly string _airWaybillDisplay;

		public SenderApplicationExcelRow(ApplicationListItem application, string airWaybillDisplay)
		{
			_application = application;
			_airWaybillDisplay = airWaybillDisplay;
		}

		[DisplayNameLocalized(typeof(Entities), "StateChangeTimestamp")]
		public string StateChangeTimestampLocalString => _application.StateChangeTimestampLocalString;

	    [DisplayNameLocalized(typeof(Entities), "Nic")]
		public string ClientNic => _application.ClientNic;

	    [DisplayNameLocalized(typeof(Entities), "City")]
		public string TransitCity => _application.TransitCity;

        [DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
        public string MethodOfDeliveryLocalString => _application.MethodOfDeliveryLocalString;

        [DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
        public string TermsOfDelivery => _application.TermsOfDelivery;

        [DisplayNameLocalized(typeof(Entities), "StateName")]
		public string StateName => _application.State.StateName;

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

	    [DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybill => _application.AirWaybill;

        [DisplayNameLocalized(typeof(Entities), "DaysInWork")]
		public int DaysInWork => _application.DaysInWork;

	    [DisplayNameLocalized(typeof(Entities), "CreationTimestamp")]
		public string CreationTimestampLocalString => _application.CreationTimestampLocalString;

	    [DisplayNameLocalized(typeof(Entities), "Characteristic")]
		public string Characteristic => _application.Characteristic;

	    [DisplayNameLocalized(typeof(Entities), "AddressLoad")]
		public string AddressLoad => _application.AddressLoad;

	    [DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime => _application.WarehouseWorkingTime;

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
		public decimal? ScotchCost => _application.SenderScotchCost;

	    [DisplayNameLocalized(typeof(Entities), "PickupCost")]
		public decimal? PickupCost => _application.PickupCost;

	    public override string AirWaybillDisplay => _airWaybillDisplay;
	}
}