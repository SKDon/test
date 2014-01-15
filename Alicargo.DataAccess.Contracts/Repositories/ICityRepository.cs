using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ICityRepository
	{
		CityData[] All(string language);
		long Add(string englishName, string russianName, int position);
		void Update(long id, string englishName, string russianName, int position);
	}
}