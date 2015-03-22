using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.EmailTemplate
{
	public sealed class EmailTemplateSettingsModel
	{
		[Required]
		[DisplayNameLocalized(typeof(Enums), "Admin")]
		public bool Admin { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Enums), "Manager")]
		public bool Manager { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Enums), "Sender")]
		public bool Sender { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Enums), "Broker")]
		public bool Broker { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Enums), "Forwarder")]
		public bool Forwarder { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Enums), "Client")]
		public bool Client { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Carrier")]
		public bool Carrier { get; set; }
	}
}