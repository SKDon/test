using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class RecipientsFacade : IRecipientsFacade
	{
		private readonly IAwbRepository _awbs;
		private readonly ISerializer _serializer;
		private readonly IStateSettingsRepository _stateSettings;
		private readonly IEmailTemplateRepository _templates;

		public RecipientsFacade(
			IAwbRepository awbs,
			ISerializer serializer,
			IStateSettingsRepository stateSettings,
			IEmailTemplateRepository templates)
		{
			_awbs = awbs;
			_serializer = serializer;
			_stateSettings = stateSettings;
			_templates = templates;
		}

		public RecipientData[] GetRecipients(ApplicationDetailsData application, ApplicationEventType type, byte[] data)
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

		private IEnumerable<RecipientData> GetRecipients(ApplicationDetailsData application, IEnumerable<RoleType> roles)
		{
			foreach (var role in roles)
			{
				switch (role)
				{
					case RoleType.Admin:

						foreach (var user in users)
						{
							yield return new RecipientData(((UserData) user).Email, ((UserData) user).TwoLetterISOLanguageName, role);
						}
						break;

					case RoleType.Sender:
						var sender = users.Single(x => x.EntityId == application.SenderId);
						yield return new RecipientData(((UserData) sender).Email, ((UserData) sender).TwoLetterISOLanguageName, role);
						break;

					case RoleType.Broker:
						if (application.AirWaybillId.HasValue)
						{
							var awb = _awbs.Get(application.AirWaybillId.Value).Single();
							var broker = users.Single(x => x.EntityId == awb.BrokerId);

							yield return new RecipientData(((UserData) broker).Email, ((UserData) broker).TwoLetterISOLanguageName, role);
						}
						break;

					case RoleType.Forwarder:
						// todo: 1. get forwarder from application data
						foreach (var user in users)
						{
							yield return new RecipientData(((UserData) user).Email, ((UserData) user).TwoLetterISOLanguageName, role);
						}
						break;

					case RoleType.Client:
						var client = users.Single(x => x.EntityId == application.ClientId);
						yield return new RecipientData(((UserData) client).Email, ((UserData) client).TwoLetterISOLanguageName, role);
						break;

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}
	}
}