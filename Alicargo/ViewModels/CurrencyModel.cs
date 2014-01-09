using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels
{
	public sealed class CurrencyModel
	{
		public CurrencyModel()
		{
			CurrencyId = (int)CurrencyType.Euro;
		}

		[Required, DisplayNameLocalized(typeof(Entities), "Value")]
		public decimal Value { get; set; }

		[Required]
		public int CurrencyId { get; set; }
	}
}