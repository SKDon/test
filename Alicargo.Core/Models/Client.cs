namespace Alicargo.Core.Models
{
	public sealed class Client : ClientData
	{
		public TransitEditModel Transit { get; set; }

		public AuthenticationModel AuthenticationModel { get; set; }
	}
}