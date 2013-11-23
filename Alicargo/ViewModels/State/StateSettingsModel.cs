using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.State
{
	public sealed class StateSettingsModel
	{
		public Settings Availabilities { get; set; }
		public Settings Recipients { get; set; }
		public Settings Visibilities { get; set; }

		public sealed class Settings
		{
			public Settings()
			{
			}

			public Settings(RoleType[] roles)
			{
				Admin = roles.Any(x => x == RoleType.Admin);
				Sender = roles.Any(x => x == RoleType.Sender);
				Broker = roles.Any(x => x == RoleType.Broker);
				Forwarder = roles.Any(x => x == RoleType.Forwarder);
				Client = roles.Any(x => x == RoleType.Client);
			}

			[DisplayNameLocalized(typeof (Enums), "Admin")]
			[Required]
			public bool Admin { get; set; }

			[DisplayNameLocalized(typeof (Enums), "Sender")]
			[Required]
			public bool Sender { get; set; }

			[DisplayNameLocalized(typeof (Enums), "Broker")]
			[Required]
			public bool Broker { get; set; }

			[DisplayNameLocalized(typeof (Enums), "Forwarder")]
			[Required]
			public bool Forwarder { get; set; }

			[DisplayNameLocalized(typeof (Enums), "Client")]
			[Required]
			public bool Client { get; set; }
		}
	}
}