using System.Linq;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class AdminRepository : IAdminRepository
	{
		private readonly AlicargoDataContext _context;

		public AdminRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public long Update(long adminId, string name, string login, string email)
		{
			var entity = _context.Admins.First(x => x.Id == adminId);
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

			_context.Admins.InsertOnSubmit(new Admin
			{
				Name = name,
				User = user,
				Email = email
			});

			_context.SubmitChanges();

			return user.Id;
		}

		public UserData[] GetAll()
		{
			return _context.Admins.Select(x => new UserData
			{
				EntityId = x.Id,
				UserId = x.UserId,
				Name = x.Name,
				Login = x.User.Login,
				Email = x.Email,
				TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
			}).ToArray();
		}
	}
}