using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ICityService
	{
		ListCollection<CityData> List(int take, int skip, string language);
	}
}