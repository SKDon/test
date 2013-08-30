using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class UserRepository : BaseRepository, IUserRepository
	{
		private readonly IPasswordConverter _converter;

		public UserRepository(IUnitOfWork unitOfWork, IPasswordConverter converter)
			: base(unitOfWork)
		{
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
					return Context.Admins.Select(x => new UserData
					{
						Id = x.Id,
						UserId = x.UserId,
						Name = x.Name,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Brocker:
					return Context.Brockers.Select(x => new UserData
					{
						Id = x.Id,
						UserId = x.UserId,
						Name = x.Name,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Client:
					return Context.Clients.Select(x => new UserData
					{
						Id = x.Id,
						UserId = x.UserId,
						Name = x.LegalEntity,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Forwarder:
					return Context.Forwarders.Select(x => new UserData
					{
						Id = x.Id,
						UserId = x.UserId,
						Name = x.Name,
						Login = x.User.Login,
						Email = x.Email,
						TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
					}).ToArray();

				case RoleType.Sender:
					return Context.Senders.Select(x => new UserData
					{
						Id = x.Id,
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

		public void UpdateSender(long id, string name, string login, string email, string newPassword)
		{
			var entity = Context.Senders.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
			SetNewPassword(newPassword, entity.User);
		}

		public void UpdateAdmin(long id, string name, string login, string email, string newPassword)
		{
			var entity = Context.Admins.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
			SetNewPassword(newPassword, entity.User);
		}

		public void UpdateBrocker(long id, string name, string login, string email, string newPassword)
		{
			var entity = Context.Brockers.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
			SetNewPassword(newPassword, entity.User);
		}

		public void UpdateForwarder(long id, string name, string login, string email, string newPassword)
		{
			var entity = Context.Forwarders.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
			SetNewPassword(newPassword, entity.User);
		}

		public void AddForwarder(long id, string name, string login, string email, string newPassword, string twoLetterISOLanguageName)
		{
			Context.Forwarders.InsertOnSubmit(new Forwarder
			{
				Name = name,
				User = SetNewPassword(newPassword, new User { Login = login, TwoLetterISOLanguageName = twoLetterISOLanguageName }),
				Email = email
			});
		}

		public void AddBrocker(long id, string name, string login, string email, string newPassword, string twoLetterISOLanguageName)
		{
			Context.Brockers.InsertOnSubmit(new Brocker
			{
				Name = name,
				User = SetNewPassword(newPassword, new User { Login = login, TwoLetterISOLanguageName = twoLetterISOLanguageName }),
				Email = email
			});
		}

		public void AddSender(long id, string name, string login, string email, string newPassword, string twoLetterISOLanguageName)
		{
			Context.Senders.InsertOnSubmit(new Sender
			{
				Name = name,
				User = SetNewPassword(newPassword, new User { Login = login, TwoLetterISOLanguageName = twoLetterISOLanguageName }),
				Email = email
			});
		}

		public void AddAdmin(long id, string name, string login, string email, string newPassword, string twoLetterISOLanguageName)
		{
			Context.Admins.InsertOnSubmit(new Admin
			{
				Name = name,
				User = SetNewPassword(newPassword, new User { Login = login, TwoLetterISOLanguageName = twoLetterISOLanguageName }),
				Email = email
			});
		}
	}
}
