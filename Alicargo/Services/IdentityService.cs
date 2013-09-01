using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Security;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Services.Abstract;

namespace Alicargo.Services
{
	// todo: 2. chached interception
    internal sealed class IdentityService : IIdentityService
	{
		private readonly IAuthenticationRepository _authentications;
		private readonly IUnitOfWork _unitOfWork;

		public IdentityService(
			IAuthenticationRepository authentications, 
			IUnitOfWork unitOfWork)
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
					var userId = Id.Value;
					var user = _authentications.GetById(userId);
					if (user == null)
					{
						FormsAuthentication.SignOut();
						throw new EntityNotFoundException("User " + userId + " not found");
					}
					_twoLetterISOLanguageName = user.TwoLetterISOLanguageName;
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
                _authentications.SetTwoLetterISOLanguageName(Id.Value, value);
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
		private long? _identity;
	}
}