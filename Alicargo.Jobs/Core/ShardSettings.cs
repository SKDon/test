namespace Alicargo.Jobs.Core
{
	public sealed class ShardSettings
	{
		public ShardSettings(int zeroBasedIndex, int total)
		{
			ZeroBasedIndex = zeroBasedIndex;
			Total = total;
		}

		public int Total { get; private set; }
		public int ZeroBasedIndex { get; private set; }
	}
}
