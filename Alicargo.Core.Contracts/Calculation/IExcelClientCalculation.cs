using System.IO;

namespace Alicargo.Core.Contracts.Calculation
{
	public interface IExcelClientCalculation
	{
		MemoryStream Get(long clientId, string language);
	}
}
