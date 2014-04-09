using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class AdminRepository : IAdminRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<Admin, UserData>> _selector;

		public AdminRepository(IDbConnection connection)
		{
			_context = new AlicargoDataContext(connection);

			_selector = x => new UserData
			{
				EntityId = x.Id,
				UserId = x.UserId,
				Name = x.Name,
				Login = x.User.Login,
				Email = x.Email,
				Language = x.User.TwoLetterISOLanguageName
			};
		}

		public long Update(long adminId, string name, string login, string email)
		{
			var entity = _context.Admins.First(x => x.Id == adminId);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;

			_context.SaveChanges();

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

			_context.Admins.InsertOnSubmit(new Admin
			{
				Name = name,
				User = user,
				Email = email
			});

			_context.SaveChanges();

			return user.Id;
		}

		public UserData[] GetAll()
		{			
			return _context.Admins.Select(_selector).ToArray();
		}
	}
}