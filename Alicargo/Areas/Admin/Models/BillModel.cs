using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Alicargo.Areas.Admin.Models
{
	public sealed class BillModel
	{
		[DisplayName(@"Реквизиты")]
		public BankDetails BankDetails { get; set; }

		public string Client { get; set; }
		public string Goods { get; set; }
		public string Count { get; set; }
		public string Price { get; set; }
		public string Total { get; set; }

		[DisplayName(@"Бухгалтер")]
		public string Accountant { get; set; }

		[DisplayName(@"Руководитель")]
		public string Head { get; set; }

		[DisplayName(@"Текст в начале")]
		[DataType(DataType.MultilineText)]
		public string HeaderText { get; set; }

		[DisplayName(@"Поставщик")]
		[DataType(DataType.MultilineText)]
		public string Shipper { get; set; }
	}
}