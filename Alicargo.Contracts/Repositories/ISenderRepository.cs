namespace Alicargo.Contracts.Repositories
{
	public  interface ISenderRepository
	{
		long? GetByUserId(long userId);
	}
}
