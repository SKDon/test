using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.DataAccess.Helpers;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class CarrierRepository : ICarrierRepository
	{
		private readonly IPasswordConverter _converter;
		private readonly ISqlProcedureExecutor _executor;

		public CarrierRepository(IPasswordConverter converter, ISqlProcedureExecutor executor)
		{
			_converter = converter;
			_executor = executor;
		}

		public void Update(long id, string name, string email, string phone, string contact, string address, string login)
		{
			_executor.Execute("[dbo].[Carrier_Update]", new
			{
				id,
				name,
				login,
				email,
				phone,
				contact,
				address
			});
		}

		public long Add(string name, string email, string phone, string contact, string address,
			string login, string password, string language)
		{
			var salt = _converter.GenerateSalt();
			var passwordHash = _converter.GetPasswordHash(password, salt);

			return _executor.Query<long>("[dbo].[Carrier_Add]", new
			{
				login,
				PasswordHash = passwordHash,
				PasswordSalt = salt,
				language,
				name,
				email,
				phone,
				contact,
				address
			});
		}

		public CarrierData[] GetAll()
		{
			return _executor.Array<CarrierData>("[dbo].[Carrier_GetAll]");
		}

		public CarrierData Get(long id)
		{
			return _executor.Query<CarrierData>("[dbo].[Carrier_Get]", new { id });
		}

		public long[] GetCities(long carrierId)
		{
			return _executor.Array<long>("[dbo].[CarrierCity_Get]", new { carrierId });
		}

		public void SetCities(long carrierId, long[] cities)
		{
			var table = TableParameters.GeIdsTable("CityIds", cities);

			_executor.Execute("[dbo].[CarrierCity_Set]", new TableParameters(new { carrierId }, table));
		}

		public long[] GetByCity(long cityId)
		{
			return _executor.Array<long>("[dbo].[Carrier_GetByCity]", new { cityId });
		}

		public long? GetByUserId(long userId)
		{
			return _executor.Query<long?>("[dbo].[Carrier_GetByUserId]", new { userId });
		}
	}
}