using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels
{
	public sealed class AuthenticationModel
	{
		public AuthenticationModel() {}

		public AuthenticationModel(string login)
		{
			Login = login;
		}

		[Required, DisplayNameLocalized(typeof (Entities), "Login")]
		public string Login { get; set; }

		[DataType(DataType.Password), DisplayNameLocalized(typeof (Entities), "NewPassword")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password), Compare("NewPassword"), DisplayNameLocalized(typeof (Entities), "ConfirmPassword")]
		public string ConfirmPassword { get; set; }
	}
}