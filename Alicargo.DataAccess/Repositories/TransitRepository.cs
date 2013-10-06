using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class TransitRepository : ITransitRepository
	{
		private readonly Expression<Func<Transit, TransitData>> _selector;
		private readonly AlicargoDataContext _context;

		public TransitRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_selector = x => new TransitData
			{
				Address = x.Address,
				CarrierId = x.CarrierId,
				Id = x.Id,
				City = x.City,
				DeliveryTypeId = x.DeliveryTypeId,
				MethodOfTransitId = x.MethodOfTransitId,
				Phone = x.Phone,
				RecipientName = x.RecipientName,
				WarehouseWorkingTime = x.WarehouseWorkingTime
			};
		}

		public Func<long> Add(TransitData transit)
		{
			var entity = new Transit();

			CopyTo(transit, entity);

			_context.Transits.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public void Update(TransitData transit)
		{
			var entity = _context.Transits.First(x => x.Id == transit.Id);

			CopyTo(transit, entity);
		}

		public TransitData[] Get(params long[] ids)
		{
			return _context.Transits
						  .Where(x => ids.Contains(x.Id))
						  .Select(_selector)
						  .ToArray();
		}

		public long? GetaApplicationId(long id)
		{
			return _context.Applications
						  .Where(x => x.TransitId == id)
						  .Select(x => x.Id)
						  .FirstOrDefault();
		}

		public TransitData GetByApplication(long id)
		{
			return _context.Applications
						  .Where(x => x.Id == id)
						  .Select(x => x.Transit)
						  .Select(_selector)
						  .FirstOrDefault();
		}

		public TransitData GetByClient(long clientId)
		{
			return _context.Clients
						  .Where(x => x.Id == clientId)
						  .Select(x => x.Transit)
						  .Select(_selector)
						  .FirstOrDefault();
		}

		public void Delete(long transitId)
		{
			var transit = _context.Transits.First(x => x.Id == transitId);

			_context.Transits.DeleteOnSubmit(transit);
		}

		public static void CopyTo(TransitData from, Transit to)
		{
			to.City = from.City;
			to.Address = from.Address;
			to.RecipientName = from.RecipientName;
			to.Phone = from.Phone;
			to.MethodOfTransitId = from.MethodOfTransitId;
			to.DeliveryTypeId = from.DeliveryTypeId;
			to.CarrierId = from.CarrierId;
			to.WarehouseWorkingTime = from.WarehouseWorkingTime;
		}
	}
}