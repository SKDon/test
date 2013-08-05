using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Alicargo.Core.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Models;
using Microsoft.Ajax.Utilities;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class ApplicationModel : ApplicationData
	{
		public ApplicationModel() { }

		public ApplicationModel(ApplicationData data) : base(data) { }

		#region Computed

		[DisplayNameLocalized(typeof(Entities), "DaysInWork")]
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

		[DisplayNameLocalized(typeof(Entities), "Value")]
		public string ValueString
		{
			get { return Value.ToString(CultureInfo.CurrentUICulture) + ((CurrencyType)CurrencyId).ToLocalString(); }
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
		public string ReferenceBill { get; set; }
		public string ReferenceGTD { get; set; }

		public string CountryName { get; set; }

		public DateTimeOffset? ReferenceDateOfDeparture { get; set; }

		public DateTimeOffset? ReferenceDateOfArrival { get; set; }

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
	}
}