using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Application.Helpers
{
	public sealed class ApplicationEventRecipientsFacade : IRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly ICarrierRepository _carriers;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;
		private readonly IManagerRepository _managers;
		private readonly IEventEmailRecipient _recipients;
		private readonly ISenderRepository _senders;

		public ApplicationEventRecipientsFacade(
			IAwbRepository awbs,
			IApplicationRepository applications,
			IAdminRepository admins,
			IManagerRepository managers,
			ISenderRepository senders,
			IClientRepository clients,
			ICarrierRepository carriers,
			IForwarderRepository forwarders,
			IBrokerRepository brokers,
			IEventEmailRecipient recipients)
		{
			_awbs = awbs;
			_applications = applications;
			_admins = admins;
			_managers = managers;
			_senders = senders;
			_clients = clients;
			_carriers = carriers;
			_forwarders = forwarders;
			_brokers = brokers;
			_recipients = recipients;
		}

		public RecipientData[] GetRecipients(EventType type, EventDataForEntity data)
		{
			var application = _applications.Get(data.EntityId);

			var roles = _recipients.GetRecipientRoles(type);

			return roles.Length == 0
				? null
				: GetRecipients(application, roles).ToArray();
		}

		private IEnumerable<RecipientData> GetRecipients(ApplicationData application, IEnumerable<RoleType> roles)
		{
			foreach(var role in roles)
			{
				switch(role)
				{
					case RoleType.Admin:
						foreach(var recipient in _admins.GetAll().Select(user => new RecipientData(user.Email, user.Language, RoleType.Admin)))
						{
							yield return recipient;
						}
						break;

					case RoleType.Manager:
						foreach(var recipient in _managers.GetAll().Select(user => new RecipientData(user.Email, user.Language, RoleType.Manager)))
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
							if(awb.BrokerId.HasValue)
							{
								var broker = _brokers.Get(awb.BrokerId.Value);

								yield return new RecipientData(broker.Email, broker.Language, role);
							}
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