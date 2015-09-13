using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel.Rows
{
	public sealed class CarrierApplicationExcelRow : BaseApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

	    public CarrierApplicationExcelRow(ApplicationListItem application, string airWaybillDisplay)
		{
			_application = application;
			AirWaybillDisplay = airWaybillDisplay;
		}

		[DisplayNameLocalized(typeof(Entities), "StateName")]
		public string StateName => _application.State.StateName;

        [DisplayNameLocalized(typeof(Entities), "StateChangeTimestamp")]
        public string StateChangeTimestampLocalString => _application.StateChangeTimestampLocalString;

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

	    [DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public string TransitMethodOfTransitString => _application.TransitMethodOfTransitString;

	    [DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public string TransitDeliveryTypeString => _application.TransitDeliveryTypeString;

	    [DisplayNameLocalized(typeof(Entities), "City")]
		public string TransitCity => _application.TransitCity;

	    [DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string TransitRecipientName => _application.TransitRecipientName;

	    [DisplayNameLocalized(typeof(Entities), "Address")]
		public string TransitAddress => _application.TransitAddress;

	    [DisplayNameLocalized(typeof(Entities), "Phone")]
		public string TransitPhone => _application.TransitPhone;

	    [DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string TransitWarehouseWorkingTime => _application.TransitWarehouseWorkingTime;

	    [DisplayNameLocalized(typeof(Entities), "TransitReference")]
		public string TransitReference => _application.TransitReference;

	    [DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybill => _application.AirWaybill;

	    public override string AirWaybillDisplay { get; }
	}
}