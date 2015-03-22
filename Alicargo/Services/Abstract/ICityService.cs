using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ICityService
	{
		ListCollection<CityData> List(int take, int skip, string language);
		CityEditModel Get(long id);
		long Add(CityEditModel model);
		void Edit(long id, CityEditModel model);
	}
}