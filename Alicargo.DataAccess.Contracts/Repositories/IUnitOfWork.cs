namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IUnitOfWork
	{
		void SaveChanges();
		object Context { get; }
	}
}