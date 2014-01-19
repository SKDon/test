using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class CountryRepository : ICountryRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public CountryRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public CountryData[] All(string language)
		{
			return _executor.Array<CountryData>("[dbo].[Country_GetList]", new { language });
		}

		public long Add(string englishName, string russianName, int position)
		{
			return _executor.Query<long>("[dbo].[Country_Add]", new { englishName, russianName, position });
		}

		public void Update(long id, string englishName, string russianName, int position)
		{
			_executor.Execute("[dbo].[Country_Update]", new { englishName, russianName, position, id });
		}
	}
}