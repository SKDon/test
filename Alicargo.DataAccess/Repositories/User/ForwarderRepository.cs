using System.Linq;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class ForwarderRepository : IForwarderRepository
	{
		private readonly AlicargoDataContext _context;

		public ForwarderRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public void Update(long forwarderId, string name, string login, string email)
		{
			var entity = _context.Forwarders.First(x => x.Id == forwarderId);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
		}

		public void Add(string name, string login, string email, string twoLetterISOLanguageName)
		{
			_context.Forwarders.InsertOnSubmit(new Forwarder
			{
				Name = name,
				User = new DbContext.User
				{
					Login = login,
					TwoLetterISOLanguageName = twoLetterISOLanguageName,
					PasswordHash = new byte[0],
					PasswordSalt = new byte[0]
				},
				Email = email
			});
		}

		public UserData[] GetAll()
		{
			return _context.Forwarders.Select(x => new UserData
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