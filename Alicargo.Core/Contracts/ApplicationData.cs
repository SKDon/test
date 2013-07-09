using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.Core.Contracts
{
	public class ApplicationData
	{
		public ApplicationData() { }

		// todo: tests
		public ApplicationData(ApplicationData data)
		{
			AddressLoad = data.AddressLoad;
			Characteristic = data.Characteristic;
			ClientId = data.ClientId;
			Count = data.Count;
			CPFileName = data.CPFileName;
			CreationTimestamp = data.CreationTimestamp;
			CurrencyId = data.CurrencyId;
			DeliveryBillFileName = data.DeliveryBillFileName;
			FactoryContact = data.FactoryContact;
			FactoryEmail = data.FactoryEmail;
			FactoryName = data.FactoryName;
			FactoryPhone = data.FactoryPhone;
			Gross = data.Gross;
			Id = data.Id;
			Invoice = data.Invoice;
			InvoiceFileName = data.InvoiceFileName;
			PackingFileName = data.PackingFileName;
			MarkName = data.MarkName;
			MethodOfDeliveryId = data.MethodOfDeliveryId;
			ReferenceId = data.ReferenceId;
			StateChangeTimestamp = data.StateChangeTimestamp;
			StateId = data.StateId;
			SwiftFileName = data.SwiftFileName;
			TermsOfDelivery = data.TermsOfDelivery;
			Torg12FileName = data.Torg12FileName;
			TransitId = data.TransitId;
			CountryId = data.CountryId;
			Value = data.Value;
			Volume = data.Volume;
			WarehouseWorkingTime = data.WarehouseWorkingTime;
			DateInStock = data.DateInStock;
			DateOfCargoReceipt = data.DateOfCargoReceipt;
			TransitReference = data.TransitReference;
		}

		public long Id { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CreationTimestamp")]
		public DateTimeOffset CreationTimestamp { get; set; }

		[Required]
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

		[DisplayNameLocalized(typeof(Entities), "Gross")]
		public float? Gross { get; set; } // todo: rename to weigth

		[DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Volume")]
		[Required]
		public float Volume { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
		public string TermsOfDelivery { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Value")]
		public decimal Value { get; set; }

		[Required]
		public int CurrencyId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Country")]
		public long? CountryId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "StateChangeTimestamp")]
		public DateTimeOffset StateChangeTimestamp { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DateInStock")]
		public DateTimeOffset? DateInStock { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DateOfCargoReceipt")]
		public DateTimeOffset? DateOfCargoReceipt { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[MaxLength(320)]
		[DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail { get; set; }

		[DataType(DataType.MultilineText)]
		[DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get; set; }

		public string TransitReference { get; set; }

		public long StateId { get; set; }

		public int MethodOfDeliveryId { get; set; }
		public long ClientId { get; set; }
		public long TransitId { get; set; }
		public long? ReferenceId { get; set; }
	}
}