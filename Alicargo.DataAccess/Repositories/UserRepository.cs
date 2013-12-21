using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class UserRepository : IUserRepository
	{
		private readonly IPasswordConverter _converter;
		private readonly AlicargoDataContext _context;

		public UserRepository(IUnitOfWork unitOfWork, IPasswordConverter converter)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_converter = converter;
		}

		private User SetNewPassword(string password, User user)
		{
			if (!string.IsNullOrWhiteSpace(password))
			{
				user.PasswordSalt = _converter.GenerateSalt();
				user.PasswordHash = _converter.GetPasswordHash(password, user.PasswordSalt.ToArray());
			}

			return user;
		}

		public UserData[] GetByRole(RoleType role)
		{
			switch (role)
			{
				case RoleType.Admin:
					return _context.Admins.Select(x => new UserData
					{
						EntityId = x.Id,
						UserId = x.UserId,
						Name = x.Name,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Broker:
					return _context.Brokers.Select(x => new UserData
					{
						EntityId = x.Id,
						UserId = x.UserId,
						Name = x.Name,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Client:
					return _context.Clients.Select(x => new UserData
					{
						EntityId = x.Id,
						UserId = x.UserId,
						Name = x.LegalEntity,
						Login = x.User.Login,
						Email = x.Emails,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Forwarder:
					return _context.Forwarders.Select(x => new UserData
					{
						EntityId = x.Id,
						UserId = x.UserId,
						Name = x.Name,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Sender:
					return _context.Senders.Select(x => new UserData
					{
						EntityId = x.Id,
						UserId = x.UserId,
						Name = x.Name,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				default:
					throw new ArgumentOutOfRangeException("role");
			}
		}

		public void UpdateAdmin(long id, string name, string login, string email, string newPassword)
		{
			var entity = _context.Admins.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
			SetNewPassword(newPassword, entity.User);
		}

		public void UpdateBroker(long id, string name, string login, string email, string newPassword)
		{
			var entity = _context.Brokers.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
			SetNewPassword(newPassword, entity.User);
		}

		public void UpdateForwarder(long id, string name, string login, string email, string newPassword)
		{
			var entity = _context.Forwarders.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
			SetNewPassword(newPassword, entity.User);
		}

		public void AddForwarder(long id, string name, string login, string email, string newPassword, string twoLetterISOLanguageName)
		{
			_context.Forwarders.InsertOnSubmit(new Forwarder
			{
				Name = name,
				User = SetNewPassword(newPassword, new User { Login = login, TwoLetterISOLanguageName = twoLetterISOLanguageName }),
				Email = email
			});
		}

		public void AddBroker(long id, string name, string login, string email, string newPassword, string twoLetterISOLanguageName)
		{
			_context.Brokers.InsertOnSubmit(new Broker
			{
				Name = name,
				User = SetNewPassword(newPassword, new User { Login = login, TwoLetterISOLanguageName = twoLetterISOLanguageName }),
				Email = email
			});
		}

		public void AddAdmin(long id, string name, string login, string email, string newPassword, string twoLetterISOLanguageName)
		{
			_context.Admins.InsertOnSubmit(new Admin
			{
				Name = name,
				User = SetNewPassword(newPassword, new User { Login = login, TwoLetterISOLanguageName = twoLetterISOLanguageName }),
				Email = email
			});
		}
	}
}
