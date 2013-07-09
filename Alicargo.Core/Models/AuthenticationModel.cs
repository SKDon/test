using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Contracts;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.Core.Models
{
	public sealed class AuthenticationModel
	{
		public AuthenticationModel() { }

		public AuthenticationModel(IAuthenticationData authentication)
		{
			Login = authentication.Login;
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Login")]
		public string Login { get; set; }

		[DataType(DataType.Password)]
		[DisplayNameLocalized(typeof(Entities), "NewPassword")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Compare("NewPassword")]
		[DisplayNameLocalized(typeof(Entities), "ConfirmPassword")]
		public string ConfirmPassword { get; set; }
	}
}