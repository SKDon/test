using System.Linq;
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

		public void UpdateAdmin(long id, string name, string login, string email)
		{
			var entity = _context.Admins.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
		}

		public void AddAdmin(long id, string name, string login, string email, string language)
		{
			_context.Admins.InsertOnSubmit(new Admin
			{
				Name = name,
				User = new DbContext.User
				{
					Login = login,
					TwoLetterISOLanguageName = language,
					PasswordHash = new byte[0],
					PasswordSalt = new byte[0]
				},
				Email = email
			});
		}
	}
}