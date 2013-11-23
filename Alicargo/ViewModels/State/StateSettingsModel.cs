using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.State
{
	public sealed class StateSettingsModel
	{
		public sealed class Settings
		{
			public Settings() { }

			public Settings(RoleType[] roles)
			{
				Admin = roles.Any(x => x == RoleType.Admin);
				Sender = roles.Any(x => x == RoleType.Sender);
				Broker = roles.Any(x => x == RoleType.Broker);
				Forwarder = roles.Any(x => x == RoleType.Forwarder);
				Client = roles.Any(x => x == RoleType.Client);
			}

			[DisplayNameLocalized(typeof(Enums), "Admin")]
			public bool Admin { get; set; }

			[DisplayNameLocalized(typeof(Enums), "Sender")]
			public bool Sender { get; set; }

			[DisplayNameLocalized(typeof(Enums), "Broker")]
			public bool Broker { get; set; }

			[DisplayNameLocalized(typeof(Enums), "Forwarder")]
			public bool Forwarder { get; set; }

			[DisplayNameLocalized(typeof(Enums), "Client")]
			public bool Client { get; set; }
		}

		public Settings Availabilities { get; set; }
		public Settings Recipients { get; set; }
		public Settings Visibilities { get; set; }
	}
}