using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class CarrierRepository : BaseRepository, ICarrierRepository
	{
		private readonly Expression<Func<DbContext.Carrier, Carrier>> _selector;

		public CarrierRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			_selector = x => new Carrier
			{
				Name = x.Name,
				Id = x.Id
			};
		}

		public Carrier[] GetAll()
		{
			return Context.Carriers.Select(_selector).ToArray();
		}

		public Func<long> Add(Carrier carrier)
		{
			var entity = new DbContext.Carrier
			{
				Name = carrier.Name,
				Transits = null,
				Id = 0
			};
			Context.Carriers.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public Carrier Get(string name)
		{
			return Context.Carriers.Select(_selector).FirstOrDefault(x => x.Name.Equals(name));
		}
	}
}