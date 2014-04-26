namespace Alicargo.Core.Contracts.Calculation
{
	public sealed class BillSettings
	{		
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
		public decimal VAT { get; set; }
	}
}