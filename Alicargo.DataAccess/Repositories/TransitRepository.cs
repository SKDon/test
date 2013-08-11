using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class TransitRepository : BaseRepository, ITransitRepository
	{
		private readonly Expression<Func<Transit, TransitData>> _selector;

		public TransitRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
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

			Context.Transits.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public void Update(TransitData transit)
		{
			var entity = Context.Transits.First(x => x.Id == transit.Id);

			CopyTo(transit, entity);
		}

		public TransitData[] Get(params long[] ids)
		{
			return Context.Transits
						  .Where(x => ids.Contains(x.Id))
						  .Select(_selector)
						  .ToArray();
		}

		public long? GetaApplicationId(long id)
		{
			return Context.Applications
						  .Where(x => x.TransitId == id)
						  .Select(x => x.Id)
						  .FirstOrDefault();
		}

		public TransitData GetByApplication(long id)
		{
			return Context.Applications
						  .Where(x => x.Id == id)
						  .Select(x => x.Transit)
						  .Select(_selector)
						  .FirstOrDefault();
		}

		public TransitData GetByClient(long clientId)
		{
			return Context.Clients
						  .Where(x => x.Id == clientId)
						  .Select(x => x.Transit)
						  .Select(_selector)
						  .FirstOrDefault();
		}

		public void Delete(long transitId)
		{
			var transit = Context.Transits.First(x => x.Id == transitId);

			Context.Transits.DeleteOnSubmit(transit);
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