using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
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


		public void Update(long id, string name, string login, string email, long cityId)
		{
			_executor.Execute("[dbo].[Forwarder_Update]", new { id, name, login, email, cityId });
		}

		public long Add(string name, string login, string password, string email, string language, long cityId)
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
				email,
				cityId
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
	}
}