using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Alicargo.Core.Models;
using Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationEditModel
	{
		public ApplicationEditModel()
		{
			Currency = new CurrencyModel();
		}

		// todo: refactor
		public Transit Transit { get; set; }

		#region Files

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public byte[] InvoiceFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public byte[] PackingFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Swift")]
		public byte[] SwiftFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DeliveryBill")]
		public byte[] DeliveryBillFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Torg12")]
		public byte[] Torg12File { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CP")]
		public byte[] CPFile { get; set; }

		#endregion

		#region Data

		[Required, DisplayNameLocalized(typeof(Entities), "Invoice")]
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

		[Required, DisplayNameLocalized(typeof(Entities), "Value")]
		public CurrencyModel Currency { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Country")]
		public long? CountryId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "StateChangeTimestamp")]
		public DateTimeOffset StateChangeTimestamp { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DateInStock")]
		public DateTimeOffset? DateInStock { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DateOfCargoReceipt")]
		public DateTimeOffset? DateOfCargoReceipt { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone { get; set; }

		[DataType(DataType.EmailAddress), MaxLength(320), DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail { get; set; }

		[DataType(DataType.MultilineText), DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get; set; }

		public string TransitReference { get; set; }
		public long StateId { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public int MethodOfDeliveryId { get; set; }

		public long TransitId { get; set; }

		#endregion
	}
}