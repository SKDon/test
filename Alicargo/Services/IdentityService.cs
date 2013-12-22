using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Services.Abstract;

namespace Alicargo.Services
{
	internal sealed class IdentityService : IIdentityService
	{
		private readonly IAdminRepository _admins;
		private readonly IBrokerRepository _brokers;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;

		private readonly Dictionary<long, Dictionary<RoleType, bool>> _isInRole =
			new Dictionary<long, Dictionary<RoleType, bool>>();

		private readonly ISenderRepository _senders;

		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _users;
		private long? _identity;
		private string _twoLetterISOLanguageName;

		public IdentityService(
			IUserRepository users,
			IAdminRepository admins,
			ISenderRepository senders,
			IClientRepository clients,
			IForwarderRepository forwarders,
			IBrokerRepository brokers,
			IUnitOfWork unitOfWork)
		{
			if (HttpContext.Current == null)
				throw new NotSupportedException("UserHolder works only when the HttpContext.Current is presented");

			_users = users;
			_admins = admins;
			_senders = senders;
			_clients = clients;
			_forwarders = forwarders;
			_brokers = brokers;
			_unitOfWork = unitOfWork;
		}

		public bool IsInRole(RoleType role)
		{
			if (!Id.HasValue) return false;

			if (!_isInRole.ContainsKey(Id.Value))
				_isInRole.Add(Id.Value, new Dictionary<RoleType, bool>());

			if (!_isInRole[Id.Value].ContainsKey(role))
			{
				var inRole = InRole(role, Id.Value);

				_isInRole[Id.Value].Add(role, inRole);
			}

			return _isInRole[Id.Value][role];
		}

		public string TwoLetterISOLanguageName
		{
			get
			{
				if (_twoLetterISOLanguageName != null) return _twoLetterISOLanguageName;

				if (Id.HasValue)
				{
					var userId = Id.Value;
					var language = _users.GetLanguage(userId);
					if (language == null)
					{
						FormsAuthentication.SignOut();
						throw new EntityNotFoundException("User " + userId + " not found");
					}
					_twoLetterISOLanguageName = language;
				}
				else
				{
					_twoLetterISOLanguageName = Contracts.Enums.TwoLetterISOLanguageName.Russian;
				}

				return _twoLetterISOLanguageName;
			}
		}

		public void SetTwoLetterISOLanguageName(string value)
		{
			if (value != Contracts.Enums.TwoLetterISOLanguageName.Russian
			    && value != Contracts.Enums.TwoLetterISOLanguageName.Italian
			    && value != Contracts.Enums.TwoLetterISOLanguageName.English)
			{
				throw new ArgumentOutOfRangeException("value");
			}

			if (Id.HasValue)
			{
				_users.SetLanguage(Id.Value, value);
				_unitOfWork.SaveChanges();
			}

			_twoLetterISOLanguageName = value;
		}

		public bool IsAuthenticated
		{
			get { return Id.HasValue; }
		}

		public long? Id
		{
			get
			{
				if (_identity.HasValue)
					return _identity.Value;

				long id;
				if (long.TryParse(HttpContext.Current.User.Identity.Name, NumberStyles.Number, null, out id))
					_identity = id;

				return _identity;
			}
			set { _identity = value; }
		}

		private bool InRole(RoleType role, long userId)
		{
			switch (role)
			{
				case RoleType.Admin:
					return _admins.GetAll().Any(x => x.UserId == userId);

				case RoleType.Sender:
					return _senders.GetByUserId(userId) != null;

				case RoleType.Broker:
					return _brokers.GetByUserId(userId) != null;

				case RoleType.Forwarder:
					return _forwarders.GetAll().Any(x => x.UserId == userId);

				case RoleType.Client:
					return _clients.GetByUserId(userId) != null;

				default:
					throw new ArgumentOutOfRangeException("role");
			}
		}
	}
}