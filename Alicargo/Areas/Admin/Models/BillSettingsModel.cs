using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Alicargo.Areas.Admin.Models
{
	public sealed class BillSettingsModel
	{
		[Required]
		[DisplayName(@"Реквизиты")]
		public BankDetails BankDetails { get; set; }

		[Required]
		[DisplayName(@"Поставщик")]
		[DataType(DataType.MultilineText)]
		public string Shipper { get; set; }

		[Required]
		[DisplayName(@"Руководитель")]
		public string Head { get; set; }

		[Required]
		[DisplayName(@"Бухгалтер")]
		public string Accountant { get; set; }

		[Required]
		[DisplayName(@"Текст в начале")]
		[DataType(DataType.MultilineText)]
		public string HeaderText { get; set; }

		[Required]
		[Range(1, 100)]
		[DisplayName(@"НДС (%)")]
		public byte VAT { get; set; }

		[Required]
		[DisplayName(@"EUR/RUB")]
		public decimal EuroToRuble { get; set; }

		[Range(1, 48)]
		[DisplayName(@"Переодичность обновления курса (часы)")]
		public int? AutoUpdatePeriodHours { get; set; }

		[DisplayName(@"Источник данных курса")]
		public string SourceUrl { get; set; }

		[Timestamp]
		public byte[] Version { get; set; }
	}
}