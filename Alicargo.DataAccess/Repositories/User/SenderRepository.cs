using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories.User
{
	internal sealed class SenderRepository : ISenderRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly IPasswordConverter _converter;
		private readonly ISqlProcedureExecutor _executor;

		public SenderRepository(IUnitOfWork unitOfWork, IPasswordConverter converter, ISqlProcedureExecutor executor)
		{
			_converter = converter;
			_executor = executor;
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public long? GetByUserId(long userId)
		{
			return _context.Senders.Where(x => x.UserId == userId).Select(x => (long?)x.Id).FirstOrDefault();
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
				TwoLetterISOLanguageName = TwoLetterISOLanguageName.English,
				data.Name,
				data.Email,
				data.TariffOfTapePerBox
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
				data.TariffOfTapePerBox
			});
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