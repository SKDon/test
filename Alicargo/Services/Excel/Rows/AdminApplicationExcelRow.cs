using Alicargo.Core.Localization;
using Alicargo.Core.Resources;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel.Rows
{
	public sealed class AdminApplicationExcelRow : BaseApplicationExcelRow
	{
		private readonly ApplicationListItem _application;
		private readonly string _airWaybillDisplay;

		public AdminApplicationExcelRow(ApplicationListItem application, string airWaybillDisplay)
		{
			_application = application;
			_airWaybillDisplay = airWaybillDisplay;
		}

		[DisplayNameLocalized(typeof(Entities), "CreationTimestamp")]
		public string CreationTimestampLocalString
		{
			get { return _application.CreationTimestampLocalString; }
		}

		[DisplayNameLocalized(typeof(Entities), "StateName")]
		public string StateName
		{
			get { return _application.State.StateName; }
		}

		[DisplayNameLocalized(typeof(Entities), "DateOfCargoReceipt")]
		public string DateOfCargoReceiptLocalString
		{
			get { return _application.DateOfCargoReceiptLocalString; }
		}

		[DisplayNameLocalized(typeof(Entities), "DateInStock")]
		public string DateInStockLocalString
		{
			get { return _application.DateInStockLocalString; }
		}

		[DisplayNameLocalized(typeof(Entities), "Nic")]
		public string ClientNic
		{
			get { return _application.ClientNic; }
		}

		[DisplayNameLocalized(typeof(Entities), "DisplayNumber")]
		public string DisplayNumber
		{
			get { return _application.DisplayNumber; }
		}

		[DisplayNameLocalized(typeof(Entities), "Country")]
		public string CountryName
		{
			get { return _application.CountryName; }
		}

		[DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName
		{
			get { return _application.FactoryName; }
		}

		[DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName
		{
			get { return _application.MarkName; }
		}

		[DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count
		{
			get { return _application.Count; }
		}

		[DisplayNameLocalized(typeof(Entities), "Weight")]
		public float? Weight
		{
			get { return _application.Weight; }
		}

		[DisplayNameLocalized(typeof(Entities), "Volume")]
		public float Volume
		{
			get { return _application.Volume; }
		}

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string Invoice
		{
			get { return _application.Invoice; }
		}

		[DisplayNameLocalized(typeof(Entities), "Value")]
		public string ValueString
		{
			get { return _application.ValueString; }
		}

		[DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public string TransitMethodOfTransitString
		{
			get { return _application.TransitMethodOfTransitString; }
		}

		[DisplayNameLocalized(typeof(Entities), "City")]
		public string TransitCity
		{
			get { return _application.TransitCity; }
		}

		[DisplayNameLocalized(typeof(Entities), "CarrierName")]
		public string TransitCarrierName
		{
			get { return _application.TransitCarrierName; }
		}

		[DisplayNameLocalized(typeof(Entities), "TransitReference")]
		public string TransitReference
		{
			get { return _application.TransitReference; }
		}

		[DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybill
		{
			get { return _application.AirWaybill; }
		}

		[DisplayNameLocalized(typeof(Entities), "DaysInWork")]
		public int DaysInWork
		{
			get { return _application.DaysInWork; }
		}

		[DisplayNameLocalized(typeof(Entities), "StateChangeTimestamp")]
		public string StateChangeTimestampLocalString
		{
			get { return _application.StateChangeTimestampLocalString; }
		}

		[DisplayNameLocalized(typeof(Entities), "Characteristic")]
		public string Characteristic
		{
			get { return _application.Characteristic; }
		}

		[DisplayNameLocalized(typeof(Entities), "AddressLoad")]
		public string AddressLoad
		{
			get { return _application.AddressLoad; }
		}

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime
		{
			get { return _application.WarehouseWorkingTime; }
		}

		[DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
		public string TermsOfDelivery
		{
			get { return _application.TermsOfDelivery; }
		}

		[DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public string MethodOfDeliveryLocalString
		{
			get { return _application.MethodOfDeliveryLocalString; }
		}

		[DisplayNameLocalized(typeof(Entities), "LegalEntity")]
		public string ClientLegalEntity
		{
			get { return _application.ClientLegalEntity; }
		}

		[DisplayNameLocalized(typeof(Entities), "Address")]
		public string TransitAddress
		{
			get { return _application.TransitAddress; }
		}

		[DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string TransitRecipientName
		{
			get { return _application.TransitRecipientName; }
		}

		[DisplayNameLocalized(typeof(Entities), "Phone")]
		public string TransitPhone
		{
			get { return _application.TransitPhone; }
		}

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string TransitWarehouseWorkingTime
		{
			get { return _application.TransitWarehouseWorkingTime; }
		}

		[DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public string TransitDeliveryTypeString
		{
			get { return _application.TransitDeliveryTypeString; }
		}

		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone
		{
			get { return _application.FactoryPhone; }
		}

		[DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail
		{
			get { return _application.FactoryEmail; }
		}

		[DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact
		{
			get { return _application.FactoryContact; }
		}

		[DisplayNameLocalized(typeof(Entities), "FactureCost")]
		public decimal? FactureCost
		{
			get { return _application.FactureCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "ScotchCost")]
		public decimal? ScotchCost
		{
			get { return _application.ScotchCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "TariffPerKg")]
		public decimal? TariffPerKg
		{
			get { return _application.TariffPerKg; }
		}

		[DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost
		{
			get { return _application.TransitCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "PickupCost")]
		public decimal? PickupCost
		{
			get { return _application.PickupCost; }
		}

		public override string AirWaybillDisplay
		{
			get { return _airWaybillDisplay; }
		}
	}
}