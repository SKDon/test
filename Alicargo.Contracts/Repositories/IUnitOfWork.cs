namespace Alicargo.Contracts.Repositories
{
	public interface IUnitOfWork
	{
		void SaveChanges();
		object Context { get; } // todo: 3.5. hide this property
		ITransaction StartTransaction();
	}
}