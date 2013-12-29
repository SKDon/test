namespace Alicargo.Core.Helpers
{
	public interface IPartitionConverter
	{
		int GetKey(long id);
	}
}
