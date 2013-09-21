using System.Linq;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class SenderRepository : BaseRepository, ISenderRepository
	{
		public SenderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public long? GetByUserId(long userId)
		{
			return Context.Senders.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefault();
		}
	}
}
