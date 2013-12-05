using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ICityRepository
	{
		CityData[] All(string language);
	}
}