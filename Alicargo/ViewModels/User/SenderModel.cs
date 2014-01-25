using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;
using Resources;

namespace Alicargo.ViewModels.User
{
	public sealed class SenderModel
	{
		public AuthenticationModel Authentication { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Pages), "Name")]
		public string Name { get; set; }

		[Required]
		[MaxLength(320)]
		[DataType(DataType.EmailAddress)]
		[DisplayNameLocalized(typeof(Entities), "Email")]
		public string Email { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "TariffOfTapePerBox")]
		public decimal TariffOfTapePerBox { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Countries")]
		public long[] Countries { get; set; }
	}
}