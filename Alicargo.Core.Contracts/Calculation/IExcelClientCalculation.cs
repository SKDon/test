using System.IO;

namespace Alicargo.Core.Calculation
{
	public interface IExcelClientCalculation
	{
		MemoryStream Get(long clientId, string language);
	}
}
