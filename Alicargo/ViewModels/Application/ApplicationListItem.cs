using System;
using System.Globalization;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationListItem
	{
		#region Computed

		public string DisplayNumber
		{
			get
			{
				return ApplicationModelHelper.GetDisplayNumber(Data.Id, Data.Count);
			}
		}

		public int DaysInWork
		{
			get
			{
				unchecked // todo: fix and test
				{
					return (DateTimeOffset.UtcNow - Data.CreationTimestamp.ToUniversalTime()).Days;
				}
			}
		}

		public string CreationTimestampLocalString
		{
			get
			{
				// todo: test time zones
				return Data.CreationTimestamp.LocalDateTime.ToShortDateString();
			}
		}

		public string StateChangeTimestampLocalString
		{
			get
			{
				// todo: test time zones
				return Data.StateChangeTimestamp.LocalDateTime.ToShortDateString();
			}
		}

		public string DateOfCargoReceiptLocalString
		{
			get
			{
				// todo: test time zones
				return Data.DateOfCargoReceipt.HasValue ? Data.DateOfCargoReceipt.Value.LocalDateTime.ToShortDateString() : null;
			}
		}

		public string DateInStockLocalString
		{
			get
			{
				// todo: test time zones
				return Data.DateInStock.HasValue ? Data.DateInStock.Value.LocalDateTime.ToShortDateString() : null;
			}
		}

		public string MethodOfDeliveryLocalString
		{
			get { return ((MethodOfDelivery)Data.MethodOfDeliveryId).ToLocalString(); }
		}

		public string ValueString
		{
			get { return Data.Value.ToString(".00", CultureInfo.CurrentUICulture) + ((CurrencyType)Data.CurrencyId).ToLocalString(); }
		}

		#endregion

		//public string CountryName { get; set; }

		//// todo: 3. rename to Air Way Bill
		//public string ReferenceBill { get; set; }

		//#region State

		//public ApplicationStateModel State
		//{
		//	get
		//	{
		//		return new ApplicationStateModel
		//		{
		//			StateId = StateId,
		//			StateName = StateName
		//		};
		//	}
		//}

		//public bool CanSetState
		//{
		//	get { return _canSetState; }
		//	set { _canSetState = value; }
		//}
		//private bool _canSetState = true;

		//public string StateName { get; set; }

		//public bool CanClose { get; set; }

		//#endregion

		//public Transit Transit { get; set; }

		//#region Data

		//public long Id { get; set; }

		//DateTimeOffset CreationTimestamp { get; set; }

		//public string Invoice { get; set; }

		//public string InvoiceFileName { get; set; }

		//public string SwiftFileName { get; set; }

		//public string PackingFileName { get; set; }

		//public string DeliveryBillFileName { get; set; }

		//public string Torg12FileName { get; set; }

		//public string CPFileName { get; set; }

		//public string Characteristic { get; set; }

		//public string AddressLoad { get; set; }

		//public string WarehouseWorkingTime { get; set; }

		//public float? Weigth { get; set; }

		//public int? Count { get; set; }

		//public float Volume { get; set; }

		//public string TermsOfDelivery { get; set; }

		//decimal Value { get; set; }

		//int CurrencyId { get; set; }

		//internal long? CountryId { get; set; }

		//DateTimeOffset StateChangeTimestamp { get; set; }

		//DateTimeOffset? DateInStock { get; set; }

		//DateTimeOffset? DateOfCargoReceipt { get; set; }

		//public string FactoryName { get; set; }

		//public string FactoryPhone { get; set; }

		//public string FactoryEmail { get; set; }

		//public string FactoryContact { get; set; }

		//public string MarkName { get; set; }

		//public string TransitReference { get; set; }
		//public long StateId { get; set; }
		//int MethodOfDeliveryId { get; set; }
		//internal long ClientId { get; set; }
		//public long TransitId { get; set; }
		//public long? ReferenceId { get; set; }

		//#endregion

		public ApplicationListItemData Data { get; set; }

		public ApplicationStateModel State { get; set; }

		public string CountryName { get; set; }

		public bool CanClose { get; set; }
		
		public bool CanSetState { get; set; }
	}
}