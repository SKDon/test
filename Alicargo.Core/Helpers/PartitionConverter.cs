namespace Alicargo.Core.Helpers
{
	public sealed class PartitionConverter : IPartitionConverter
	{
		private readonly int _partitionCount;

		public PartitionConverter(int partitionCount)
		{
			_partitionCount = partitionCount;
		}

		public int GetKey(long id)
		{
			return (int)(id % _partitionCount);
		}
	}
}