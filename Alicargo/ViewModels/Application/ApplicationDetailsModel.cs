using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Helpers;
using Resources;

namespace Alicargo.ViewModels.Application
{
    public sealed class ApplicationDetailsModel
	{
		#region Computed

		[DisplayNameLocalized(typeof(Entities), "DateOfCargoReceipt")]
		public string DateOfCargoReceiptLocalString
		{
			get
			{
				return DateOfCargoReceipt.HasValue ? DateOfCargoReceipt.Value.LocalDateTime.ToShortDateString() : null;
			}
		}

		[DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public string MethodOfDeliveryLocalString
		{
			get { return ((MethodOfDelivery)MethodOfDeliveryId).ToLocalString(); }
		}

		[DisplayNameLocalized(typeof(Entities), "Value")]
		public string ValueString
		{
			get { return ApplicationModelHelper.GetValueString(Value, CurrencyId); }
		}

		#endregion

		#region Additional

		public long ClientUserId { get; set; }

		public string ClientNic { get; set; }

		public string ClientLegalEntity { get; set; }

		public string ClientEmail { get; set; }

		public string AirWaybill { get; set; }

		public string AirWaybillGTD { get; set; }

		public string CountryName { get; set; }

		public DateTimeOffset? AirWaybillDateOfDeparture { get; set; }

		public DateTimeOffset? AirWaybillDateOfArrival { get; set; }

		public string TransitCity { get; set; }

		public string TransitAddress { get; set; }

		public string TransitRecipientName { get; set; }

		public string TransitPhone { get; set; }

		public int TransitMethodOfTransitId { get; set; }

		public int TransitDeliveryTypeId { get; set; }

		public string TransitWarehouseWorkingTime { get; set; }

		public string TransitCarrierName { get; set; }

		#endregion

		#region Application Data

		public long Id { get; set; }

		public DateTimeOffset CreationTimestamp { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string Invoice { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string InvoiceFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Swift")]
		public string SwiftFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public string PackingFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DeliveryBill")]
		public string DeliveryBillFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Torg12")]
		public string Torg12FileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CP")]
		public string CPFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Characteristic")]
		public string Characteristic { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AddressLoad")]
		public string AddressLoad { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Weigth")]
		public float? Weigth { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Volume"), Required]
		public float Volume { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
		public string TermsOfDelivery { get; set; }

		public DateTimeOffset StateChangeTimestamp { get; set; }

		public DateTimeOffset? DateOfCargoReceipt { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone { get; set; }

		[DataType(DataType.EmailAddress), MaxLength(320), DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail { get; set; }

		[DataType(DataType.MultilineText), DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get; set; }

		public string TransitReference { get; set; }

		public long StateId { get; set; }

		public int MethodOfDeliveryId { get; set; }

		public decimal Value { get; set; }

		public int CurrencyId { get; set; }

		public long? AirWaybillId { get; set; }

		#endregion
	}
}