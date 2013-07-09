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
	// todo: extract ApplicationListItem for grids
	public sealed class ApplicationModel : ApplicationData
	{
		public ApplicationModel() { }

		public ApplicationModel(ApplicationData data) : base(data) { }

		#region Computed

		[DisplayNameLocalized(typeof(Entities), "DisplayNumber")]
		public string DisplayNumber
		{
			get
			{
				return GetDisplayNumber(Id, Count);
			}
		}

		public static string GetDisplayNumber(long id, long? count)
		{
			id = id % 1000;

			return string.Format("{0:000}{1}", id, count.HasValue && count > 0 ? "/" + count.Value : "");
		}

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

		[DisplayNameLocalized(typeof(Entities), "CreationTimestamp")]
		public string CreationTimestampLocalString
		{
			get
			{
				// todo: test time zones
				return CreationTimestamp.LocalDateTime.ToShortDateString();
			}
		}

		[DisplayNameLocalized(typeof(Entities), "StateChangeTimestamp")]
		public string StateChangeTimestampLocalString
		{
			get
			{
				// todo: test time zones
				return StateChangeTimestamp.LocalDateTime.ToShortDateString();
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

		public string DateInStockLocalString
		{
			get
			{
				// todo: test time zones
				return DateInStock.HasValue ? DateInStock.Value.LocalDateTime.ToShortDateString() : null;
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

		public bool CanSetState
		{
			get { return _canSetState; }
			set { _canSetState = value; }
		}
		private bool _canSetState = true;

		// todo: 3. rename to Air Way Bill
		public string ReferenceBill { get; set; }
		public string ReferenceGTD { get; set; }

		public string AirWayBillDisplay { get; set; }

		public string CountryName { get; set; }

		public string AirWayBillSorter
		{
			get
			{
				var dateOfArrivalUtcTicks = ReferenceDateOfArrival.HasValue ? ReferenceDateOfArrival.Value.UtcTicks : 0;
				var dateOfDepartureUtcTicks = ReferenceDateOfDeparture.HasValue ? ReferenceDateOfDeparture.Value.UtcTicks : 0;

				return GetSorter(ReferenceBill, dateOfArrivalUtcTicks, dateOfDepartureUtcTicks);
			}
		}

		// todo: test
		public static string GetSorter(string referenceBill, long dateOfArrivalUtcTicks, long dateOfDepartureUtcTicks)
		{
			var noBill = referenceBill.IsNullOrWhiteSpace();

			if (noBill && dateOfArrivalUtcTicks == 0 && dateOfDepartureUtcTicks == 0)
				return "";

			return string.Format("{0}_{1}_{2}_{3}", noBill ? "0" : "1", dateOfArrivalUtcTicks / 10000000000,
				dateOfDepartureUtcTicks / 10000000000, referenceBill);
		}

		public DateTimeOffset? ReferenceDateOfDeparture { get; set; }

		public DateTimeOffset? ReferenceDateOfArrival { get; set; }

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

		public string StateName { get; set; }

		public bool CanClose { get; set; }

		#endregion

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