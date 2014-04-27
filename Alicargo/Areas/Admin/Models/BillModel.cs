using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Alicargo.Areas.Admin.Models
{
	public sealed class BillModel
	{
		[DisplayName(@"Реквизиты")]
		public BankDetails BankDetails { get; set; }

		[Required]
		[DisplayName(@"Покупатель")]
		public string Client { get; set; }

		[Required]
		[DisplayName(@"Товары")]
		public string Goods { get; set; }

		[Range(1, short.MaxValue)]
		[DisplayName(@"Количество")]
		public short Count { get; set; }

		[Required]
		[DisplayName(@"Цена")]
		public decimal? PriceRuble { get; set; }

		[Required]
		[DisplayName(@"Бухгалтер")]
		public string Accountant { get; set; }

		[Required]
		[DisplayName(@"Руководитель")]
		public string Head { get; set; }

		[Required]
		[DisplayName(@"Текст в начале")]
		[DataType(DataType.MultilineText)]
		public string HeaderText { get; set; }

		[Required]
		[DisplayName(@"Поставщик")]
		[DataType(DataType.MultilineText)]
		public string Shipper { get; set; }
	}
}