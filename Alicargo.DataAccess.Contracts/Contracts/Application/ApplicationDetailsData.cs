using System;
using System.Collections.Generic;

namespace Alicargo.DataAccess.Contracts.Contracts.Application
{
	public sealed class ApplicationDetailsData
	{
		public long ClientUserId { get; set; }

		public string ClientEmail { get; set; }

		public string ClientLegalEntity { get; set; }

		public string ClientNic { get; set; }

		public string AirWaybill { get; set; }

		public string AirWaybillGTD { get; set; }

		public DateTimeOffset? AirWaybillDateOfDeparture { get; set; }

		public DateTimeOffset? AirWaybillDateOfArrival { get; set; }

		public string TransitAddress { get; set; }

		public string TransitCarrierName { get; set; }

		public string TransitCity { get; set; }

		public int TransitDeliveryTypeId { get; set; }

		public int TransitMethodOfTransitId { get; set; }

		public string TransitPhone { get; set; }

		public string TransitRecipientName { get; set; }

		public string TransitWarehouseWorkingTime { get; set; }

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

		public int CurrencyId { get; set; }

		public decimal Value { get; set; }

		public long? AirWaybillId { get; set; }

		public KeyValuePair<string, string>[] CountryName { get; set; }

		public long ClientId { get; set; }

		public long SenderId { get; set; }
	}
}