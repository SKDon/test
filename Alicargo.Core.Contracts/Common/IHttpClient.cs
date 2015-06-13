using System.Threading.Tasks;

namespace Alicargo.Core.Contracts.Common
{
	public interface IHttpClient
	{
		byte[] Get(string url);
		Task<byte[]> GetAsync(string url);
	}
}