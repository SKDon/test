using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	internal sealed class CityService : ICityService
	{
		private readonly ICityRepository _cities;

		public CityService(ICityRepository cities)
		{
			_cities = cities;
		}

		public ListCollection<CityData> List(int take, int skip, string language)
		{
			var data = _cities.All(language);

			return new ListCollection<CityData>
			{
				Data = data.Skip(skip).Take(take).ToArray(),
				Groups = null,
				Total = data.Length
			};
		}
	}
}