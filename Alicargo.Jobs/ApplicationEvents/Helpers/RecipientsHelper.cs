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
	internal sealed class RecipientsHelper
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly ISerializer _serializer;
		private readonly IStateSettingsRepository _stateSettings;
		private readonly IEmailTemplateRepository _templates;

		private readonly IUserRepository _users;

		public RecipientsHelper(
			IApplicationRepository applications,
			IAwbRepository awbs,
			ISerializer serializer,
			IStateSettingsRepository stateSettings,
			IEmailTemplateRepository templates,
			IUserRepository users)
		{
			_applications = applications;
			_awbs = awbs;
			_serializer = serializer;
			_stateSettings = stateSettings;
			_templates = templates;
			_users = users;
		}

		public RecipientData[] GetRecipients(long applicationId, ApplicationEventType type, byte[] data)
		{
			var roles = GetRoles(type, data);

			return roles.Length == 0
				? null
				: GetRecipients(applicationId, roles);
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

		private RecipientData[] GetRecipients(long applicationId, IEnumerable<RoleType> roles)
		{
			var application = _applications.Get(applicationId);
			if (application == null)
			{
				throw new InvalidOperationException("Can't find application by id " + applicationId);
			}

			return GetRecipients(application, roles).ToArray();
		}

		private IEnumerable<RecipientData> GetRecipients(ApplicationData application, IEnumerable<RoleType> roles)
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
							yield return GetRecipientData(user);
						}
						break;

					case RoleType.Sender:
						if (application.SenderId.HasValue)
						{
							var sender = users.Single(x => x.EntityId == application.SenderId.Value);

							yield return GetRecipientData(sender);
						}
						break;

					case RoleType.Broker:
						if (application.AirWaybillId.HasValue)
						{
							var awb = _awbs.Get(application.AirWaybillId.Value).Single();
							var broker = users.Single(x => x.EntityId == awb.BrokerId);

							yield return GetRecipientData(broker);
						}
						break;

					case RoleType.Forwarder:
						// todo: get forwarder from application data
						foreach (var user in users)
						{
							yield return GetRecipientData(user);
						}
						break;

					case RoleType.Client:
						var client = users.Single(x => x.EntityId == application.ClientId);

						yield return GetRecipientData(client);
						break;

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}

		private static RecipientData GetRecipientData(UserData data)
		{
			return new RecipientData
			{
				Culture = data.TwoLetterISOLanguageName,
				Email = data.Email
			};
		}
	}
}