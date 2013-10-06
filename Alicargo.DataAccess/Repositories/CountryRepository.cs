using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	sealed class CountryRepository : ICountryRepository
	{
		private readonly AlicargoDataContext _context;

		public CountryRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public CountryData[] Get(params long[] ids)
		{
			var countries = ids.Length > 0
				? _context.Countries.Where(x => ids.Contains(x.Id)).ToArray()
				: _context.Countries.ToArray();

			return countries.Select(x => new CountryData(x.Id, new Dictionary<string, string>
				{
					{ TwoLetterISOLanguageName.English, x.Name_En },
					{ TwoLetterISOLanguageName.Italian, x.Name_En },
					{ TwoLetterISOLanguageName.Russian, x.Name_Ru },
				})).ToArray();
		}
	}
}
