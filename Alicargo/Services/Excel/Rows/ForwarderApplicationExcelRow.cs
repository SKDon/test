using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel.Rows
{
	public sealed class ForwarderApplicationExcelRow : BaseApplicationExcelRow
	{
		private readonly ApplicationListItem _application;
		private readonly string _airWaybillDisplay;

		public ForwarderApplicationExcelRow(ApplicationListItem application, string airWaybillDisplay)
		{
			_application = application;
			_airWaybillDisplay = airWaybillDisplay;
		}

		[DisplayNameLocalized(typeof(Entities), "StateName")]
		public string StateName
		{
			get { return _application.State.StateName; }
		}

		[DisplayNameLocalized(typeof(Entities), "LegalEntity")]
		public string ClientLegalEntity
		{
			get { return _application.ClientLegalEntity; }
		}

		[DisplayNameLocalized(typeof(Entities), "DisplayNumber")]
		public string DisplayNumber
		{
			get { return _application.DisplayNumber; }
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
		public float? Volume
		{
			get { return _application.Volume; }
		}

		[DisplayNameLocalized(typeof(Entities), "Carrier")]
		public string CarrierName
		{
			get { return _application.CarrierName; }
		}

		[DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public string TransitMethodOfTransitString
		{
			get { return _application.TransitMethodOfTransitString; }
		}

		[DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public string TransitDeliveryTypeString
		{
			get { return _application.TransitDeliveryTypeString; }
		}

		[DisplayNameLocalized(typeof(Entities), "City")]
		public string TransitCity
		{
			get { return _application.TransitCity; }
		}

		[DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string CarrierContact
		{
			get { return _application.CarrierContact; }
		}

		[DisplayNameLocalized(typeof(Entities), "Address")]
		public string CarrierAddress
		{
			get { return _application.CarrierAddress; }
		}

		[DisplayNameLocalized(typeof(Entities), "Phone")]
		public string CarrierPhone
		{
			get { return _application.CarrierPhone; }
		}

		[DisplayNameLocalized(typeof(Entities), "TransitReference")]
		public string TransitReference
		{
			get { return _application.TransitReference; }
		}

		[DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost
		{
			get { return _application.ForwarderTransitCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybill
		{
			get { return _application.AirWaybill; }
		}

		public override string AirWaybillDisplay
		{
			get { return _airWaybillDisplay; }
		}
	}
}