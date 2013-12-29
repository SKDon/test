namespace Alicargo.Core.Event
{
	public interface IPartitionConverter
	{
		int GetKey(long id);
	}
}
