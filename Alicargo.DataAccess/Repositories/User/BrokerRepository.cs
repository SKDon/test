using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class BrokerRepository : IBrokerRepository
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
				UserId = x.UserId,
				TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName,
				Login = x.User.Login
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

		public long Update(long brokerId, string name, string login, string email)
		{
			var entity = _context.Brokers.First(x => x.Id == brokerId);
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

			_context.Brokers.InsertOnSubmit(new Broker
			{
				Name = name,
				User = user,
				Email = email
			});

			_context.SubmitChanges();

			return user.Id;
		}
	}
}