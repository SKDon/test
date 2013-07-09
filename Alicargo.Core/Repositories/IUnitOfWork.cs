using System.Data.Linq;

namespace Alicargo.Core.Repositories
{
	public interface IUnitOfWork
	{
		void SaveChanges();
		DataContext Context { get; }
		ITransaction GetTransactionScope();
	}
}