using System;
using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class TransitRepository : BaseRepository, ITransitRepository
	{
		public TransitRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public Func<long> Add(TransitData transit)
		{
			var entity = new DbContext.Transit();

			transit.CopyTo(entity);

			Context.Transits.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public void Update(TransitData transit)
		{
			var entity = Context.Transits.First(x => x.Id == transit.Id);

			transit.CopyTo(entity);
		}

		public TransitData[] Get(params long[] ids)
		{
			return Context.Transits
				.Where(x => ids.Contains(x.Id))
				.Select(x => new TransitData
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
				})
				.ToArray();
		}

		public void Delete(long transitId)
		{
			var transit = Context.Transits.First(x => x.Id == transitId);
			Context.Transits.DeleteOnSubmit(transit);
		}
	}
}