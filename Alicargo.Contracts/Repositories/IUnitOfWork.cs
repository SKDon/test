//using System.Data.Linq;

namespace Alicargo.Contracts.Repositories
{
	public interface IUnitOfWork
	{
		void SaveChanges();
		object Context { get; }
		ITransaction StartTransaction();
	}
}