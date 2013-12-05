using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class CityRepository : ICityRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public CityRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public CityData[] All(string language)
		{
			return _executor.Array<CityData>("[dbo].[City_GetList]", new { language });
		}
	}
}