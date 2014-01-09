using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Resources;
using Alicargo.Utilities.Localization;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class CityEditModel
	{
		[Required, DisplayNameLocalized(typeof(Pages), "Russian")]
		public string RussianName { get; set; }

		[Required, DisplayNameLocalized(typeof(Pages), "English")]
		public string EnglishName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }
	}
}