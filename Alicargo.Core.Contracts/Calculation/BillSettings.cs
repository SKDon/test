namespace Alicargo.Core.Contracts.Calculation
{
	public sealed class BillSettings
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

		/// <summary>
		/// Поставщик
		/// </summary>
		public string Shipper { get; set; }

		/// <summary>
		/// Руководитель
		/// </summary>
		public string Head { get; set; }

		/// <summary>
		/// Поставщик
		/// </summary>
		public string Accountant { get; set; }

		/// <summary>
		/// Предупреждение
		/// </summary>
		public string HeaderText { get; set; }
	}
}