using System.IO;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class TransitRepository : ITransitRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public TransitRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public long Add(TransitData transit)
		{
			if(transit.Id > 0)
			{
				throw new InvalidDataException("Id should be undefined");
			}

			return _executor.Query<long>("[dbo].[Transit_Add]", new
			{
				transit.Address,
				transit.Phone,
				transit.CarrierId,
				transit.CityId,
				transit.DeliveryType,
				transit.MethodOfTransit,
				transit.RecipientName,
				transit.WarehouseWorkingTime
			});
		}

		public void Update(TransitData transit)
		{
			_executor.Execute("[dbo].[Transit_Update]", transit);
		}

		public TransitData[] Get(params long[] ids)
		{
			var idsTable = TableParameters.GeIdsTable("Ids", ids.Distinct().ToArray());

			return _executor.Array<TransitData>("[dbo].[Transit_Get]", new TableParameters(idsTable));
		}

		public TransitData GetByApplication(long applicationId)
		{
			return _executor.Query<TransitData>("[dbo].[Transit_GetByApplication]", new { applicationId });
		}

		public TransitData GetByClient(long clientId)
		{
			return _executor.Query<TransitData>("[dbo].[Transit_GetByClient]", new { clientId });
		}

		public void Delete(long transitId)
		{
			_executor.Execute("[dbo].[Transit_Delete]", new { Id = transitId });
		}
	}
}