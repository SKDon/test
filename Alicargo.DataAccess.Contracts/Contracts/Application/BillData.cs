using System;

namespace Alicargo.DataAccess.Contracts.Contracts.Application
{
	public sealed class BillData
	{
		public int Number { get; set; }
		public DateTimeOffset SaveDate { get; set; }
		public string Bank { get; set; }
		public string BIC { get; set; }
		public string CorrespondentAccount { get; set; }
		public string TIN { get; set; }
		public string TaxRegistrationReasonCode { get; set; }
		public string CurrentAccount { get; set; }
		public string Payee { get; set; }
		public string Shipper { get; set; }
		public string Head { get; set; }
		public string Accountant { get; set; }
		public string HeaderText { get; set; }
		public string Client { get; set; }
		public string Goods { get; set; }
		public short Count { get; set; }
		public decimal Price { get; set; } // Euro
		public decimal VAT { get; set; }
		public decimal EuroToRuble { get; set; } // EUR/RUB
	}
}