using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.EmailTemplate
{
	public sealed class EmailTemplateSettingsModel
	{
		[DisplayNameLocalized(typeof(Enums), "Admin")]
		[Required]
		public bool Admin { get; set; }

		[DisplayNameLocalized(typeof(Enums), "Sender")]
		[Required]
		public bool Sender { get; set; }

		[DisplayNameLocalized(typeof(Enums), "Broker")]
		[Required]
		public bool Broker { get; set; }

		[DisplayNameLocalized(typeof(Enums), "Forwarder")]
		[Required]
		public bool Forwarder { get; set; }

		[DisplayNameLocalized(typeof(Enums), "Client")]
		[Required]
		public bool Client { get; set; }
	}
}