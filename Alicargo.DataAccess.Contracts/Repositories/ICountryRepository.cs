using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ICountryRepository
	{
		CountryData[] Get(params long[] ids);
	}
}
