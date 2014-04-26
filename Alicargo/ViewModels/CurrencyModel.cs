using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels
{
	public sealed class CurrencyModel
	{
		public CurrencyModel()
		{
			CurrencyId = CurrencyType.Euro;
		}

		[Required, DisplayNameLocalized(typeof(Entities), "Value")]
		public decimal Value { get; set; }

		[Required]
		public CurrencyType CurrencyId { get; set; }
	}
}