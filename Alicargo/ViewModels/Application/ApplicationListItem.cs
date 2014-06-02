using System;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationListItem
	{
		public string DisplayNumber { get; set; }

		public int DaysInWork { get; set; }

		public string CreationTimestampLocalString { get; set; }

		public string StateChangeTimestampLocalString { get; set; }

		public string DateOfCargoReceiptLocalString { get; set; }

		public string DateInStockLocalString { get; set; }

		public string MethodOfDeliveryLocalString { get; set; }

		public string ValueString { get; set; }

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

		public string CarrierName { get; set; }

		public string CarrierPhone { get; set; }

		public string CarrierAddress { get; set; }

		public string CarrierContact { get; set; }

		public long Id { get; set; }

		public DateTimeOffset CreationTimestamp { get; set; }

		public string Invoice { get; set; }

		public FileInfo[] InvoiceFiles { get; set; }

		public FileInfo[] SwiftFiles { get; set; }

		public FileInfo[] PackingFiles { get; set; }

		public FileInfo[] DeliveryBillFiles { get; set; }

		public FileInfo[] Torg12Files { get; set; }

		public FileInfo[] CPFiles { get; set; }

		public FileInfo[] OtherFiles { get; set; }

		public string Characteristic { get; set; }

		public string AddressLoad { get; set; }

		public string WarehouseWorkingTime { get; set; }

		public float? Weight { get; set; }

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

		public decimal? FactureCostEx { get; set; }

		public decimal? ScotchCost { get; set; }

		public string SenderName { get; set; }

		public decimal? SenderScotchCost { get; set; }

		public decimal? TariffPerKg { get; set; }

		public decimal? TransitCost { get; set; }

		public decimal? ForwarderTransitCost { get; set; }

		public decimal? PickupCost { get; set; }

		public ApplicationStateModel State { get; set; }

		public string CountryName { get; set; }

		public bool CanClose { get; set; }

		public bool CanSetState { get; set; }

		public bool CanSetTransitCost { get; set; }

		public string ForwarderName { get; set; }
	}
}