using System.ComponentModel;

namespace Alicargo.Areas.Admin.Models
{
	public sealed class BankDetails
	{
		[DisplayName(@"Банк получателя")]
		public string Bank { get; set; }

		[DisplayName(@"БИК")]
		public string BIC { get; set; }

		[DisplayName(@"Корреспондентский счёт")]
		public string CorrespondentAccount { get; set; }

		[DisplayName(@"ИНН")]
		public string TIN { get; set; }

		[DisplayName(@"КПП")]
		public string TaxRegistrationReasonCode { get; set; }

		[DisplayName(@"Расчетный счёт")]
		public string CurrentAccount { get; set; }

		[DisplayName(@"Получатель")]
		public string Payee { get; set; }
	}
}