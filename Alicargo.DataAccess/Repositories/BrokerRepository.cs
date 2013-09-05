using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class BrokerRepository : BaseRepository, IBrokerRepository
	{
		private readonly Expression<Func<DbContext.Broker, BrokerData>> _selector;

		public BrokerRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
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
			return Context.Brokers.Where(x => x.Id == brokerId).Select(_selector).FirstOrDefault();
		}

		public BrokerData GetByUserId(long userId)
		{
			return Context.Brokers.Where(x => x.UserId == userId).Select(_selector).FirstOrDefault();
		}

		public BrokerData[] GetAll()
		{
			return Context.Brokers.Select(_selector).ToArray();
		}
	}
}
