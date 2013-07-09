using Alicargo.Core.Helpers;

namespace Alicargo.ViewModels
{
	public sealed class ListCollection<T>
	{
		public T[] Data { get; set; }
		public long Total { get; set; }
		public Order[] Groups { get; set; }
	}
}