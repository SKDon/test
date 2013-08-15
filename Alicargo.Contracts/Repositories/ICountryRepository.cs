using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ICountryRepository
	{
		CountryData[] Get(params long[] ids);
	}
}
