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

			var tariffs = _executor.Array<SenderTariff>("[dbo].[Sender_GetTariffs]", new TableParameters(table));

			return tariffs.ToDictionary(x => x.Id, x => x.TariffOfTapePerBox);
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
				data.CountryId
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
				data.CountryId
			});
		}

		public UserData[] GetAll()
		{
			return _executor.Array<UserData>("[dbo].[Sender_GetAll]");
		}

		public long[] GetByCountry(long countryId)
		{
			throw new System.NotImplementedException();
		}

		public long GetUserId(long senderId)
		{
			return _executor.Query<long>("[dbo].[Sender_GetUserId]", new { id = senderId });
		}

		// ReSharper disable ClassNeverInstantiated.Local
		private sealed class SenderTariff// ReSharper restore ClassNeverInstantiated.Local
		{
			// ReSharper disable UnusedAutoPropertyAccessor.Local
			public decimal TariffOfTapePerBox { get; set; }
			public long Id { get; set; }// ReSharper restore UnusedAutoPropertyAccessor.Local
		}
	}
}