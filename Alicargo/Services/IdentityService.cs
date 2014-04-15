using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Services
{
	internal sealed class IdentityService : IIdentityService
	{
		private readonly IAdminRepository _admins;
		private readonly IManagerRepository _managers;
		private readonly IBrokerRepository _brokers;
		private readonly ICarrierRepository _carriers;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;

		private readonly Dictionary<long, Dictionary<RoleType, bool>> _isInRole =
			new Dictionary<long, Dictionary<RoleType, bool>>();

		private readonly ISenderRepository _senders;
		private readonly IUserRepository _users;
		//private long? _identity;
		private string _language;

		public IdentityService(
			IUserRepository users,
			IAdminRepository admins,
			IManagerRepository managers,
			ICarrierRepository carriers,
			ISenderRepository senders,
			IClientRepository clients,
			IForwarderRepository forwarders,
			IBrokerRepository brokers)
		{
			if(HttpContext.Current == null)
				throw new NotSupportedException("UserHolder works only when the HttpContext.Current is presented");

			_users = users;
			_admins = admins;
			_managers = managers;
			_carriers = carriers;
			_senders = senders;
			_clients = clients;
			_forwarders = forwarders;
			_brokers = brokers;
		}

		public bool IsInRole(RoleType role)
		{
			if(!Id.HasValue) return false;

			if(!_isInRole.ContainsKey(Id.Value))
				_isInRole.Add(Id.Value, new Dictionary<RoleType, bool>());

			if(!_isInRole[Id.Value].ContainsKey(role))
			{
				var inRole = InRole(role, Id.Value);

				_isInRole[Id.Value].Add(role, inRole);
			}

			return _isInRole[Id.Value][role];
		}

		public string Language
		{
			get
			{
				if(_language != null) return _language;

				if(Id.HasValue)
				{
					var userId = Id.Value;
					var language = _users.GetLanguage(userId);
					if(language == null)
					{
						FormsAuthentication.SignOut();
						throw new EntityNotFoundException("User " + userId + " not found");
					}
					_language = language;
				}
				else
				{
					_language = TwoLetterISOLanguageName.Russian;
				}

				return _language;
			}
		}

		public void SetLanguage(string value)
		{
			if(value != TwoLetterISOLanguageName.Russian
			   && value != TwoLetterISOLanguageName.Italian
			   && value != TwoLetterISOLanguageName.English)
			{
				throw new ArgumentOutOfRangeException("value");
			}

			if(Id.HasValue)
			{
				_users.SetLanguage(Id.Value, value);
			}

			_language = value;
		}

		public bool IsAuthenticated
		{
			get { return Id.HasValue; }
		}

		public long? Id
		{
			get
			{
				//if(_identity.HasValue)
				//	return _identity.Value;

				long id;
				if(long.TryParse(HttpContext.Current.User.Identity.Name, NumberStyles.Number, null, out id))
					//_identity = id;
					return id;

				return null;

				//return _identity;
			}
			//set { _identity = value; }
		}

		private bool InRole(RoleType role, long userId)
		{
			switch(role)
			{
				case RoleType.Manager:
					return _managers.GetAll().Any(x => x.UserId == userId);

				case RoleType.Admin:
					return _admins.GetAll().Any(x => x.UserId == userId);

				case RoleType.Sender:
					return _senders.GetByUserId(userId) != null;

				case RoleType.Broker:
					return _brokers.GetByUserId(userId) != null;

				case RoleType.Forwarder:
					return _forwarders.GetByUserId(userId) != null;

				case RoleType.Carrier:
					return _carriers.GetByUserId(userId) != null;

				case RoleType.Client:
					return _clients.GetByUserId(userId) != null;

				default:
					throw new ArgumentOutOfRangeException("role");
			}
		}
	}
}