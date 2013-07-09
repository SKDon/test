using Alicargo.Core.Contracts;

namespace Alicargo.Core.Models
{
	public sealed class Client : ClientData
	{
		public Transit Transit { get; set; }

		public AuthenticationModel AuthenticationModel { get; set; }
	}
}