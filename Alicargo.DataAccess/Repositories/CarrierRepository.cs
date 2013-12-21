using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class CarrierRepository : ICarrierRepository
	{
		private readonly Expression<Func<Carrier, CarrierData>> _selector;
		private readonly AlicargoDataContext _context;

		public CarrierRepository(IUnitOfWork unitOfWork)			
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_selector = x => new CarrierData
			{
				Name = x.Name,
				Id = x.Id
			};
		}

		public CarrierData[] GetAll()
		{
			return _context.Carriers.Select(_selector).ToArray();
		}

		public Func<long> Add(CarrierData carrier)
		{
			var entity = new Carrier
			{
				Name = carrier.Name,
				Transits = null,
				Id = 0
			};
			_context.Carriers.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public CarrierData Get(string name)
		{
			return _context.Carriers.Select(_selector).FirstOrDefault(x => x.Name.Equals(name));
		}
	}
}