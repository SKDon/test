using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel.Rows
{
	public sealed class ForwarderApplicationExcelRow : BaseApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

	    public ForwarderApplicationExcelRow(ApplicationListItem application, string airWaybillDisplay)
		{
			_application = application;
			AirWaybillDisplay = airWaybillDisplay;
		}

		[DisplayNameLocalized(typeof(Entities), "StateName")]
		public string StateName => _application.State.StateName;

	    [DisplayNameLocalized(typeof(Entities), "LegalEntity")]
		public string ClientLegalEntity => _application.ClientLegalEntity;

	    [DisplayNameLocalized(typeof(Entities), "DisplayNumber")]
		public string DisplayNumber => _application.DisplayNumber;

	    [DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count => _application.Count;

	    [DisplayNameLocalized(typeof(Entities), "Weight")]
		public float? Weight => _application.Weight;

	    [DisplayNameLocalized(typeof(Entities), "Volume")]
		public float? Volume => _application.Volume;

	    [DisplayNameLocalized(typeof(Entities), "Carrier")]
		public string CarrierName => _application.CarrierName;

	    [DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public string TransitMethodOfTransitString => _application.TransitMethodOfTransitString;

	    [DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public string TransitDeliveryTypeString => _application.TransitDeliveryTypeString;

	    [DisplayNameLocalized(typeof(Entities), "City")]
		public string TransitCity => _application.TransitCity;

	    [DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string CarrierContact => _application.CarrierContact;

	    [DisplayNameLocalized(typeof(Entities), "Address")]
		public string CarrierAddress => _application.CarrierAddress;

	    [DisplayNameLocalized(typeof(Entities), "Phone")]
		public string CarrierPhone => _application.CarrierPhone;

	    [DisplayNameLocalized(typeof(Entities), "TransitReference")]
		public string TransitReference => _application.TransitReference;

	    [DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost => _application.ForwarderTransitCost;

	    [DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybill => _application.AirWaybill;

	    public override string AirWaybillDisplay { get; }
	}
}