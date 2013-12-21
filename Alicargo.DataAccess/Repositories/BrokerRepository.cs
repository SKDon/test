using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class BrokerRepository : IBrokerRepository
	{
		private readonly Expression<Func<Broker, BrokerData>> _selector;
		private readonly AlicargoDataContext _context;

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
	}
}
