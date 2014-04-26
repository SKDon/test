namespace Alicargo.Areas.Admin.Models
{
	public sealed class BankDetails
	{
		/// <summary>
		/// Банк получателя
		/// </summary>
		public string Bank { get; set; }

		/// <summary>
		/// БИК
		/// </summary>
		public string BIC { get; set; }

		/// <summary>
		/// Корреспондентский счёт
		/// </summary>
		public string CorrespondentAccount { get; set; }

		/// <summary>
		/// ИНН
		/// </summary>
		public string TIN { get; set; }

		/// <summary>
		/// КПП
		/// </summary>
		public string TaxRegistrationReasonCode { get; set; }

		/// <summary>
		/// Расчетный счёт
		/// </summary>
		public string CurrentAccount { get; set; }

		/// <summary>
		/// Получатель
		/// </summary>
		public string Payee { get; set; }
	}
}