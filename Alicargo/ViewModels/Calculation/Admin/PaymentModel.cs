using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.Calculation.Admin
{
	public sealed class PaymentModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "Money")]
		[DataType(DataType.Currency)]
		public decimal Money { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Comment")]
		[DataType(DataType.MultilineText)]
		public string Comment { get; set; }
	}
}