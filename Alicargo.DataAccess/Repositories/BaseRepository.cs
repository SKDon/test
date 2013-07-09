using Alicargo.Core.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal abstract class BaseRepository
	{
		protected readonly AlicargoDataContext Context;

		protected BaseRepository(IUnitOfWork unitOfWork)
		{
			Context = (AlicargoDataContext)unitOfWork.Context;
		}
	}
}
