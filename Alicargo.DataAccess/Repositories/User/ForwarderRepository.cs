using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.DataAccess.Helpers;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class ForwarderRepository : IForwarderRepository
	{
		private readonly IPasswordConverter _converter;
		private readonly ISqlProcedureExecutor _executor;

		public ForwarderRepository(IPasswordConverter converter, ISqlProcedureExecutor executor)
		{
			_converter = converter;
			_executor = executor;
		}

		public void Update(long id, string name, string login, string email)
		{
			_executor.Execute("[dbo].[Forwarder_Update]", new { id, name, login, email });
		}

		public long Add(string name, string login, string password, string email, string language)
		{
			var salt = _converter.GenerateSalt();
			var passwordHash = _converter.GetPasswordHash(password, salt);

			return _executor.Query<long>("[dbo].[Forwarder_Add]", new
			{
				login,
				PasswordHash = passwordHash,
				PasswordSalt = salt,
				language,
				name,
				email
			});
		}

		public ForwarderData[] GetAll()
		{
			return _executor.Array<ForwarderData>("[dbo].[Forwarder_GetAll]");
		}

		public ForwarderData Get(long id)
		{
			return _executor.Query<ForwarderData>("[dbo].[Forwarder_Get]", new { id });
		}

		public long[] GetCities(long forwarderId)
		{
			return _executor.Array<long>("[dbo].[ForwarderCity_Get]", new { forwarderId });
		}

		public void SetCities(long forwarderId, long[] cities)
		{
			var table = TableParameters.GeIdsTable("CityIds", cities);

			_executor.Execute("[dbo].[ForwarderCity_Set]", new TableParameters(new { forwarderId }, table));
		}

		public long[] GetByCity(long cityId)
		{
			return _executor.Array<long>("[dbo].[Forwarder_GetByCity]", new { cityId });
		}
	}
}