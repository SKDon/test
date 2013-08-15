using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	sealed class CountryRepository : BaseRepository, ICountryRepository
	{
		public CountryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public CountryData[] Get(params long[] ids)
		{
			var countries = ids.Length > 0
				? Context.Countries.Where(x => ids.Contains(x.Id)).ToArray()
				: Context.Countries.ToArray();

			return countries.Select(x => new CountryData(x.Id, new Dictionary<string, string>
				{
					{ TwoLetterISOLanguageName.English, x.Name_En },
					{ TwoLetterISOLanguageName.Italian, x.Name_En },
					{ TwoLetterISOLanguageName.Russian, x.Name_Ru },
				})).ToArray();
		}
	}
}
