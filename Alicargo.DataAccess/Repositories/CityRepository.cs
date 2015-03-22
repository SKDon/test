using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;

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

		public long Add(string englishName, string russianName, int position)
		{
			return _executor.Query<long>("[dbo].[City_Add]", new { englishName, russianName, position });
		}

		public void Update(long id, string englishName, string russianName, int position)
		{
			_executor.Execute("[dbo].[City_Update]", new { englishName, russianName, position, id });
		}
	}
}