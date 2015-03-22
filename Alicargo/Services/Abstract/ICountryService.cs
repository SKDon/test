using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ICountryService
	{
		ListCollection<CountryData> List(int take, int skip, string language);
		CountryEditModel Get(long id);
		long Add(CountryEditModel model);
		void Edit(long id, CountryEditModel model);
	}
}