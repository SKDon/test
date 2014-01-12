using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Resources;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Resources;

namespace Alicargo.ViewModels.User
{
	public sealed class SenderModel
	{
		public AuthenticationModel Authentication { get; set; }

		[Required, DisplayNameLocalized(typeof(Pages), "Name")]
		public string Name { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Email"), DataType(DataType.EmailAddress), MaxLength(320)]
		public string Email { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "TariffOfTapePerBox")]
		public decimal TariffOfTapePerBox { get; set; }
	}
}