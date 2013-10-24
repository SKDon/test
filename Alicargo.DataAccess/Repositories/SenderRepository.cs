using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class SenderRepository : ISenderRepository
	{
		private readonly IPasswordConverter _converter;
		private readonly ISqlProcedureExecutor _executor;
		private readonly AlicargoDataContext _context;

		public SenderRepository(IUnitOfWork unitOfWork, IPasswordConverter converter, ISqlProcedureExecutor executor)
		{
			_converter = converter;
			_executor = executor;
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public long? GetByUserId(long userId)
		{
			return _context.Senders.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefault();
		}

		public SenderData Get(long id)
		{
			return _executor.Query<SenderData>("[dbo].[Sender_Get]", new { id });
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
				TwoLetterISOLanguageName = TwoLetterISOLanguageName.English,

				data.Name,
				data.Email,
				data.TariffOfTapePerBox
			});
		}

		public void Update(long id, SenderData data)
		{
			_executor.Execute("[dbo].[Sender_Update]", new
			{
				id,
				data.Login,
				data.Name,
				data.Email,
				data.TariffOfTapePerBox
			});
		}

		public long GetUserId(long id)
		{
			return _executor.Query<long>("[dbo].[Sender_GetUserId]", new { id });
		}
	}
}
