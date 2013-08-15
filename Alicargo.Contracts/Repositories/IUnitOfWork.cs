//using System.Data.Linq;

namespace Alicargo.Core.Repositories
{
	public interface IUnitOfWork
	{
		void SaveChanges();
		object Context { get; }
		ITransaction StartTransaction();
	}
}