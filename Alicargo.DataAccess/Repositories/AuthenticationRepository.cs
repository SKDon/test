using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
    internal sealed class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        private readonly IPasswordConverter _converter;

        public AuthenticationRepository(IUnitOfWork unitOfWork, IPasswordConverter converter)
            : base(unitOfWork)
        {
            _converter = converter;
        }

        public bool IsInRole(RoleType role, long userId)
        {
            switch (role)
            {
                case RoleType.Admin:
                    return Context.Admins.Any(x => x.UserId == userId);

                case RoleType.Broker:
                    return Context.Brokers.Any(x => x.UserId == userId);

                case RoleType.Client:
                    return Context.Clients.Any(x => x.UserId == userId);

                case RoleType.Forwarder:
                    return Context.Forwarders.Any(x => x.UserId == userId);

                case RoleType.Sender:
                    return Context.Senders.Any(x => x.UserId == userId);

                default:
                    throw new ArgumentOutOfRangeException("role");
            }
        }

        public void SetTwoLetterISOLanguageName(long id, string twoLetterISOLanguageName)
        {
            var user = Context.Users.First(x => x.Id == id);
            user.TwoLetterISOLanguageName = twoLetterISOLanguageName;
        }

        public AuthenticationData GetByClientId(long clientId)
        {
            var enitity = Context.Clients
                                 .Where(x => x.Id == clientId)
                                 .Select(x => x.User)
                                 .First();

            return MapUser(enitity);
        }

        public AuthenticationData GetByLogin(string login)
        {
            // ReSharper disable SpecifyStringComparison
            var enitity = Context.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
            // ReSharper restore SpecifyStringComparison

            return MapUser(enitity);
        }

        public AuthenticationData GetById(long id)
        {
            var enitity = Context.Users.FirstOrDefault(x => x.Id == id);

            return MapUser(enitity);
        }

        public Func<long> Add(string login, string password, string twoLetterISOLanguageName)
        {
            var user = new User
                {
                    Login = login,
                    TwoLetterISOLanguageName = twoLetterISOLanguageName
                };
            SetNewPassword(password, user);

            Context.Users.InsertOnSubmit(user);

            return () => user.Id;
        }

        public void Update(long userId, string newLogin, string newPassword = null)
        {
            if (string.IsNullOrWhiteSpace(newLogin)) throw new ArgumentNullException("newLogin");

            var entity = Context.Users.First(x => x.Id == userId);

            SetNewPassword(newPassword, entity);

            entity.Login = newLogin;
        }

        private static AuthenticationData MapUser(User enitity)
        {
            return enitity == null
                       ? null
                       : new AuthenticationData
                           {
                               Id = enitity.Id,
                               Login = enitity.Login,
                               PasswordHash = enitity.PasswordHash.ToArray(),
                               PasswordSalt = enitity.PasswordSalt.ToArray(),
                               TwoLetterISOLanguageName = enitity.TwoLetterISOLanguageName
                           };
        }

        private void SetNewPassword(string password, User user)
        {
            if (string.IsNullOrWhiteSpace(password)) return;

            user.PasswordSalt = _converter.GenerateSalt();
            user.PasswordHash = _converter.GetPasswordHash(password, user.PasswordSalt.ToArray());
        }
    }
}