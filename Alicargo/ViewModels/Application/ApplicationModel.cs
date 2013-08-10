﻿using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Models;
using Microsoft.Ajax.Utilities;
using Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationModel
	{
		public ApplicationModel()
		{
			Currency = new CurrencyModel();
		}

		// todo: tests
		public ApplicationModel(ApplicationData data)
		{
			AddressLoad = data.AddressLoad;
			Characteristic = data.Characteristic;
			ClientId = data.ClientId;
			Count = data.Count;
			CPFileName = data.CPFileName;
			CreationTimestamp = data.CreationTimestamp;
			Currency = new CurrencyModel
			{
				CurrencyId = data.CurrencyId,
				Value = data.Value
			};
			DeliveryBillFileName = data.DeliveryBillFileName;
			FactoryContact = data.FactoryContact;
			FactoryEmail = data.FactoryEmail;
			FactoryName = data.FactoryName;
			FactoryPhone = data.FactoryPhone;
			Weigth = data.Weigth;
			Id = data.Id;
			Invoice = data.Invoice;
			InvoiceFileName = data.InvoiceFileName;
			PackingFileName = data.PackingFileName;
			MarkName = data.MarkName;
			MethodOfDeliveryId = data.MethodOfDeliveryId;
			AirWaybillId = data.AirWaybillId;
			StateChangeTimestamp = data.StateChangeTimestamp;
			StateId = data.StateId;
			SwiftFileName = data.SwiftFileName;
			TermsOfDelivery = data.TermsOfDelivery;
			Torg12FileName = data.Torg12FileName;
			TransitId = data.TransitId;
			CountryId = data.CountryId;
			Volume = data.Volume;
			WarehouseWorkingTime = data.WarehouseWorkingTime;
			DateInStock = data.DateInStock;
			DateOfCargoReceipt = data.DateOfCargoReceipt;
			TransitReference = data.TransitReference;
		}

		// todo: tests
		public ApplicationData GetData()
		{
			return new ApplicationData
			{
				AddressLoad = AddressLoad,
				Characteristic = Characteristic,
				ClientId = ClientId,
				Count = Count,
				CPFileName = CPFileName,
				CreationTimestamp = CreationTimestamp,
				CurrencyId = Currency.CurrencyId,
				DeliveryBillFileName = DeliveryBillFileName,
				FactoryContact = FactoryContact,
				FactoryEmail = FactoryEmail,
				FactoryName = FactoryName,
				FactoryPhone = FactoryPhone,
				Weigth = Weigth,
				Id = Id,
				Invoice = Invoice,
				InvoiceFileName = InvoiceFileName,
				PackingFileName = PackingFileName,
				MarkName = MarkName,
				MethodOfDeliveryId = MethodOfDeliveryId,
				AirWaybillId = AirWaybillId,
				StateChangeTimestamp = StateChangeTimestamp,
				StateId = StateId,
				SwiftFileName = SwiftFileName,
				TermsOfDelivery = TermsOfDelivery,
				Torg12FileName = Torg12FileName,
				TransitId = TransitId,
				CountryId = CountryId,
				Value = Currency.Value,
				Volume = Volume,
				WarehouseWorkingTime = WarehouseWorkingTime,
				DateInStock = DateInStock,
				DateOfCargoReceipt = DateOfCargoReceipt,
				TransitReference = TransitReference,
			};
		}

		#region Computed

		[DisplayNameLocalized(typeof(Entities), "DateOfCargoReceipt")]
		public string DateOfCargoReceiptLocalString
		{
			get
			{
				// todo: test time zones
				return DateOfCargoReceipt.HasValue ? DateOfCargoReceipt.Value.LocalDateTime.ToShortDateString() : null;
			}
			set
			{
				// todo: test
				if (!value.IsNullOrWhiteSpace())
				{
					DateOfCargoReceipt = DateTimeOffset.Parse(value);
				}
			}
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public MethodOfDelivery MethodOfDelivery
		{
			get { return (MethodOfDelivery)MethodOfDeliveryId; }
			set { MethodOfDeliveryId = (int)value; }
		}

		[DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public string MethodOfDeliveryLocalString
		{
			get { return MethodOfDelivery.ToLocalString(); }
		}

		#endregion

		#region ClientData

		[DisplayNameLocalized(typeof(Entities), "LegalEntity")]
		public string LegalEntity { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Nic")]
		public string ClientNic { get; set; }

		public long ClientUserId { get; set; }

		public string ClientEmail { get; set; }

		#endregion

		// todo: 3. rename to Air Way Bill
		public string AirWaybillBill { get; set; }
		public string AirWaybillGTD { get; set; }

		public string CountryName { get; set; }

		public DateTimeOffset? AirWaybillDateOfDeparture { get; set; }

		public DateTimeOffset? AirWaybillDateOfArrival { get; set; }

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

		public long Id { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CreationTimestamp")]
		public DateTimeOffset CreationTimestamp { get; set; }

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
		public int MethodOfDeliveryId { get; set; }
		public long ClientId { get; set; }
		public long TransitId { get; set; }
		public long? AirWaybillId { get; set; }

		#endregion
	}
}