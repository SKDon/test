using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Awb
{
	public sealed class AwbEventRecipientsFacade : IRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IBrokerRepository _brokers;
		private readonly IEventEmailRecipient _recipients;
		private readonly ISerializer _serializer;

		public AwbEventRecipientsFacade(
			IAdminRepository admins,
			IBrokerRepository brokers,
			ISerializer serializer,
			IEventEmailRecipient recipients)
		{
			_admins = admins;
			_brokers = brokers;
			_serializer = serializer;
			_recipients = recipients;
		}

		public RecipientData[] GetRecipients(EventType type, EventDataForEntity data)
		{
			var roles = _recipients.GetRecipientRoles(type);

			return roles.Length == 0
				? null
				: GetRecipients(roles, data).ToArray();
		}

		private IEnumerable<RecipientData> GetRecipients(IEnumerable<RoleType> roles, EventDataForEntity data)
		{
			foreach(var role in roles)
			{
				switch(role)
				{
					case RoleType.Admin:
						var recipients = _admins.GetAll()
							.Select(user => new RecipientData(user.Email, user.Language, RoleType.Admin));
						foreach(var recipient in recipients)
						{
							yield return recipient;
						}
						break;

					case RoleType.Broker:
						var brokerId = _serializer.Deserialize<AirWaybillData>(data.Data).BrokerId;
						var broker = _brokers.Get(brokerId);

						yield return new RecipientData(broker.Email, broker.Language, role);
						break;

					case RoleType.Sender:
					case RoleType.Client:
					case RoleType.Forwarder:
					case RoleType.Carrier:
						throw new NotSupportedException("Only broker and admin can be recipient of an event");

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}
	}
}