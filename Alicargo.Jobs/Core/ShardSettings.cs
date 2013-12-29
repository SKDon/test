namespace Alicargo.Jobs.Core
{
	public sealed class ShardSettings
	{
		public ShardSettings(int zeroBasedIndex, int count)
		{
			ZeroBasedIndex = zeroBasedIndex;
			Count = count;
		}

		public int Count { get; private set; }
		public int ZeroBasedIndex { get; private set; }
	}
}
