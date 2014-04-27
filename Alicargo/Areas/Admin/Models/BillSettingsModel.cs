using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Alicargo.Areas.Admin.Models
{
	public sealed class BillSettingsModel
	{
		[DisplayName(@"Реквизиты")]
		public BankDetails BankDetails { get; set; }

		[DisplayName(@"Поставщик")]
		[DataType(DataType.MultilineText)]
		public string Shipper { get; set; }

		[DisplayName(@"Руководитель")]
		public string Head { get; set; }

		[DisplayName(@"Бухгалтер")]
		public string Accountant { get; set; }

		[DisplayName(@"Текст в начале")]
		[DataType(DataType.MultilineText)]
		public string HeaderText { get; set; }

		[DisplayName(@"НДС (%)")]
		public uint VAT { get; set; }

		[DisplayName(@"EUR/RUB")]
		public decimal EuroToRuble { get; set; }

		[Timestamp]
		public byte[] Version { get; set; }
	}
}