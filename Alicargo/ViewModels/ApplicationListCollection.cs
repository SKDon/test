namespace Alicargo.ViewModels
{
	public sealed class ApplicationListCollection
	{
		public ApplicationGroup[] Groups { get; set; }		
		public ApplicationModel[] Data { get; set; }
		public long Total { get; set; }
	}
}
