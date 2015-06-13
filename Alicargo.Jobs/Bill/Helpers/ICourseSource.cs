using System.Threading.Tasks;

namespace Alicargo.Jobs.Bill.Helpers
{
	public interface ICourseSource
	{
		Task<decimal> GetEuroToRuble(string url);
	}
}