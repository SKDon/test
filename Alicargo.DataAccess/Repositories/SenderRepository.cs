using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class SenderRepository : ISenderRepository
	{
		private readonly AlicargoDataContext _context;

		public SenderRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public long? GetByUserId(long userId)
		{
			return _context.Senders.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefault();
		}
	}
}
