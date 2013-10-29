﻿using System;

namespace Alicargo.Contracts.Contracts
{
	public sealed class ApplicationData
	{
		public long Id { get; set; }
		public DateTimeOffset CreationTimestamp { get; set; }
		public string Invoice { get; set; }		
		public string Characteristic { get; set; }
		public string AddressLoad { get; set; }
		public string WarehouseWorkingTime { get; set; }
		public float? Weight { get; set; }
		public int? Count { get; set; }
		public float Volume { get; set; }
		public string TermsOfDelivery { get; set; }
		public decimal Value { get; set; }
		public int CurrencyId { get; set; }
		public long? CountryId { get; set; }
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
		public int MethodOfDeliveryId { get; set; }
		public long ClientId { get; set; }
		public long TransitId { get; set; }
		public long? AirWaybillId { get; set; }
		public long? SenderId { get; set; }
		public int? ClassId { get; set; }

		public string InvoiceFileName { get; set; }
		public string SwiftFileName { get; set; }
		public string PackingFileName { get; set; }
		public string DeliveryBillFileName { get; set; }
		public string Torg12FileName { get; set; }
		public string CPFileName { get; set; }
		
		public decimal? FactureCost { get; set; }
		public decimal? PickupCost { get; set; }
		public decimal? FactureCostEdited { get; set; }
		public decimal? TransitCostEdited { get; set; }
		public decimal? ScotchCostEdited { get; set; }
		public decimal? PickupCostEdited { get; set; }
		public decimal? TariffPerKg { get; set; }
		public decimal? TransitCost { get; set; }
		public decimal? SenderRate { get; set; }
	}
}