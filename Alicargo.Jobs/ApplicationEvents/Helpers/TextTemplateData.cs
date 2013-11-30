using System;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal sealed class TextTemplateData
	{
		public long Id { get; set; }

		public long? Count { get; set; }

		public string Number { get; set; }

		public string ClientNic { get; set; }

		public DateTimeOffset CreationTimestamp { get; set; }

		public string FactoryName { get; set; }

		public string MarkName { get; set; }

		public string Invoice { get; set; }

		public string LegalEntity { get; set; }

		public string InvoiceFileName { get; set; }

		public string PackingFileName { get; set; }

		public string SwiftFileName { get; set; }

		public string DeliveryBillFileName { get; set; }

		public string Torg12FileName { get; set; }

		public DateTimeOffset? DateOfCargoReceipt { get; set; }

		public string TransitReference { get; set; }
		public string TransitCarrierName { get; set; }
		public string FactoryEmail { get; set; }
		public string FactoryPhone { get; set; }
		public string FactoryContact { get; set; }
		public int DaysInWork { get; set; }
		public DateTimeOffset StateChangeTimestamp { get; set; }
		public string TransitReference { get; set; }
		public string TransitReference { get; set; }
	}
}