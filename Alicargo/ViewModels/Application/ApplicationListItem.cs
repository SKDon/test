using System;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Services;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationListItem
	{
		#region Computed

		public string DisplayNumber
		{
			get
			{
				return ApplicationHelper.GetDisplayNumber(Id, Count);
			}
		}

		public int DaysInWork
		{
			get
			{
				unchecked // todo: 3. fix and test
				{
					return (DateTimeOffset.UtcNow - CreationTimestamp.ToUniversalTime()).Days;
				}
			}
		}

		public string CreationTimestampLocalString
		{
			get
			{
				// todo: 3. test time zones
				return CreationTimestamp.ToLocalShortDateString();
			}
		}

		public string StateChangeTimestampLocalString
		{
			get
			{
				// todo: 3. test time zones
				return StateChangeTimestamp.ToLocalShortDateString();
			}
		}

		public string DateOfCargoReceiptLocalString
		{
			get
			{
                // todo: 3. test time zones
				return DateOfCargoReceipt.HasValue ? DateOfCargoReceipt.Value.ToLocalShortDateString() : null;
			}
		}

		public string DateInStockLocalString
		{
			get
			{
				// todo: 3. test time zones
				return DateInStock.HasValue ? DateInStock.Value.ToLocalShortDateString() : null;
			}
		}

		public string MethodOfDeliveryLocalString
		{
			get { return ((MethodOfDelivery)MethodOfDeliveryId).ToLocalString(); }
		}

		public string ValueString
		{
			get { return ApplicationHelper.GetValueString(Value, CurrencyId); }
		}

		#endregion

		#region Additional

		public string ClientLegalEntity { get; set; }

		public string ClientNic { get; set; }

		public string AirWaybill { get; set; }

		public string TransitCity { get; set; }

		public string TransitAddress { get; set; }

		public string TransitRecipientName { get; set; }

		public string TransitPhone { get; set; }

		public string TransitWarehouseWorkingTime { get; set; }

		public string TransitMethodOfTransitString { get; set; }

		public string TransitDeliveryTypeString { get; set; }

		public string TransitCarrierName { get; set; }

		#endregion

		#region Application Data

		public long Id { get; set; }

		public DateTimeOffset CreationTimestamp { get; set; }

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

		public DateTimeOffset StateChangeTimestamp { get; set; }

		public DateTimeOffset? DateInStock { get; set; }

		public DateTimeOffset? DateOfCargoReceipt { get; set; }

		public string FactoryName { get; set; }

		public string FactoryPhone { get; set; }

		public string FactoryEmail { get; set; }

		public string FactoryContact { get; set; }

		public string MarkName { get; set; }

		public string TransitReference { get; set; }

		public long StateId { get; set; }

		public long? AirWaybillId { get; set; }

		public int MethodOfDeliveryId { get; set; }

		public int CurrencyId { get; set; }

		public decimal Value { get; set; }

		public long TransitId { get; set; }

		public decimal? FactureCost { get; set; }

		public decimal? ScotchCost { get; set; }

		public decimal? TariffPerKg { get; set; }

		public decimal? TransitCost { get; set; }

		public decimal? WithdrawCost { get; set; }

		public decimal? ForwarderCost { get; set; }

		#endregion

		public ApplicationStateModel State { get; set; }

		public string CountryName { get; set; }

		public bool CanClose { get; set; }

		public bool CanSetState { get; set; }
	}
}