using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.ViewModels.EmailTemplate;

namespace Alicargo.ViewModels.Helpers
{
	internal static class EmailTemplateSettingsModelHelper
	{
		public static EmailTemplateSettingsModel GetModel(RoleType[] roles)
		{
			if (roles == null)
			{
				return null;
			}

			return new EmailTemplateSettingsModel
			{
				Admin = roles.Any(x => x == RoleType.Admin),
				Sender = roles.Any(x => x == RoleType.Sender),
				Broker = roles.Any(x => x == RoleType.Broker),
				Forwarder = roles.Any(x => x == RoleType.Forwarder),
				Client = roles.Any(x => x == RoleType.Client)
			};
		}

		public static RoleType[] GetSettings(this EmailTemplateSettingsModel settings)
		{
			var list = new List<RoleType>(5);

			if (settings.Admin)
			{
				list.Add(RoleType.Admin);
			}
			if (settings.Broker)
			{
				list.Add(RoleType.Broker);
			}
			if (settings.Client)
			{
				list.Add(RoleType.Client);
			}
			if (settings.Forwarder)
			{
				list.Add(RoleType.Forwarder);
			}
			if (settings.Sender)
			{
				list.Add(RoleType.Sender);
			}

			return list.ToArray();
		}
	}
}