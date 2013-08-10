using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class CurrencyModel
	{
		public CurrencyModel()
		{
			CurrencyId = (int)CurrencyType.Euro;
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Value")]
		public decimal Value { get; set; }

		[Required]
		public int CurrencyId { get; set; }
	}
}