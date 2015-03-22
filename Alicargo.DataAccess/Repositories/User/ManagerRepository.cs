using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	// todo: 1. tests (262)
	public sealed class ManagerRepository : IManagerRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<Manager, UserEntityData>> _selector;

		public ManagerRepository(IDbConnection connection)
		{
			_context = new AlicargoDataContext(connection);

			_selector = x => new UserEntityData
			{
				EntityId = x.Id,
				UserId = x.UserId,
				Name = x.Name,
				Login = x.User.Login,
				Email = x.Email,
				Language = x.User.TwoLetterISOLanguageName
			};
		}

		public long Update(long managerId, string name, string login, string email)
		{
			var entity = _context.Managers.First(x => x.Id == managerId);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;

			_context.SubmitChanges();

			return entity.UserId;
		}

		public long Add(string name, string login, string email, string language)
		{
			var user = new DbContext.User
			{
				Login = login,
				TwoLetterISOLanguageName = language,
				PasswordHash = new byte[0],
				PasswordSalt = new byte[0]
			};

			_context.Managers.InsertOnSubmit(new Manager
			{
				Name = name,
				User = user,
				Email = email
			});

			_context.SubmitChanges();

			return user.Id;
		}

		public UserEntityData[] GetAll()
		{
			return _context.Managers.Select(_selector).ToArray();
		}
	}
}