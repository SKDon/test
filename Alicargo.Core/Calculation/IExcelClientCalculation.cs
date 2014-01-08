using System.IO;
using Alicargo.Core.Calculation.Entities;

namespace Alicargo.Core.Calculation
{
	public interface IExcelClientCalculation
	{
		MemoryStream Get(ClientCalculationGroup[] groups, decimal balance, string language);
	}
}
