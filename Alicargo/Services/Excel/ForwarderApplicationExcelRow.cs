using Alicargo.Core.Localization;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.Services.Excel
{
	public sealed class ForwarderApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

		public ForwarderApplicationExcelRow(ApplicationListItem application)
		{
			_application = application;
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

		[DisplayNameLocalized(typeof(Entities), "Weigth")]
		public float? Weigth
		{
			get { return _application.Weigth; }
		}

		[DisplayNameLocalized(typeof(Entities), "CarrierName")]
		public string TransitCarrierName
		{
			get { return _application.TransitCarrierName; }
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

		[DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string TransitRecipientName
		{
			get { return _application.TransitRecipientName; }
		}

		[DisplayNameLocalized(typeof(Entities), "Address")]
		public string TransitAddress
		{
			get { return _application.TransitAddress; }
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

		[DisplayNameLocalized(typeof(Entities), "TransitReference")]
		public string TransitReference
		{
			get { return _application.TransitReference; }
		}


		[DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost
		{
			get { return _application.TransitCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybill
		{
			get { return _application.AirWaybill; }
		}
	}
}