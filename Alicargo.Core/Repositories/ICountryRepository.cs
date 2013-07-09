using Alicargo.Core.Contracts;

namespace Alicargo.Core.Repositories
{
	public interface ICountryRepository
	{
		CountryData[] Get(params long[] ids);
	}
}
