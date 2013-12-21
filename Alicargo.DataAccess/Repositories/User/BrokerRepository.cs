using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	internal sealed class BrokerRepository : IBrokerRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<Broker, BrokerData>> _selector;

		public BrokerRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_selector = x => new BrokerData
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
			};
		}

		public BrokerData Get(long brokerId)
		{
			return _context.Brokers.Where(x => x.Id == brokerId).Select(_selector).FirstOrDefault();
		}

		public BrokerData GetByUserId(long userId)
		{
			return _context.Brokers.Where(x => x.UserId == userId).Select(_selector).FirstOrDefault();
		}

		public BrokerData[] GetAll()
		{
			return _context.Brokers.Select(_selector).ToArray();
		}

		public void UpdateBroker(long id, string name, string login, string email)
		{
			var entity = _context.Brokers.First(x => x.Id == id);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;
		}

		public void AddBroker(long id, string name, string login, string email, string twoLetterISOLanguageName)
		{
			_context.Brokers.InsertOnSubmit(new Broker
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
	}
}