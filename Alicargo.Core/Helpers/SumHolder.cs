namespace Alicargo.Core.Helpers
{
	public sealed class SumHolder<T>
	{
		public SumHolder(T value)
		{
			sum = value;
		}

		// ReSharper disable InconsistentNaming
		public T sum { get; private set; } // ReSharper restore InconsistentNaming
	}
}