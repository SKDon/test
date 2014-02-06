using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class RecipientsFacade : IRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly IClientRepository _clients;
		private readonly ICarrierRepository _carriers;
		private readonly IForwarderRepository _forwarders;
		private readonly ISenderRepository _senders;
		private readonly ISerializer _serializer;
		private readonly IStateSettingsRepository _stateSettings;
		private readonly IEventEmailRecipient _recipients;

		public RecipientsFacade(
			IAwbRepository awbs,
			ISerializer serializer,
			IStateSettingsRepository stateSettings,
			IAdminRepository admins,
			ISenderRepository senders,
			IClientRepository clients,
			ICarrierRepository carriers,
			IForwarderRepository forwarders,
			IBrokerRepository brokers,
			IEventEmailRecipient recipients)
		{
			_awbs = awbs;
			_serializer = serializer;
			_stateSettings = stateSettings;
			_admins = admins;
			_senders = senders;
			_clients = clients;
			_carriers = carriers;
			_forwarders = forwarders;
			_brokers = brokers;
			_recipients = recipients;
		}

		public RecipientData[] GetRecipients(ApplicationExtendedData application, EventType type, byte[] data)
		{
			var roles = GetRoles(type, data);

			return roles.Length == 0
				? null
				: GetRecipients(application, roles).ToArray();
		}

		private RoleType[] GetRoles(EventType type, byte[] data)
		{
			RoleType[] roles;
			if(type == EventType.ApplicationSetState)
			{
				var stateEventData = _serializer.Deserialize<ApplicationSetStateEventData>(data);

				roles = _stateSettings.GetStateEmailRecipients()
					.Where(x => x.StateId == stateEventData.StateId)
					.Select(x => x.Role)
					.ToArray();
			}
			else
			{
				roles = _recipients.GetRecipientRoles(type);
			}

			return roles;
		}

		private IEnumerable<RecipientData> GetRecipients(ApplicationExtendedData application, IEnumerable<RoleType> roles)
		{
			foreach(var role in roles)
			{
				switch(role)
				{
					case RoleType.Admin:
						var recipients =
							_admins.GetAll().Select(user => new RecipientData(user.Email, user.Language, RoleType.Admin));
						foreach(var recipient in recipients)
						{
							yield return recipient;
						}
						break;

					case RoleType.Sender:
						var sender = _senders.Get(application.SenderId);
						yield return new RecipientData(sender.Email, sender.Language, role);
						break;

					case RoleType.Broker:
						if(application.AirWaybillId.HasValue)
						{
							var awb = _awbs.Get(application.AirWaybillId.Value).Single();
							var broker = _brokers.Get(awb.BrokerId);

							yield return new RecipientData(broker.Email, broker.Language, role);
						}
						break;

					case RoleType.Forwarder:
						var forwarder = _forwarders.Get(application.ForwarderId);
						yield return new RecipientData(forwarder.Email, forwarder.Language, role);
						break;

					case RoleType.Carrier:
						var carrier = _carriers.Get(application.CarrierId);
						yield return new RecipientData(carrier.Email, carrier.Language, role);
						break;

					case RoleType.Client:
						var client = _clients.Get(application.ClientId);
						foreach(var email in client.Emails)
						{
							yield return new RecipientData(email, client.Language, role);
						}
						break;

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}
	}
}