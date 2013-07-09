using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class BrockerRepository : BaseRepository, IBrockerRepository
	{
		private readonly Expression<Func<DbContext.Brocker, Brocker>> _selector;

		public BrockerRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			_selector = x => new Brocker
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName
			};
		}

		public Brocker Get(long brockerId)
		{
			return Context.Brockers.Where(x => x.Id == brockerId).Select(_selector).FirstOrDefault();
		}

		public Brocker GetByUserId(long userId)
		{
			return Context.Brockers.Where(x => x.UserId == userId).Select(_selector).FirstOrDefault();
		}

		public Brocker[] GetAll()
		{
			return Context.Brockers.Select(_selector).ToArray();
		}
	}
}
