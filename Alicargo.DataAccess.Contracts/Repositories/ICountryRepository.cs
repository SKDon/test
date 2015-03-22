using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ICountryRepository
	{
		CountryData[] All(string language);
		long Add(string englishName, string russianName, int position);
		void Update(long id, string englishName, string russianName, int position);
	}
}
