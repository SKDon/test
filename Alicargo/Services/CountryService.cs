using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	internal sealed class CountryService : ICountryService
	{
		private readonly ICountryRepository _countries;

		public CountryService(ICountryRepository countries)
		{
			_countries = countries;
		}

		public ListCollection<CountryData> List(int take, int skip, string language)
		{
			var data = _countries.All(language);

			return new ListCollection<CountryData>
			{
				Data = data.Skip(skip).Take(take).ToArray(),
				Groups = null,
				Total = data.Length
			};
		}

		public CountryEditModel Get(long id)
		{
			var english = _countries.All(TwoLetterISOLanguageName.English).FirstOrDefault(x => x.Id == id);
			if (english == null)
				return null;

			var russian = _countries.All(TwoLetterISOLanguageName.Russian).First(x => x.Id == id);

			return new CountryEditModel
			{
				EnglishName = english.Name,
				RussianName = russian.Name,
				Position = english.Position
			};
		}

		public long Add(CountryEditModel model)
		{
			return _countries.Add(model.EnglishName, model.RussianName, model.Position);
		}

		public void Edit(long id, CountryEditModel model)
		{
			_countries.Update(id, model.EnglishName, model.RussianName, model.Position);
		}
	}
}