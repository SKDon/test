using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using Alicargo.Core.Enums;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services
{
	// todo: tests
	public sealed class IdentityService : IIdentityService
	{
		private readonly IAuthenticationRepository _authentications;
		private readonly IUnitOfWork _unitOfWork;

		public IdentityService(IAuthenticationRepository authentications, IUnitOfWork unitOfWork)
		{
			if (HttpContext.Current == null)
				throw new NotSupportedException("UserHolder works only when the HttpContext.Current is presented");

			_authentications = authentications;
			_unitOfWork = unitOfWork;
		}

		readonly Dictionary<long, Dictionary<RoleType, bool>> _isInRole = new Dictionary<long, Dictionary<RoleType, bool>>();

		public bool IsInRole(RoleType role)
		{
			if (!Id.HasValue) return false;

			if (!_isInRole.ContainsKey(Id.Value))
				_isInRole.Add(Id.Value, new Dictionary<RoleType, bool>());

			if (!_isInRole[Id.Value].ContainsKey(role))
				_isInRole[Id.Value].Add(role, _authentications.IsInRole(role, Id.Value));

			return _isInRole[Id.Value][role];
		}

		private string _twoLetterISOLanguageName;
		public string TwoLetterISOLanguageName
		{
			get
			{
				if (_twoLetterISOLanguageName != null) return _twoLetterISOLanguageName;

				if (Id.HasValue)
				{
					var user = _authentications.GetById(Id.Value);
					_twoLetterISOLanguageName = user.TwoLetterISOLanguageName;
				}
				else
				{
					_twoLetterISOLanguageName = Core.Localization.TwoLetterISOLanguageName.Russian;
				}

				return _twoLetterISOLanguageName;
			}
			set
			{
				if (value != Core.Localization.TwoLetterISOLanguageName.Russian 
					&& value != Core.Localization.TwoLetterISOLanguageName.Italian
					&& value != Core.Localization.TwoLetterISOLanguageName.English)
				{
					throw new ArgumentOutOfRangeException("value");
				}

				if (Id.HasValue)
				{
					_authentications.SetTwoLetterISOLanguageName(Id.Value, value);
					_unitOfWork.SaveChanges();
				}

				_twoLetterISOLanguageName = value;
			}
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
		private long? _identity;
	}
}