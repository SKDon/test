namespace Alicargo.Core.Contracts.Event
{
	public interface IPartitionConverter
	{
		int GetKey(long id);
	}
}
