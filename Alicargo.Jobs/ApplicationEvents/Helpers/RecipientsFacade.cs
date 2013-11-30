using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal sealed class RecipientsFacade : IRecipientsFacade
	{
		private readonly IAwbRepository _awbs;
		private readonly ISerializer _serializer;
		private readonly IStateSettingsRepository _stateSettings;
		private readonly IEmailTemplateRepository _templates;

		private readonly IUserRepository _users;

		public RecipientsFacade(
			IAwbRepository awbs,
			ISerializer serializer,
			IStateSettingsRepository stateSettings,
			IEmailTemplateRepository templates,
			IUserRepository users)
		{
			_awbs = awbs;
			_serializer = serializer;
			_stateSettings = stateSettings;
			_templates = templates;
			_users = users;
		}

		public Recipient[] GetRecipients(ApplicationDetailsData application, ApplicationEventType type, byte[] data)
		{
			var roles = GetRoles(type, data);

			return roles.Length == 0
				? null
				: GetRecipients(application, roles).ToArray();
		}

		private RoleType[] GetRoles(ApplicationEventType type, byte[] data)
		{
			RoleType[] roles;
			if (type == ApplicationEventType.SetState)
			{
				var stateEventData = _serializer.Deserialize<ApplicationSetStateEventData>(data);

				roles = _stateSettings.GetStateEmailRecipients()
					.Where(x => x.StateId == stateEventData.StateId)
					.Select(x => x.Role)
					.ToArray();
			}
			else
			{
				roles = _templates.GetRecipientRoles(type);
			}

			return roles;
		}

		private IEnumerable<Recipient> GetRecipients(ApplicationDetailsData application, IEnumerable<RoleType> roles)
		{
			foreach (var role in roles)
			{
				var users = _users.GetByRole(role);
				if (users == null || users.Length == 0)
				{
					continue;
				}

				switch (role)
				{
					case RoleType.Admin:
						foreach (var user in users)
						{
							yield return GetRecipientData(user, role);
						}
						break;

					case RoleType.Sender:
						var sender = users.Single(x => x.EntityId == application.SenderId);
						yield return GetRecipientData(sender, role);
						break;

					case RoleType.Broker:
						if (application.AirWaybillId.HasValue)
						{
							var awb = _awbs.Get(application.AirWaybillId.Value).Single();
							var broker = users.Single(x => x.EntityId == awb.BrokerId);

							yield return GetRecipientData(broker, role);
						}
						break;

					case RoleType.Forwarder:
						// todo: get forwarder from application data
						foreach (var user in users)
						{
							yield return GetRecipientData(user, role);
						}
						break;

					case RoleType.Client:
						var client = users.Single(x => x.EntityId == application.ClientId);
						yield return GetRecipientData(client, role);
						break;

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}

		private static Recipient GetRecipientData(UserData data, RoleType role)
		{
			return new Recipient
			{
				Culture = data.TwoLetterISOLanguageName,
				Email = data.Email,
				Role = role
			};
		}
	}
}