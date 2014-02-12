using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.DataAccess.Helpers;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class SenderRepository : ISenderRepository
	{
		private readonly IPasswordConverter _converter;
		private readonly ISqlProcedureExecutor _executor;

		public SenderRepository(IPasswordConverter converter, ISqlProcedureExecutor executor)
		{
			_converter = converter;
			_executor = executor;
		}

		public long? GetByUserId(long userId)
		{
			return _executor.Query<long?>("[dbo].[Sender_GetByUser]", new { userId });
		}

		public SenderData Get(long senderId)
		{
			return _executor.Query<SenderData>("[dbo].[Sender_Get]", new { id = senderId });
		}

		public Dictionary<long, decimal> GetTariffs(long[] ids)
		{
			var table = TableParameters.GeIdsTable("Ids", ids);

			var tariffs = _executor.Array<dynamic>("[dbo].[Sender_GetTariffs]", new TableParameters(table));

			return tariffs.ToDictionary(x => (long)x.Id, x => (decimal)x.TariffOfTapePerBox);
		}

		public long Add(SenderData data, string password)
		{
			var salt = _converter.GenerateSalt();
			var passwordHash = _converter.GetPasswordHash(password, salt);

			return _executor.Query<long>("[dbo].[Sender_Add]", new
			{
				data.Login,
				PasswordHash = passwordHash,
				PasswordSalt = salt,
				TwoLetterISOLanguageName = data.Language,
				data.Name,
				data.Email,
				data.TariffOfTapePerBox,
				data.Contact,
				data.Phone,
				data.Address
			});
		}

		public void Update(long senderId, SenderData data)
		{
			_executor.Execute("[dbo].[Sender_Update]", new
			{
				id = senderId,
				data.Login,
				data.Name,
				data.Email,
				data.TariffOfTapePerBox,
				TwoLetterISOLanguageName = data.Language,
				data.Contact,
				data.Phone,
				data.Address
			});
		}

		public UserData[] GetAll()
		{
			return _executor.Array<UserData>("[dbo].[Sender_GetAll]");
		}

		public long GetUserId(long senderId)
		{
			return _executor.Query<long>("[dbo].[Sender_GetUserId]", new { id = senderId });
		}

		public long[] GetByCountry(long countryId)
		{
			return _executor.Array<long>("[dbo].[Sender_GetByCountry]", new { countryId });
		}

		public void SetCountries(long senderId, long[] countries)
		{
			var table = TableParameters.GeIdsTable("CountryIds", countries);

			_executor.Execute("[dbo].[SenderCountry_Set]", new TableParameters(new { senderId }, table));
		}

		public long[] GetCountries(long senderId)
		{
			return _executor.Array<long>("[dbo].[SenderCountry_Get]", new { senderId });
		}
	}
}