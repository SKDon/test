using System.IO;

namespace Alicargo.Core.Calculation
{
	public interface IExcelClientCalculation
	{
		MemoryStream Get(ClientCalculationGroup[] groups, string language);
	}
}
