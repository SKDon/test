using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
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

		public CityEditModel Get(long id)
		{
			var english = _cities.All(TwoLetterISOLanguageName.English).FirstOrDefault(x => x.Id == id);
			if (english == null)
				return null;

			var russian = _cities.All(TwoLetterISOLanguageName.Russian).First(x => x.Id == id);

			return new CityEditModel
			{
				EnglishName = english.Name,
				RussianName = russian.Name,
				Position = english.Position
			};
		}

		public long Add(CityEditModel model)
		{
			return _cities.Add(model.EnglishName, model.RussianName, model.Position);
		}

		public void Edit(long id, CityEditModel model)
		{
			_cities.Update(id, model.EnglishName, model.RussianName, model.Position);
		}
	}
}