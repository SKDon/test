using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Models;
using Microsoft.Ajax.Utilities;
using Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationListItem
	{
		public ApplicationListItem() { }

		public ApplicationListItem(ApplicationData data)
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
			Weigth = data.Weigth;
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

		#region Computed

		public string DisplayNumber
		{
			get
			{
				return ApplicationModelHelper.GetDisplayNumber(Id, Count);
			}
		}

		public int DaysInWork
		{
			get
			{
				unchecked // todo: fix and test
				{
					return (DateTimeOffset.UtcNow - CreationTimestamp.ToUniversalTime()).Days;
				}
			}
		}

		public string CreationTimestampLocalString
		{
			get
			{
				// todo: test time zones
				return CreationTimestamp.LocalDateTime.ToShortDateString();
			}
		}

		public string StateChangeTimestampLocalString
		{
			get
			{
				// todo: test time zones
				return StateChangeTimestamp.LocalDateTime.ToShortDateString();
			}
		}

		public string DateOfCargoReceiptLocalString
		{
			get
			{
				// todo: test time zones
				return DateOfCargoReceipt.HasValue ? DateOfCargoReceipt.Value.LocalDateTime.ToShortDateString() : null;
			}
		}

		public string DateInStockLocalString
		{
			get
			{
				// todo: test time zones
				return DateInStock.HasValue ? DateInStock.Value.LocalDateTime.ToShortDateString() : null;
			}
		}

		public string MethodOfDeliveryLocalString
		{
			get { return ((MethodOfDelivery)MethodOfDeliveryId).ToLocalString(); }
		}

		public string ValueString
		{
			get { return Value.ToString(".00", CultureInfo.CurrentUICulture) + ((CurrencyType)CurrencyId).ToLocalString(); }
		}

		#endregion

		#region ClientData

		public string LegalEntity { get; set; }

		public string ClientNic { get; set; }

		#endregion

		public string CountryName { get; set; }

		// todo: 3. rename to Air Way Bill
		public string ReferenceBill { get; set; }

		#region State

		public ApplicationStateModel State
		{
			get
			{
				return new ApplicationStateModel
				{
					StateId = StateId,
					StateName = StateName
				};
			}
		}

		public bool CanSetState
		{
			get { return _canSetState; }
			set { _canSetState = value; }
		}
		private bool _canSetState = true;

		public string StateName { get; set; }

		public bool CanClose { get; set; }

		#endregion

		public Transit Transit { get; set; }

		#region Data

		public long Id { get; set; }

		DateTimeOffset CreationTimestamp { get; set; }

		public string Invoice { get; set; }

		public string InvoiceFileName { get; set; }

		public string SwiftFileName { get; set; }

		public string PackingFileName { get; set; }

		public string DeliveryBillFileName { get; set; }

		public string Torg12FileName { get; set; }

		public string CPFileName { get; set; }

		public string Characteristic { get; set; }

		public string AddressLoad { get; set; }

		public string WarehouseWorkingTime { get; set; }

		public float? Weigth { get; set; }

		public int? Count { get; set; }

		public float Volume { get; set; }

		public string TermsOfDelivery { get; set; }

		decimal Value { get; set; }

		int CurrencyId { get; set; }

		internal long? CountryId { get; set; }

		DateTimeOffset StateChangeTimestamp { get; set; }

		DateTimeOffset? DateInStock { get; set; }

		DateTimeOffset? DateOfCargoReceipt { get; set; }

		public string FactoryName { get; set; }

		public string FactoryPhone { get; set; }

		public string FactoryEmail { get; set; }

		public string FactoryContact { get; set; }

		public string MarkName { get; set; }

		public string TransitReference { get; set; }
		public long StateId { get; set; }
		int MethodOfDeliveryId { get; set; }
		internal long ClientId { get; set; }
		public long TransitId { get; set; }
		public long? ReferenceId { get; set; }

		#endregion
	}
}